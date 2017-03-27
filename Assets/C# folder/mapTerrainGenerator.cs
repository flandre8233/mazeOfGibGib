using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapTerrainGenerator : MonoBehaviour {
    public static mapTerrainGenerator Static;
    public List<GameObject> thisLevelAllFloor;
    public List<GameObject> gameAllTerrainParts;
    public List<GameObject> ThisLevelAllTerrainParts;


    public Vector3 mapCenter = new Vector3();
    public Vector3 center;
    //public GameObject startPoint;

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

    float Toppest;
    float Leftest;
    float Downest;
    float Rightest;

    public void findCenter() {//可能唔要
        findTopLeftDownRight();
        float centerX = (Leftest + Rightest) / 2;
        float centerY = (Toppest + Downest ) / 2;
        center = new Vector3(centerX,centerY,0);
        Debug.Log(center);
    }//可能唔要
    public void findTopLeftDownRight() {

        if (thisLevelAllFloor.Count == 0) {
            return;
        }

        Vector3 LeftCheckLine = new Vector3(-200, 0, 0);
        Vector3 UPCheckLine = new Vector3(200, 0, 0);
        GameObject LeftV3GO = thisLevelAllFloor[0];
        GameObject UpV3GO = thisLevelAllFloor[0];
        GameObject RightV3GO = thisLevelAllFloor[0];
        GameObject DownV3GO = thisLevelAllFloor[0];

        float LeftGODistance = Vector3.Distance(LeftCheckLine, LeftV3GO.transform.position);
        float UpGODistance = Vector3.Distance(UPCheckLine, LeftV3GO.transform.position);
        float RightGODistance = Vector3.Distance(LeftCheckLine, LeftV3GO.transform.position);
        float DownGODistance = Vector3.Distance(UPCheckLine, LeftV3GO.transform.position);

        foreach (var item in thisLevelAllFloor) {

            if (Vector3.Distance(LeftCheckLine, item.transform.position) <= LeftGODistance) {
                LeftGODistance = Vector3.Distance(LeftCheckLine, item.transform.position);
                LeftV3GO = item;
            }
            if (Vector3.Distance(UPCheckLine, item.transform.position) >= UpGODistance) {
                UpGODistance = Vector3.Distance(UPCheckLine, item.transform.position);
                UpV3GO = item;
            }
            if (Vector3.Distance(LeftCheckLine, item.transform.position) >= RightGODistance) {
                RightGODistance = Vector3.Distance(LeftCheckLine, item.transform.position);
                RightV3GO = item;
            }
            if (Vector3.Distance(UPCheckLine, item.transform.position) <= DownGODistance) {
                DownGODistance = Vector3.Distance(UPCheckLine, item.transform.position);
                DownV3GO = item;
            }
        }
        //LeftV3GO.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Leftest = LeftV3GO.transform.position.x;
        Rightest = RightV3GO.transform.position.x;
        Toppest = UpV3GO.transform.position.x;
        Downest = DownV3GO.transform.position.x;
    } //可能唔要

    public void mapLimitDestroyer() {
        if (thisLevelAllFloor.Count <= 0) {
            return;
        }

        int widthLimit = 5;
        int heightLimit = 5;
        for (int i = 0; i < thisLevelAllFloor.Count; i++) {
            if ((((thisLevelAllFloor[i].transform.position.x > mapCenter.x + widthLimit) || (thisLevelAllFloor[i].transform.position.x < mapCenter.x - widthLimit)) || ((thisLevelAllFloor[i].transform.position.y > mapCenter.y + heightLimit) || (thisLevelAllFloor[i].transform.position.y < mapCenter.y - heightLimit)) )   ) {
                

                //thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                if (thisLevelAllFloor[i].GetComponent<groundScript>().type == groundType.isPortFloor) {
                    Debug.Log("sadsadfsafsafsafsafdgsdgsd");
                    for (int j = 0; j < thisLevelAllFloor.Count; j++) {
                        if (thisLevelAllFloor[j].GetComponent<groundScript>().TerrainUID == thisLevelAllFloor[i].GetComponent<groundScript>().TerrainUID && j != i ) {

                            thisLevelAllFloor[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                            //Destroy(thisLevelAllFloor[j]);
                            //thisLevelAllFloor.RemoveAt(j);
                            //j--;
                        }
                    }
                }

                thisLevelAllFloor[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //Destroy(thisLevelAllFloor[i]);
                //thisLevelAllFloor.RemoveAt(i);

                //i--;
            }

        }

    }

    public bool checkMapLimit(int where) { //true 唔得 //false ok
        if (thisLevelAllFloor.Count <= 0) {
            return true;
        }

        int widthLimit = 5;
        int heightLimit = 5;
        for (int i = where; i < thisLevelAllFloor.Count; i++) {
            if (((thisLevelAllFloor[i].transform.position.x > mapCenter.x + widthLimit) || (thisLevelAllFloor[i].transform.position.x < mapCenter.x - widthLimit)) || ((thisLevelAllFloor[i].transform.position.y > mapCenter.y + heightLimit) || (thisLevelAllFloor[i].transform.position.y < mapCenter.y - heightLimit))) {
                return true;
            }

        }
        return false;
    }

    public void resetTerrain() {
        ThisLevelAllTerrainParts.RemoveRange(1,ThisLevelAllTerrainParts.Count-1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        //selectTerrainInAssets(); //碰撞會在下一個frame才做
        createTerrain();
        //FindALLTerrainPortAndPortExit(ref allTerrainPort,ref allTerrainPortExit);

        //syncTwoPortList(ref allTerrainPort);
        //syncTwoPortList(ref allTerrainPortExit);

        //linkAllPort();
        allFloorDetach();
        mapLimitDestroyer();
    }

    void allFloorDetach() {

        thisLevelAllFloor[0].transform.parent = levelMapParentObject.transform;
        for (int i = 1; i < thisLevelAllFloor.Count; i++) {
            thisLevelAllFloor[i].transform.parent = levelMapParentObject.transform;
            thisLevelAllFloor[i].transform.parent = thisLevelAllFloor[0].transform; //起初點
        }
        thisLevelAllFloor[0].transform.position = Vector3.zero; //原點

        foreach (var item in thisLevelAllFloor) {
            item.transform.parent = null;
        }

        //mapLimitDestroyer();
        findCenter();
    }

    void FindALLTerrainPortAndPortExit(ref List<GameObject> Port,ref  List<GameObject>  PortExit) {
        Port.Clear();
        PortExit.Clear();

        if (thisLevelAllFloor.Count == 0) {
            return;
        }

        foreach (var item in thisLevelAllFloor) {
            if (item.GetComponent<groundScript>().type == groundType.startPoint) {
                Port.Add(item);
                //PortExit.Add(item);
            }

            if (item.GetComponent<groundScript>().type == groundType.isPortFloor) {
                Port.Add(item);
            }
            if (item.GetComponent<groundScript>().type == groundType.isPortExitFloor) {
                PortExit.Add(item);
            }
        }

    }
    void syncTwoPortList(ref List<GameObject> port) {
        List<GameObject> returnObject = new List<GameObject>();
        for (int i = 0 ; i < port.Count ; i++) {
            foreach (var item in port) {
                if (item.GetComponent<groundScript>().TerrainUID == i) {
                    returnObject.Add(item);
                }
            }
        }
        port = returnObject;
    }

    void selectTerrainInAssets() { //隨機在數據庫抽出地形
        GameObject spawnObject = null;

        for (int i = 0; i < terrainLength; i++) {
            if (i == 0) {
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], Vector3.zero, Quaternion.identity); //startpoint
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], Vector3.up , Quaternion.identity);
                ThisLevelAllTerrainParts.Add(spawnObject);
            }

            thisLevelAllFloor.Add(spawnObject);
            spawnObject.GetComponent<groundScript>().TerrainUID = i;
            foreach (Transform child in spawnObject.transform) { //依個work
                thisLevelAllFloor.Add(child.gameObject);
                child.gameObject.GetComponent<groundScript>().TerrainUID = i;
            }


        }
    }

    public void createTerrain() {
        GameObject spawnObject = null;

        int count = 0;
        for (int i = 0; i < terrainLength; i++) {
            if (i == 0) {
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], Vector3.zero, Quaternion.identity); //startpoint
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], Vector3.up, Quaternion.identity);
                ThisLevelAllTerrainParts.Add(spawnObject);
            }
            thisLevelAllFloor.Add(spawnObject);
            spawnObject.GetComponent<groundScript>().TerrainUID = i;
            foreach (Transform child in spawnObject.transform) { //依個work
                thisLevelAllFloor.Add(child.gameObject);
                child.gameObject.GetComponent<groundScript>().TerrainUID = i;
            }

            for (int j = count; j < thisLevelAllFloor.Count; j++) { //findallportandexit 由最新一堆地板開始loop
                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.startPoint) {
                    allTerrainPort.Add(thisLevelAllFloor[j]);
                }

                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.isPortFloor) {
                    allTerrainPort.Add(thisLevelAllFloor[j]);
                }
                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.isPortExitFloor) {
                    allTerrainPortExit.Add(thisLevelAllFloor[j]);

                    //linkAllPort
                    foreach (var item in allTerrainPortExit) {
                        if (item.GetComponent<groundScript>().TerrainUID != allTerrainPort[i].GetComponent<groundScript>().TerrainUID) {

                            allTerrainPort[i].transform.position = item.transform.position;
                            allTerrainPort[i].transform.Rotate(randomRotation());
                            allTerrainPort[i].transform.parent = item.transform;
                            allTerrainPortExit.Remove(item);
                            break;
                        }
                    }
                }
            }
            count = thisLevelAllFloor.Count;
        }
    }

    /*
    public void OLDcreateTerrain() {
        GameObject spawnObject = null;

        int count = 0;
        for (int i = 0; i < terrainLength; i++) {
            Debug.Log(i + " . " + count + " . " +thisLevelAllFloor.Count );
            if (i == 0) {
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], Vector3.zero, Quaternion.identity); //startpoint
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], Vector3.up, Quaternion.identity);
                ThisLevelAllTerrainParts.Add(spawnObject);
            }
            thisLevelAllFloor.Add(spawnObject);
            spawnObject.GetComponent<groundScript>().TerrainUID = i;
            foreach (Transform child in spawnObject.transform) { //依個work
                thisLevelAllFloor.Add(child.gameObject);
                //child.parent = null;
                child.gameObject.GetComponent<groundScript>().TerrainUID = i;
            }

            bool FindAndDone = false;
            Debug.Log(thisLevelAllFloor.Count);
            for (int j = count; j < thisLevelAllFloor.Count; j++) { //findallportandexit 由最新一堆地板開始loop
                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.startPoint) {
                    allTerrainPort.Add(thisLevelAllFloor[j]);
                    //PortExit.Add(item);
                }

                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.isPortFloor) {
                    allTerrainPort.Add(thisLevelAllFloor[j]);
                }
                if (thisLevelAllFloor[j].GetComponent<groundScript>().type == groundType.isPortExitFloor) {
                    allTerrainPortExit.Add(thisLevelAllFloor[j]);

                    //linkAllPort
                    foreach (var item in allTerrainPortExit) {
                        if (item.GetComponent<groundScript>().TerrainUID != allTerrainPort[i].GetComponent<groundScript>().TerrainUID) {

                            allTerrainPort[i].transform.position = item.transform.position;
                            allTerrainPort[i].transform.Rotate(randomRotation());
                            

                            if (checkMapLimit(count) == false) { //檢查是否超出map限制
                                allTerrainPort[i].transform.parent = item.transform;
                                allTerrainPortExit.Remove(item);
                            }
                            else {
                                GameObject target = null;
                                for (int k = count; k < thisLevelAllFloor.Count; k++) { //回去最新一堆地板開始loop 進行刪除動作
                                    if (thisLevelAllFloor[k].GetComponent<groundScript>().type == groundType.isPortFloor) {
                                        target = thisLevelAllFloor[k];
                                    }
                                    thisLevelAllFloor.RemoveAt(k);
                                    k--;
                                }
                                Destroy(target);
                                
                                FindAndDone = true;
                            }

                            break;
                        }
                    }
                    if (FindAndDone) {
                        //j = count; //還原j指針
                        i--;
                        break;
                    }
                }

            }

            count = thisLevelAllFloor.Count;
        }

    }
    */
    void linkAllPort() { //可能存在問題 多重出口
        allTerrainPortExit.RemoveAt(0);
         for (int i = 0; i < allTerrainPort.Count ; i++) {
        //for (int i = allTerrainPort.Count-1; i > 0 ; i--) {
            foreach (var item in allTerrainPortExit) {
                if (item.GetComponent<groundScript>().TerrainUID != allTerrainPort[i].GetComponent<groundScript>().TerrainUID) {

                    allTerrainPort[i].transform.position = item.transform.position;
                    allTerrainPort[i].transform.Rotate(randomRotation());
                    allTerrainPort[i].transform.parent = item.transform;

                    allTerrainPortExit.Remove(item);
                    break;
                }
            }

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
