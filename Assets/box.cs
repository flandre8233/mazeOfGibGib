using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {
    public int itemProbability;
    Animator ani;

    public int existItemType;
    public int coin;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        transform.rotation = new Quaternion();
    }

    private void Start()
    {
        coin = ((11 + 56 + 304) * (1 + playerDataBase.Static.currentFloor / 5));
        existItemType = itemAndEnemyProcessor.RandomProbabilitySystem(ref itemGenerator.Static.ProbabilityArray) - 1;
    }

    public void openChest()
    {
        //spawn Item
        if (Random.Range(0,100) <= itemProbability )
        {
            spawnExItem();
        }

        //open chest ani
        roundScript.Static.IsOpeningChest = true;
        ani.Play("open");
        StartCoroutine(WaitForAnimation("openChest"));

        GetCoin();
    }

    private IEnumerator WaitForAnimation(string animationTag)
    {
        do
        {
            yield return null;
        } while (ani.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));

        roundScript.Static.IsOpeningChest = false;
        Destroy(gameObject);
        //dead here
    }

    public void GetCoin()
    {
        playerDataBase.Static.COIN += coin;

    }

    public void spawnExItem()
    {
        GameObject InstantiateItem = Instantiate(mapThingsGenerator.Static.itemArray[existItemType], transform.position, Quaternion.Euler(-90, 0, 0));
        InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);

        //
    }



    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to)
    {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ-90));
        return rotation;
    }

    public void allwayFaceAtPlayer()
    {
        //float Angle = ImageLookAt2D(transform.position, chessMovement.Static.transform.position).eulerAngles.y;
        transform.rotation = ImageLookAt2D(transform.position, chessMovement.Static.transform.position);
    }

}
