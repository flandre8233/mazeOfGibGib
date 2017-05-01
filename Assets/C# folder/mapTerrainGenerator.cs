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
        float centerY = (Downest + Toppest) / 2;
        center = new Vector3(centerX,centerY,0);
        Debug.Log(center);
    }//要
    public void findTopLeftDownRight() {

        if (thisLevelAllFloor.Count == 0) {
            return;
        }

        Vector3 LeftCheckLine = new Vector3(-200, 0, 0);
        Vector3 UPCheckLine = new Vector3(0, 200, 0);
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
            if (Vector3.Distance(UPCheckLine, item.transform.position) <= UpGODistance) {
                UpGODistance = Vector3.Distance(UPCheckLine, item.transform.position);
                UpV3GO = item;
            }
            if (Vector3.Distance(LeftCheckLine, item.transform.position) >= RightGODistance) {
                RightGODistance = Vector3.Distance(LeftCheckLine, item.transform.position);
                RightV3GO = item;
            }
            if (Vector3.Distance(UPCheckLine, item.transform.position) >= DownGODistance) {
                DownGODistance = Vector3.Distance(UPCheckLine, item.transform.position);
                DownV3GO = item;
            }
        }
        Leftest = LeftV3GO.transform.position.x;
        Rightest = RightV3GO.transform.position.x;
        Toppest = UpV3GO.transform.position.y;
        Downest = DownV3GO.transform.position.y;
    } //要

    public void mapLimitDestroyer() {
        if (thisLevelAllFloor.Count <= 0) {
            return;
        }

        for (int i = 0; i < thisLevelAllFloor.Count; i++) {
            if ((((thisLevelAllFloor[i].transform.position.x > center.x + mapLimit.x) || (thisLevelAllFloor[i].transform.position.x < center.x - (mapLimit.x))) || ((thisLevelAllFloor[i].transform.position.y > center.y + mapLimit.y) || (thisLevelAllFloor[i].transform.position.y < center.y - (mapLimit.y))) )   ) {
                

                //thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                for (int j = 0; j < thisLevelAllFloor.Count; j++) {
                    if (thisLevelAllFloor[j].GetComponent<groundScript>().TerrainUID == thisLevelAllFloor[i].GetComponent<groundScript>().TerrainUID && j != i) {
                        thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                        //thisLevelAllFloor[j].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                }
                

                //thisLevelAllFloor[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                //Destroy(thisLevelAllFloor[i]);
            }

        }

    }

    public Vector2 mapLimit;


    public bool checkMapLimit(int where,int end) { //true 唔得 //false ok
        bool returnBool = false;
        if (thisLevelAllFloor.Count <= 0) {
            return true;
        }
        for (int i = where; i > end; i--) {
            // Debug.Log(mapLimit);
            if ((((thisLevelAllFloor[i].transform.position.x >= mapLimit.x) || (thisLevelAllFloor[i].transform.position.x <= -mapLimit.x)) || ((thisLevelAllFloor[i].transform.position.y >= mapLimit.y) || (thisLevelAllFloor[i].transform.position.y <= -mapLimit.y)) ) && !thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit ) {

                thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                returnBool = true;
               
            }
        }
        return returnBool;
    }

    public void resetTerrain() {
        ThisLevelAllTerrainParts.RemoveRange(1,ThisLevelAllTerrainParts.Count-1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        OLDcreateTerrain();
        allFloorDetach();
        canelFloorMesh();
        createNewFloorMesh();
        //findCenter();
    }

     public void checkPointTerrain() {
        ThisLevelAllTerrainParts.RemoveRange(1, ThisLevelAllTerrainParts.Count - 1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        createCheckPointTerrain();
        canelFloorMesh();
        createNewFloorMesh();
    }

    void allFloorDetach() {

        foreach (var item in thisLevelAllFloor) {
            item.transform.parent = null;
            //item.transform.parent = levelMapParentObject.transform;
        }

        /*
        for (int i = 1; i < thisLevelAllFloor.Count; i++) {
            
            //thisLevelAllFloor[i].transform.parent = thisLevelAllFloor[0].transform; //起初點
        }
       // thisLevelAllFloor[0].transform.position = Vector3.zero; //原點
       */


        //mapLimitDestroyer();

        foreach (var item in GameObject.FindGameObjectsWithTag("floor")) {
            if (item.GetComponent<groundScript>().delByMapLimit) {
                thisLevelAllFloor.Remove(item);
                    thisLevelAllFloor.Remove(item);
                Destroy(item);
            }
        }

           

    }

    void canelFloorMesh() {
        foreach (var item in thisLevelAllFloor) {
            Destroy(item.GetComponent<MeshFilter>());
            Destroy(item.GetComponent<MeshRenderer>());
        }
    }

    public GameObject floorModel;
    public GameObject[] floorModelDust;

    public GameObject floorCheckpointModel;

     void createNewFloorMesh() {
        foreach (var item in thisLevelAllFloor)
        { //地塊設定
            GameObject spawnObj;
            if (!roundScript.Static.isEnterCheckPoint())
            {
                spawnObj = Instantiate(floorModel, Vector3.zero, Quaternion.identity); //生成
            }
            else
            {
                spawnObj = Instantiate(floorCheckpointModel, Vector3.zero, Quaternion.identity); //生成
            }
            spawnObj.transform.parent = item.transform; //把地塊黏在當前的地形上
            spawnObj.transform.rotation = Quaternion.Euler(180, 0, randomRotation()  );
        // spawnObj.transform.rotation = randomRotation();
            //spawnObj.transform.localPosition = Vector3.zero;
            spawnObj.transform.localPosition = new Vector3(0, 0, -0.5f);

            if (!roundScript.Static.isEnterCheckPoint())
            {
                for (int i = 0; i < floorModelDust.Length; i++)
                { //泥土設定
                    if (itemAndEnemyProcessor.randomSetThingsType(floorModelDust) == i)
                    {
                        GameObject InstantiateItem = Instantiate(floorModelDust[i], Vector3.zero, Quaternion.identity);
                        InstantiateItem.transform.parent = item.transform;
                        InstantiateItem.transform.rotation = Quaternion.Euler(180, 0, randomRotation() );
                        //InstantiateItem.transform.localPosition = Vector3.zero;
                        InstantiateItem.transform.localPosition = new Vector3(0, 0, -0.5f);
                    }

                }
            }

        }
    }

    public GameObject checkPoint;
    void createCheckPointTerrain() {
        GameObject spawnObject = null;
        mapCenter = Vector3.zero;
        spawnObject = Instantiate(checkPoint, mapCenter, Quaternion.identity);

        allTerrainPort.Add(spawnObject);
        thisLevelAllFloor.Add(spawnObject);
        spawnObject.GetComponent<groundScript>().TerrainUID = 0;
        foreach (Transform child in spawnObject.transform) { //依個work
            if (child.GetComponent<groundScript>()) {
                thisLevelAllFloor.Add(child.gameObject);
                child.gameObject.GetComponent<groundScript>().TerrainUID = 0;
            }
        }

    }


    public void OLDcreateTerrain() {
        GameObject spawnObject = null;

        int count = 0;
        for (int i = 0; i < terrainLength; i++) {
            //Debug.Log(i + " . " + count + " . " + thisLevelAllFloor.Count);
            if (i == 0) {
                mapCenter = new Vector3((int)Random.Range(0, (mapLimit.x - 2) * 2) - (mapLimit.x - 2), (int)Random.Range(0, (mapLimit.y - 2) * 2) - (mapLimit.y - 2), 0);
                //mapCenter = new Vector3(0, 0, 0);
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], mapCenter, Quaternion.identity); //startpoint
                //spawnObject.GetComponent<groundScript>().type = groundType.startPoint;
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], new Vector3(0,0,0), Quaternion.identity);
                //spawnObject.transform.Rotate(randomRotation());
                spawnObject.transform.rotation = Quaternion.Euler(0,0, randomRotation() );
                ThisLevelAllTerrainParts.Add(spawnObject);
            }
            allTerrainPort.Add(spawnObject);
            thisLevelAllFloor.Add(spawnObject);
            spawnObject.GetComponent<groundScript>().TerrainUID = i;
            foreach (Transform child in spawnObject.transform) { //依個work
                thisLevelAllFloor.Add(child.gameObject);
                child.gameObject.GetComponent<groundScript>().TerrainUID = i;

                if (child.gameObject.GetComponent<groundScript>().type == groundType.isPortExitFloor) {
                    allTerrainPortExit.Add(child.gameObject);
                }

            }
            checkMapLimit(thisLevelAllFloor.Count - 1, count);
            //allTerrainPort[0].GetComponent<groundScript>().alreadyLink = true;
            if (i == 0 ) {
                for (int n = 0; n < thisLevelAllFloor.Count; n++) {
                    thisLevelAllFloor[n].GetComponent<groundScript>().alreadyLink = true;
                }
            }
            else {
                //linkAllPort
                if (i != 0) {
                    for (int h = 0; h < allTerrainPortExit.Count; h++) {

                        if (allTerrainPortExit[h].GetComponent<groundScript>().TerrainUID != i && !allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit) {

                            allTerrainPort[i].transform.position = allTerrainPortExit[h].transform.position;

                            //Debug.Log( (checkMapLimit(thisLevelAllFloor.Count - 1,count) + "   " + i ) );                        
                            if (checkMapLimit(thisLevelAllFloor.Count - 1, count) == false) { //檢查是否超出map限制
                                allTerrainPort[i].transform.parent = allTerrainPortExit[h].transform;
                                allTerrainPort[i].GetComponent<groundScript>().alreadyLink = true;
                                for (int n = count + 1; n < thisLevelAllFloor.Count; n++) {
                                    thisLevelAllFloor[n].GetComponent<groundScript>().alreadyLink = true;
                                }
                                //allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit = true;
                            }

                            else {

                                for (int k = 0; k < thisLevelAllFloor.Count; k++) { //回去最新一堆地板開始loop 進行刪除動作
                                    if (thisLevelAllFloor[k].GetComponent<groundScript>().TerrainUID == i) {
                                        thisLevelAllFloor[k].GetComponent<groundScript>().delByMapLimit = true;
                                    }
                                }
                                //allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit = true;
                            }

                            break;
                        }
                    }
                }

                count = thisLevelAllFloor.Count;
            }


           
        }

        foreach (var item in thisLevelAllFloor) { //刪掉未連上的floor
            if (!item.GetComponent<groundScript>().alreadyLink) {
                item.GetComponent<groundScript>().delByMapLimit = true;
            }

        }

        /*
        foreach (var item in allTerrainPort) {
            if (!item.GetComponent<groundScript>().alreadyLink) {
                for (int i = 0; i < thisLevelAllFloor.Count; i++) {
                    if (item.GetComponent<groundScript>().TerrainUID == thisLevelAllFloor[i].GetComponent<groundScript>().TerrainUID && !thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit) {
                        thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit = true;
                        //Destroy(thisLevelAllFloor[i]);
                        //thisLevelAllFloor.RemoveAt(i);
                    }
                }
            }
            
        }
        */
    }

    int randomRotation() {
        short rotation = 0;
        short randomNumber = (short)Random.Range(0,3);
        switch (randomNumber) {
            case 0:
                rotation = 0;
                break;
            case 1:
                rotation = 90;
                break;
            case 2:
                rotation = 270;
                break;
            case 3:
                rotation = 180;
                break;

            default:
                rotation = 0;
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
