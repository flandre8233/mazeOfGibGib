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
            if ((((thisLevelAllFloor[i].transform.position.x > center.x + mapLimit.x+1000) || (thisLevelAllFloor[i].transform.position.x < center.x - (mapLimit.x+1000))) || ((thisLevelAllFloor[i].transform.position.y > center.y + mapLimit.y+1000) || (thisLevelAllFloor[i].transform.position.y < center.y - (mapLimit.y+1000))) )   ) {
                

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
            if ((((thisLevelAllFloor[i].transform.position.x >= mapLimit.x+ 1000) || (thisLevelAllFloor[i].transform.position.x <= -mapLimit.x+ 1000)) || ((thisLevelAllFloor[i].transform.position.y >= mapLimit.y+ 1000) || (thisLevelAllFloor[i].transform.position.y <= -mapLimit.y+ 1000)) ) && !thisLevelAllFloor[i].GetComponent<groundScript>().delByMapLimit ) {

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

        classificationFloorType();
    }

    public void reGenTerrainFromSave()
    {
        ThisLevelAllTerrainParts.RemoveRange(1, ThisLevelAllTerrainParts.Count - 1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        LoadSaveTerrainSetting();

        allFloorDetach();
        canelFloorMesh();

        classificationFloorType();
    }

    public void checkPointTerrain()
    {
        ThisLevelAllTerrainParts.RemoveRange(1, ThisLevelAllTerrainParts.Count - 1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        specificTerrain(checkPoint);
        canelFloorMesh();
        classificationFloorType();
    }

    public void startPointTerrain()
    {

        ThisLevelAllTerrainParts.RemoveRange(1, ThisLevelAllTerrainParts.Count - 1);
        allTerrainPort.Clear();
        allTerrainPortExit.Clear();

        specificTerrain(startPoint);
        canelFloorMesh();
        classificationFloorType();
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



    public GameObject[] floorModel;

    public GameObject[] spikeModel;

    public GameObject[] grassModelDust;
    public GameObject[] iceModelDust;
    public GameObject[] lavaModelDust;
    public GameObject[] desertModelDust;
    public GameObject[] swampModelDust;

    public GameObject floorCheckpointModel;

    int floorArea;
    void classificationFloorType()
    {
        if (playerDataBase.Static.currentFloor > 40)
        {
            floorArea = 4;
            skyboxSetting(floorArea);

            createNewFloorMesh(floorModel[floorArea], swampModelDust);
        }
        else if (playerDataBase.Static.currentFloor > 30)
        {
            floorArea = 3;
            skyboxSetting(floorArea);
            createNewFloorMesh(floorModel[floorArea], desertModelDust);
        }
        else if (playerDataBase.Static.currentFloor > 20)
        {

            floorArea = 2;
            skyboxSetting(floorArea);
            createNewFloorMesh(floorModel[floorArea], lavaModelDust);
        }
        else if (playerDataBase.Static.currentFloor > 10)
        {
            floorArea = 1;
            skyboxSetting(0);
            createNewFloorMesh(floorModel[1], iceModelDust);
        }
        else
        {
            skyboxSetting(1);
            createNewFloorMesh(floorModel[0], grassModelDust);
        }
    }

    public Material[] skyboxMaterialArray;

    void skyboxSetting(int type)
    {
        RenderSettings.skybox = skyboxMaterialArray[type];
    }

     void createNewFloorMesh(GameObject model ,GameObject[] modelDust) {
        for (int i = 0; i < thisLevelAllFloor.Count; i++)
        { //地塊設定
            var item = thisLevelAllFloor[i];

            GameObject spawnObj;
            if (!roundScript.Static.isEnterCheckPoint())
            {
                spawnObj = Instantiate(model, Vector3.zero, Quaternion.identity); //生成

            }
            else
            {
                spawnObj = Instantiate(floorCheckpointModel, Vector3.zero, Quaternion.identity); //生成
            }



            spawnObj.transform.parent = item.transform; //把地塊黏在當前的地形上

            spawnObj.transform.rotation = Quaternion.Euler(180, 0, randomRotation()  );
        // spawnObj.transform.rotation = randomRotation();
         
            //spawnObj.transform.localPosition = Vector3.zero;
            spawnObj.transform.localPosition = new Vector3(0, 0, 0);

            if (!roundScript.Static.isEnterCheckPoint())
            {
                if (gamemanager.Static.beLoaded)
                {

                    if (testSaveLoad.Static.mydata.allFloorVector2[i].isSpike) //load突刺 暫時放係度
                    {
                        addSpikeFunction(spawnObj.transform.parent.gameObject, spawnObj);
                        Spike spikeObj = spawnObj.transform.parent.gameObject.GetComponent<Spike>();

                        spikeObj.curRoundCountDown = testSaveLoad.Static.mydata.allFloorVector2[i].curRoundCountDown; //令突刺進度回複
                        if (spikeObj.curRoundCountDown >= spikeObj.perRoundShowUpSpike) //
                        {
                            Debug.Log(spikeObj.curRoundCountDown + "  kk");
                            spikeObj.inShowSpike = true;
                            spikeObj.serializeSpike();
                        }
                }
                else
                {
                    if (Random.Range(0, 100) < 10) //尖刺生成
                    {
                        addSpikeFunction(spawnObj.transform.parent.gameObject, spawnObj);
                    }
                }
            }

                if (!roundScript.Static.isEnterCheckPoint())
                { //泥土設定
                    int Randnumber = Random.Range(0, modelDust.Length + 1);
                    if (Randnumber == modelDust.Length)
                    {
                        continue;
                    }
                    //Randnumber -= 2;
                    GameObject InstantiateItem = Instantiate(modelDust[Randnumber], spawnObj.transform);
                    //InstantiateItem.transform.parent = spawnObj.transform;
                    InstantiateItem.transform.rotation = Quaternion.Euler(180, 0, randomRotation());
                    InstantiateItem.transform.localPosition = Vector3.zero;
                    //InstantiateItem.transform.localPosition = new Vector3(0, 0, -0.5f);

                }

            }

        }
    }

    public GameObject spikePrefab;
    void addSpikeFunction(GameObject parent,GameObject plane)
    {
        groundScript orlGroundScript = parent.GetComponent<groundScript>();
        Spike spikeComponent = parent.AddComponent<Spike>();
        spikeComponent.delByMapLimit = orlGroundScript.delByMapLimit;
        spikeComponent.alreadyLink = orlGroundScript.alreadyLink;
        spikeComponent.TerrainUID = orlGroundScript.TerrainUID;
        Destroy(orlGroundScript);

        GameObject go = Instantiate(spikeModel[floorArea], plane.transform);
        spikeComponent.spikeObjectTransform = go.transform;
        spikeComponent.planeTransform = plane.transform;
        spikeComponent.ani = go.GetComponentInChildren<Transform>().GetComponentInChildren<Animator>();
        spikeComponent.isSpike = true;
        spikeComponent.serializeSpike();
        spikeComponent.UpdataSystem += spikeComponent.earthQuake ;
        roundScript.Static.spikeSystem += spikeComponent.countSpikeRound;
        roundScript.Static.groundCheckSystem += spikeComponent.roundSystemUseOnly;
        //go.transform.localPosition = Vector3.zero;

    }

    public GameObject startPoint;
    public GameObject checkPoint;
    void specificTerrain(GameObject go) {
        GameObject spawnObject = null;
        mapCenter = Vector3.zero;
        spawnObject = Instantiate(go, mapCenter, Quaternion.identity);

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

    public GameObject singleCube;

     public void LoadSaveTerrainSetting()
    {
        //testSaveLoad.Static.mydata.allFloorVector2

        foreach (var item in testSaveLoad.Static.mydata.allFloorVector2)
        {
            GameObject spawnObject = Instantiate(singleCube, new Vector3(item.X,item.Y,0),Quaternion.identity );
            thisLevelAllFloor.Add(spawnObject);
            spawnObject.transform.rotation = Quaternion.Euler(0, 0, randomRotation());
        }
    }

    public void OLDcreateTerrain() {
        GameObject spawnObject = null;

        int count = 0;
        for (int i = 0; i < terrainLength; i++) {
            //Debug.Log(i + " . " + count + " . " + thisLevelAllFloor.Count);
            if (i == 0) {
                //mapCenter = new Vector3((int)Random.Range(0, (mapLimit.x + 1000 - 2) * 2) - (mapLimit.x + 1000 - 2), (int)Random.Range(0, (mapLimit.y + 1000 - 2) * 2) - (mapLimit.y + 1000 - 2), 0);
                mapCenter = new Vector3(1000,1000, 0);
                spawnObject = Instantiate(ThisLevelAllTerrainParts[0], mapCenter, Quaternion.identity); //startpoint
                //spawnObject.GetComponent<groundScript>().type = groundType.startPoint;
            }
            else {
                int randomNumber = Random.Range(0, gameAllTerrainParts.Count);
                spawnObject = Instantiate(gameAllTerrainParts[randomNumber], new Vector3(1000, 1000, 0), Quaternion.identity);
                //spawnObject.transform.Rotate(randomRotation());
                spawnObject.transform.rotation = Quaternion.Euler(0,0, randomRotation() );
                //spawnObject.isStatic = true;
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
            SuperfluousFloorList = new List<GameObject>();
            if (!item.GetComponent<groundScript>().alreadyLink) {
                item.GetComponent<groundScript>().delByMapLimit = true;
                SuperfluousFloorList.Add(item);
            }
        }

        clearSuperfluousFloor();
        addToSystem();

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

    public List<GameObject> SuperfluousFloorList;
    void clearSuperfluousFloor()
    {
        foreach (var item in SuperfluousFloorList)
        {
            thisLevelAllFloor.Remove(item);
            Destroy(item);
        }
    }

    void addToSystem()
    {
        foreach (var item in thisLevelAllFloor)
        {
            if (item.GetComponent<groundScript>() != null)
            {
                roundScript.Static.groundCheckSystem += item.GetComponent<groundScript>().roundSystemUseOnly;
            }
        }

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
