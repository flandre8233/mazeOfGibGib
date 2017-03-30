﻿using System.Collections;
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
            Debug.Log(mapLimit);
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
        //findCenter();
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

    public void OLDcreateTerrain() {
        GameObject spawnObject = null;

        int count = 0;
        for (int i = 0; i < terrainLength; i++) {
            //Debug.Log(i + " . " + count + " . " + thisLevelAllFloor.Count);
            if (i == 0) {
                mapCenter = new Vector3(Random.Range(0, 16)-8, Random.Range(0, 16)-8, 0);
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], mapCenter, Quaternion.identity); //startpoint
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], Vector3.up, Quaternion.identity);
                spawnObject.transform.Rotate(randomRotation());
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
            //linkAllPort
            if (i != 0) {
                for (int h = 0; h < allTerrainPortExit.Count; h++) {

                    if (allTerrainPortExit[h].GetComponent<groundScript>().TerrainUID != i && !allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit) {

                        allTerrainPort[i].transform.position = allTerrainPortExit[h].transform.position;
                        Debug.Log( (checkMapLimit(thisLevelAllFloor.Count - 1,count) + "   " + i ) );                        
                        if (checkMapLimit(thisLevelAllFloor.Count - 1, count) == false) { //檢查是否超出map限制
                            allTerrainPort[i].transform.parent = allTerrainPortExit[h].transform;
                            allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit = true;
                        }

                        else {

                            for (int k = 0 ; k < thisLevelAllFloor.Count; k++) { //回去最新一堆地板開始loop 進行刪除動作
                                if (thisLevelAllFloor[k].GetComponent<groundScript>().TerrainUID == i) {
                                    thisLevelAllFloor[k].GetComponent<groundScript>().delByMapLimit = true;
                                }
                            }
                            allTerrainPortExit[h].GetComponent<groundScript>().delByMapLimit = true;
                        }

                        break;
                    }
                }
            }

            count = thisLevelAllFloor.Count;
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