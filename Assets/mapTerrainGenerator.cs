using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapTerrainGenerator : MonoBehaviour {
    public static mapTerrainGenerator Static;
    public List<GameObject> thisLevelAllFloor;
    public List<GameObject> gameAllTerrainParts;
    public List<GameObject> ThisLevelAllTerrainParts;

    public int terrainLength;

    public List<GameObject> allTerrainPort; //0同0就係同一個地形 1同1就係令一個 port同portEXIT一定要成相成對
    public List<GameObject> allTerrainPortExit;

    public GameObject levelMapParentObject;

    void Awake() {
        if (Static != null) {
            Destroy(this);
        }
        else {
            Static = this;
        }
    }

    // Use this for initialization
    void Start () {


        resetTerrain();
    }

     public void resetTerrain() {
        ThisLevelAllTerrainParts.Clear();

        selectTerrainInAssets(); //碰撞會在下一個frame才做
        FindALLTerrainPortAndPortExit(ref allTerrainPort,ref allTerrainPortExit);

        syncTwoPortList(ref allTerrainPort);
         syncTwoPortList(ref allTerrainPortExit);

        linkAllPort();
        allFloorDetach();
    }

    void allFloorDetach() {
        foreach (var item in thisLevelAllFloor) {
            item.transform.parent = levelMapParentObject.transform;
        }
    }

    void FindALLTerrainPortAndPortExit(ref List<GameObject> Port,ref  List<GameObject>  PortExit) {
        Port.Clear();
        PortExit.Clear();
        if (thisLevelAllFloor.Count != 0) {
            foreach (var item in thisLevelAllFloor) {
                if (item.GetComponent<groundScript>().type == groundType.isPortFloor) {
                    Port.Add(item);
                }
                if (item.GetComponent<groundScript>().type == groundType.isPortExitFloor) {
                    PortExit.Add(item);
                }
            }
        }

    }
    void syncTwoPortList(ref List<GameObject> port) {
        List<GameObject> returnObject = new List<GameObject>();
        for (int i = port.Count - 1; i >= 0; i--) {
            foreach (var item in port) {
                if (item.GetComponent<groundScript>().TerrainUID == i) {
                    returnObject.Add(item);
                }
            }
        }
        port = returnObject;
    }

    void selectTerrainInAssets() { //隨機在數據庫抽出地形
        for (int i = 0; i < terrainLength; i++) {
            int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
            GameObject spawnObject = Instantiate(gameAllTerrainParts[randomNumber], Vector3.up*i*50, Quaternion.identity);
            ThisLevelAllTerrainParts.Add(spawnObject);

            thisLevelAllFloor.Add(spawnObject);
            spawnObject.GetComponent<groundScript>().TerrainUID = i;
            foreach (Transform child in spawnObject.transform) { //依個work
                thisLevelAllFloor.Add(child.gameObject);
                child.gameObject.GetComponent<groundScript>().TerrainUID = i;
            }

        }
    }

    void linkAllPort() {
        for (int i = 0; i < allTerrainPort.Count - 1; i++) {
            allTerrainPort[i].transform.position = allTerrainPortExit[i + 1].transform.position;
            allTerrainPort[i].transform.Rotate(randomRotation());
            allTerrainPort[i].transform.parent = allTerrainPortExit[i + 1].transform;
        }

    }

    Vector3 randomRotation() {
        Vector3 rotation = new Vector3() ;
        int randomNumber = Random.Range(0,4);
        switch (randomNumber) {
            case 0:
                rotation = new Vector3(0,0,0);
                break;
            case 1:
                rotation = new Vector3(0, 0, 90);
                break;
            case 2:
                rotation = new Vector3(0, 0, 180);
                break;
            case 3:
                rotation = new Vector3(0, 0, 270);
                break;

            default:
                rotation = new Vector3(0, 0, 0);
                break;
        }

        return rotation;
    }



    // Update is called once per frame
    void Update () {
        /*
        if (Input.anyKeyDown) {
            ThisLevelAllTerrainParts.Clear();
            allTerrainPort.Clear();
            allTerrainPortExit.Clear();
        }
        */
    }
}
