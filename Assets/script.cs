using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {
    [SerializeField]
    GameObject player;

    [SerializeField]
    Material metal;

    List<GameObject> allBlock = new List<GameObject>();


    void openAttackRange() {
        //allBlock[2].GetComponent<Renderer>().material = metal;

        foreach (GameObject item in allBlock) { //select
            if (item != player) {
                if (

    (player.transform.position.x - 1 <= item.transform.position.x &&
    player.transform.position.x + 1 >= item.transform.position.x &&
    player.transform.position.y - 1 <= item.transform.position.y &&
    player.transform.position.y + 1 >= item.transform.position.y) ||

    (player.transform.position.x - 2 <= item.transform.position.x &&
    player.transform.position.x + 2 >= item.transform.position.x &&
    player.transform.position.y == item.transform.position.y) ||

    (player.transform.position.y - 2 <= item.transform.position.y &&
    player.transform.position.y + 2 >= item.transform.position.y &&
    player.transform.position.x == item.transform.position.x)


    ) {

                    item.GetComponent<Renderer>().material.color = new Color(255,0,0,255);
                }
            }

        }
    }

    void closeAttackRange() {
        foreach (GameObject item in allBlock) {
            if (item.GetComponent<Renderer>().material.color == new Color(255, 0, 0,255)) {
                item.GetComponent<Renderer>().material.color = new Color(255, 255, 255,0);
            }
        }
    }


    // Use this for initialization
    void Start() {
        foreach (var item in GameObject.FindGameObjectsWithTag("item")) {
            allBlock.Add(item);
            if (item != player) {
                item.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 0);
            }
        } // array to list
    }

    /*
                if (item!=player &&
                player.transform.position.x -2 <= item.transform.position.x &&
                player.transform.position.x + 2 >= item.transform.position.x &&
                player.transform.position.y - 2 <= item.transform.position.y &&
                player.transform.position.y + 2 >= item.transform.position.y &&

                !(player.transform.position.x - 1 <= item.transform.position.x &&
                player.transform.position.x + 1 >= item.transform.position.x &&
                player.transform.position.y - 1 <= item.transform.position.y &&
                player.transform.position.y + 1 >= item.transform.position.y)


                )




                        player.transform.position.x -1 <= item.transform.position.x &&
                player.transform.position.x + 1 >= item.transform.position.x &&
                player.transform.position.y - 1 <= item.transform.position.y &&
                player.transform.position.y + 1 >= item.transform.position.y

    */

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {

            openAttackRange();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            closeAttackRange();
        }
    }
}
