using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {
    public bool isClose = true;
    public int itemProbability;
    Animator ani;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        transform.rotation = new Quaternion();
    }

    public void openChest()
    {
        //spawn Item
        if (Random.Range(1,100) <= itemProbability )
        {
            spawnExItem();
        }

        //open chest ani
        ani.SetTrigger("open");


    }

    public void spawnExItem()
    {
        GameObject InstantiateItem = Instantiate(mapThingsGenerator.Static.selectType(), transform.position, Quaternion.Euler(-90, 0, 0));
        InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);
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
