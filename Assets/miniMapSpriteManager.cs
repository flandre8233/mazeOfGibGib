using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapSpriteManager : MonoBehaviour
{
    public static miniMapSpriteManager Static;

    public List<Sprite> miniMapSpriteArray;

    public List<Transform> allMiniIcon;

    GameObject spawnIcon(int arrayNumber,Vector3 v3,int sortLayer,Transform parent)
    {
        GameObject newObj = new GameObject();
        allMiniIcon.Add(newObj.transform);
        newObj.layer = LayerMask.NameToLayer("UI");
        SpriteRenderer objSprite = newObj.AddComponent<SpriteRenderer>();

        objSprite.sprite = miniMapSpriteArray[arrayNumber];
        objSprite.transform.localScale = new Vector3(1.9f, 1.9f, 1);
        objSprite.transform.position = v3;
        objSprite.transform.parent = parent.transform;
        objSprite.sortingOrder = sortLayer;

        return newObj;
    }

    void genFloorIcon()
    {
        foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor)
        {
            Vector3 v3 = new Vector3(item.transform.position.x, item.transform.position.y, 5f);
            spawnIcon(0, v3, 0,item.transform);
        }
    }

    void genExitIcon()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("exit") )
        {
            Vector3 v3 = new Vector3(item.transform.position.x, item.transform.position.y, 5f);
            spawnIcon(4, v3, 2, item.transform);
        }
    }

    void genItemIcon()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("item"))
        {
            Vector3 v3 = new Vector3(item.transform.position.x, item.transform.position.y, 5f);
            switch (item.GetComponent<itemScript>().type)
            {
                case itemType.HP:
                    break;
                case itemType.SP:
                    spawnIcon(10, v3, 2, item.transform);
                    break;
                case itemType.SPNoCost:
                    spawnIcon(9, v3, 2, item.transform);
                    break;
                case itemType.ATK:
                    spawnIcon(7, v3, 2, item.transform);
                    break;
                case itemType.DEF:
                    spawnIcon(8, v3, 2, item.transform);
                    break;
                case itemType.HPMAX:
                    spawnIcon(5, v3, 2, item.transform);
                    break;
                case itemType.SPMAX:
                    spawnIcon(6, v3, 2, item.transform);
                    break;
                case itemType.COIN:
                    break;
                default:
                    break;
            }
        }
    }

    void genChestIcon()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("chest"))
        {
            Vector3 v3 = new Vector3(item.transform.position.x, item.transform.position.y, 5f);
            spawnIcon(2, v3, 2, item.transform);
        }
    }

    void genEnemyIcon()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Vector3 v3 = new Vector3(item.transform.position.x, item.transform.position.y, 5f);
            GameObject newObj =  spawnIcon(3, v3, 2, item.transform);
            newObj.AddComponent<lockRotation>();
            /*
            switch (item.GetComponent<enemyDataBase>().Level)
            {
                case 1:
                    spawnIcon(3, v3, 2);
                    break;
                case 2:
                    spawnIcon(3, v3, 2);
                    break;
                case 3:
                    spawnIcon(3, v3, 2);
                    break;
                case 4:
                    spawnIcon(3, v3, 2);
                    break;
                default:
                    break;
            }*/

        }
    }


    public void startGenIcon()
    {
        genFloorIcon();
        genExitIcon();
        genItemIcon();
        genChestIcon();
        genEnemyIcon();
    }




    public void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }
}
