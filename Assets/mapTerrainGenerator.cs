using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapTerrainGenerator : MonoBehaviour {
    public List<GameObject> allTerrainPort; //0同0就係同一個地形 1同1就係令一個 port同portEXIT一定要成相成對
    public List<GameObject> allTerrainPortExit;
    // Use this for initialization
    void Start () {
        allTerrainPort = FindALLTerrainPort();
        allTerrainPortExit = FindALLTerrainPortExit();
        allTerrainPort = syncTwoPortList(allTerrainPort);
        allTerrainPortExit = syncTwoPortList(allTerrainPortExit);
        //allTerrainPort[0].transform.position = allTerrainPortExit[1].transform.position;
        //allTerrainPort[0].transform.parent = allTerrainPortExit[1].transform;
        //transform.parent = allTerrainPort[0].transform;

        linkAllPort();
        WithForeachLoop(allTerrainPortExit[allTerrainPort.Count-1]);
        Destroy(allTerrainPortExit[allTerrainPort.Count-1]);


    }

    void linkAllPort() {
        for (int i = 0; i < allTerrainPort.Count - 1; i++) {
            allTerrainPort[i].transform.position = allTerrainPortExit[i + 1].transform.position;
            allTerrainPort[i].transform.rotation = new Quaternion(0,0,90,0);
            allTerrainPort[i].transform.parent = allTerrainPortExit[i + 1].transform;
            WithForeachLoop(allTerrainPortExit[i]);
            Destroy(allTerrainPortExit[i]);
        }

    }

    void WithForeachLoop(GameObject go) {
        foreach (Transform child in go.transform)
            child.parent = null;
    }

    List<GameObject> syncTwoPortList( List<GameObject> port) {
        List<GameObject> returnObject = new List<GameObject>();
        for (int i = port.Count-1; i >= 0; i--) {
            foreach (var item in port) {
                if (item.GetComponent<groundScript>().TerrainUID == i) {
                    returnObject.Add(item);
                }
            }
        }
        return returnObject;
    }

    List<GameObject> FindALLTerrainPort() {
        GameObject[] allBlock;
        List<GameObject> returnObject = new List<GameObject>();
        allBlock = GameObject.FindGameObjectsWithTag("floor");
        if (allBlock.Length != 0) {
            foreach (var item in allBlock) {
                if (item.GetComponent<groundScript>().isPortFloor) {
                    returnObject.Add(item);
                }
            }
        }

        return returnObject;
    }
    List<GameObject> FindALLTerrainPortExit() {
        GameObject[] allBlock;
        List<GameObject> returnObject = new List<GameObject>();
        allBlock = GameObject.FindGameObjectsWithTag("floor");
        if (allBlock.Length != 0) {
            foreach (var item in allBlock) {
                if (item.GetComponent<groundScript>().isPortExitFloor) {
                    returnObject.Add(item);
                }
            }
        }

        return returnObject;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
