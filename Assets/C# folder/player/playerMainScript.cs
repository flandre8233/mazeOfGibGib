using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMainScript : MonoBehaviour
{
    public static playerMainScript Static;

    public bool inATKBuffStatus;
    public bool inDEFBuffStatus;
    public bool inSPBuffStatus;

    //public itemScript[] itemArray = new itemScript[2] { null, null };
    // public itemScript[] itemArray = new itemScript[2];
    public itemScript[] itemArrayClone = new itemScript[2];

    public GameObject[] itemV3;

    int ATKContinueRound = 3;
    int DEFContinueRound = 3;
    int ATKbuffStartRound = 0;
    int DEFbuffStartRound = 0;
    int originalATKNumber = 0;
    int originalDEFNumber = 0;

    // Use this for initialization
    void Awake()
    {
        Static = this;
    }

    private void Start()
    {

    }

    public void loadPlayerItem()
    {
        if (gamemanager.Static.beLoaded)
        {
            for (int i = 0; i < testSaveLoad.Static.mydata.playerItem.Length; i++) //好白痴
            {
                if (testSaveLoad.Static.mydata.playerItem[i] == null)
                {
                    break;
                }
                saveGameData.item item = testSaveLoad.Static.mydata.playerItem[i];
                Vector3 spawnPos = new Vector3(0, 0, -1);
                //selectType

                GameObject InstantiateItem = Instantiate(mapThingsGenerator.Static.selectType(item.type), spawnPos, Quaternion.Euler(-90, 0, 0));
                Debug.Log("kkkk" + item.level );
                InstantiateItem.GetComponent<itemScript>().level = item.level;
                hitItem = InstantiateItem;

                getItemSet(i);
                spawnItemIn3DUI(InstantiateItem, itemV3[i].transform);

                Destroy(InstantiateItem);
            }

        }
    }

    public void subSP()
    {
        if (roundScript.Static.isEnterCheckPoint() ) {
            return;
        }

        if (playerDataBase.Static.SP > 0 && !roundScript.Static.isExitTouchPlayer)
        {
            playerDataBase.Static.SP--;
        }
    }

    public shrinkManager UIShrink;
    public void playerTakeDamge(int damage , bool ignoreDEF)
    {
        int DEF = 0;
        if (ignoreDEF)
        {
            DEF = 0;
        }
        else
        {
            DEF = playerDataBase.Static.DEF;
        }



        if (DEF < damage)
        {
            gamemanager.Static.spawnNumberDisplay(chessMovement.Static.gameObject.transform.position, (damage - DEF), 5);
            UIShrink.startShrink();
            UIShrink.strong = (int)(350 *  ( 1.2f * ( playerDataBase.Static.HP +  damage)  / playerDataBase.Static.HP) );
            UIShrink.lerpSpeed =  (15 * ( 1.5f * ( playerDataBase.Static.HP +  damage) / playerDataBase.Static.HP) ); 
            playerDataBase.Static.HP -= (damage - DEF);
            deadAliveCheck();
        }
        else
        {
            gamemanager.Static.spawnNumberDisplay(chessMovement.Static.gameObject.transform.position, 0, 5);
        }
    }

    public GameObject closeDeadWarning;

    public void checkLife()
    {
        if (roundScript.Static.isExitTouchPlayer)
        {
            return;
        }

        bool hpHasChanged = false;
        int hpNumber=0;

        if (playerDataBase.Static.SP > 0) {
            if (playerDataBase.Static.HP < playerDataBase.Static.MaxHP)
            {
                Instantiate(particleManager.Static.character_run_inside01_heal, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.015f), new Quaternion(-180, 0, 0, 0) ); //粒子
                hpHasChanged = true;
                if ((int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 5.0f)) > 1)
                {
                    hpNumber = (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f));
                }
                else {
                    hpNumber = 1;
                }
            }
            playerDataBase.Static.HP += hpNumber ;
            if (hpNumber > 0)
            {
                gamemanager.Static.spawnNumberDisplay(transform.position, hpNumber, 3);
            }
        }
        else {
            if (playerDataBase.Static.HP > 0)
            {
                Instantiate(particleManager.Static.character_run_inside01_hurt, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.015f), new Quaternion(-180,0,0,0) );  //粒子
                hpHasChanged = true;
                if ((int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f)) > 1)
                {
                    hpNumber =  (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 15.0f));
                }
                else {
                    hpNumber = 1;
                }
            }
            playerDataBase.Static.HP -= hpNumber;
            gamemanager.Static.spawnNumberDisplay(transform.position, hpNumber, 5);
        }


        if (!hpHasChanged)
        {
            Instantiate(particleManager.Static.character_run_inside01_normal,new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.015f), new Quaternion(-180, 0, 0, 0) ); //粒子
        }


        if (playerDataBase.Static.HP > playerDataBase.Static.MaxHP) {
            playerDataBase.Static.HP = playerDataBase.Static.MaxHP;
        }
        else if (playerDataBase.Static.HP < 0) {
            playerDataBase.Static.HP = 0;
        }

        if (playerDataBase.Static.SP > playerDataBase.Static.MaxSP) {
            playerDataBase.Static.SP = playerDataBase.Static.MaxSP;
        }
        else if (playerDataBase.Static.SP < 0) {
            playerDataBase.Static.SP = 0;
        }

        Debug.Log("llkpkp");
        Debug.Log(playerDataBase.Static.MaxHP / 100.0f * 25.0f);

        Debug.Log(playerDataBase.Static.HP);
        displayCloseDeadWarning();

        //deadAliveCheck();

    }

    public void displayCloseDeadWarning()
    {
        if (playerDataBase.Static.HP <= playerDataBase.Static.MaxHP / 100.0f * 25.0f)
        {
            closeDeadWarning.GetComponent<ParticleSystem>().loop = true;
            closeDeadWarning.SetActive(true);
        }
        else
        {
            closeDeadWarning.GetComponent<ParticleSystem>().loop = false;
        }
    }

     void checkMaxFloor()
    {
        //currentLifeMaxFloor
        if (playerDataBase.Static.maxFloor < playerDataBase.Static.currentFloor)
        {
            playerDataBase.Static.maxFloor = playerDataBase.Static.currentFloor;
        }
    }

    public void deadAliveCheck()
    {



        if (playerDataBase.Static.HP <= 0)
        {
            if (playerDataBase.Static.revive_value)
            {
                revive();
                return;
            }
            //event : hp = 0  gameover
            playerDataBase.Static.HP = 0;
            roundScript.Static.IsDead = true;
            //saveLoadManager.clearSave();
            testSaveLoad.Static.mydata.define = false;
            saveLoadManager.Save(testSaveLoad.Static.mydata);
            checkMaxFloor();
            //GetComponent<chessMovement>().enabled = false; //youdead
        }
        else
        {
            roundScript.Static.IsDead = false;
            //GetComponent<chessMovement>().enabled = true;
        }

        if (playerDataBase.Static.SP == 0)
        {
            //event : sp = 0
        }
    }

    void revive()
    {
        playerDataBase.Static.revive_value = false;
        int sendPlayerBackLevel;
        sendPlayerBackLevel = playerDataBase.Static.currentFloor - (playerDataBase.Static.currentFloor % 10);
        playerDataBase.Static.currentFloor = sendPlayerBackLevel - 1;
        roundScript.Static.OnEnterNextLevel();
        roundScript.Static.IsDead = false;
        chessMovement.Static.charactor_move.Play("idle", 0);

        roundScript.Static.movementProcessingChecker = false;
        roundScript.Static.DoAttackAniProcessingChecker = false;
        roundScript.Static.IsOpeningChest = false;
        roundScript.Static.enemyAttackAniProcessingChecker = false;

    }

    public void getItem() {
        if (hitItem == null) {
            return;
        }
        playerDataBase.Static.HP += hitItem.gameObject.GetComponent<itemScript>().AddHP;
        playerDataBase.Static.SP += hitItem.gameObject.GetComponent<itemScript>().AddSP;
        playerDataBase.Static.HPBuff += hitItem.gameObject.GetComponent<itemScript>().AddHPMax;
        playerDataBase.Static.SPBuff += hitItem.gameObject.GetComponent<itemScript>().AddSPMax;
        playerDataBase.Static.COIN += hitItem.gameObject.GetComponent<itemScript>().AddCOIN;

        if (playerDataBase.Static.HP >= playerDataBase.Static.MaxHP) { //max check
            playerDataBase.Static.HP = playerDataBase.Static.MaxHP;
        }
        if (playerDataBase.Static.SP >= playerDataBase.Static.MaxSP) {
            playerDataBase.Static.SP = playerDataBase.Static.MaxSP;
        }

        if (hitItem.gameObject.GetComponent<itemScript>().AddHP > 0) {
            gamemanager.Static.spawnNumberDisplay(transform.position, hitItem.gameObject.GetComponent<itemScript>().AddHP, 3);
        }

        if (hitItem.gameObject.GetComponent<itemScript>().AddSP > 0) {
            gamemanager.Static.spawnNumberDisplay(transform.position, hitItem.gameObject.GetComponent<itemScript>().AddSP, 3);
        }

        Destroy(hitItem);
        hitItem = null;
    }

    




    public void getItemSet(int saveIn)
    {
        if (hitItem == null)
        {
            return;
        }


        itemArrayClone[saveIn] = DeepCopyType(hitItem.GetComponent<itemScript>().type, saveIn);
        itemArrayClone[saveIn] = DeepCopyItem(hitItem.gameObject.GetComponent<itemScript>(), saveIn);
        //itemArray[saveIn] = DeepCopyItem(hitItem.gameObject.GetComponent<itemScript>(), saveIn);
        hitItem = null;
    }


    itemScript DeepCopyType(itemType name, int saveIn)
    {
        switch (name)
        {
            case itemType.HP:
                return gameObject.AddComponent<HP>(); //?
            case itemType.SP:
                return gameObject.AddComponent<SP>();
            case itemType.SPNoCost:
                return gameObject.AddComponent<SPNoCost>();
            case itemType.ATK:
                return gameObject.AddComponent<ATKBuff>();
            case itemType.DEF:
                return gameObject.AddComponent<DEFBuff>();
            case itemType.HPMAX:
                break;
            case itemType.SPMAX:
                break;
            default:
                break;
        }
        return gameObject.AddComponent<SP>();
    }

    itemScript DeepCopyItem(itemScript original, int i)
    {
        itemScript clone = itemArrayClone[i];

        clone.level = original.level;
        //clone.SetUp();
        //clone.includeLevelSetUp();
        
        /*
        clone.itemName = original.itemName;
        clone.AddHP = original.AddHP;
        clone.AddSP = original.AddSP;
        clone.AddHPMax = original.AddHPMax;
        clone.AddSPMax = original.AddSPMax;
        clone.AddCOIN = original.AddCOIN;
        clone.AddATK = original.AddATK;
        clone.AddDEF = original.AddDEF;
        clone.ContinueRound = original.ContinueRound;
        clone.SPNoCostTime = original.SPNoCostTime;
        */

        return clone;
    }



    public void useItem(int number)
    {
        if (number > 1 || number < 0)
        {

            return;
        }

        //itemArray.Add(hitItem.gameObject.GetComponent<itemScript>()); // this work
        switch (itemArrayClone[number].type)
        {
            case itemType.HP:
                Instantiate(particleManager.Static.character_run_inside01_heal, gameObject.transform); //粒子
                break;
            case itemType.SP:
                Instantiate(particleManager.Static.character_item_food, gameObject.transform); //粒子
                break;
            case itemType.SPNoCost:
                Instantiate(particleManager.Static.character_item_foodtime, gameObject.transform); //粒子
                inSPBuffStatus = true;
                StartCoroutine(NoCostSpItem(itemArrayClone[number].SPNoCostTime));
                break;
            case itemType.ATK:
                Instantiate(particleManager.Static.character_item_attack, gameObject.transform); //粒子
                inATKBuffStatus = ATKBuffSetUp(itemArrayClone[number].ContinueRound, itemArrayClone[number].AddATK);
                break;
            case itemType.DEF:
                Instantiate(particleManager.Static.character_item_defense, gameObject.transform); //粒子
                inDEFBuffStatus = DEFBuffSetUp(itemArrayClone[number].ContinueRound, itemArrayClone[number].AddDEF);
                break;
            default:
                break;
        }

        playerDataBase.Static.HP += itemArrayClone[number].AddHP;
        playerDataBase.Static.SP += itemArrayClone[number].AddSP;
        playerDataBase.Static.HPBuff += itemArrayClone[number].AddHPMax;
        playerDataBase.Static.SPBuff += itemArrayClone[number].AddSPMax;
        playerDataBase.Static.COIN += itemArrayClone[number].AddCOIN;

        if (itemArrayClone[number].AddHP > 0) {
            gamemanager.Static.spawnNumberDisplay(transform.position, itemArrayClone[number].AddHP, 3);
        }

        if (itemArrayClone[number].AddSP > 0) {
            gamemanager.Static.spawnNumberDisplay(transform.position, itemArrayClone[number].AddSP, 3);
        }


        if (playerDataBase.Static.HP >= playerDataBase.Static.MaxHP)
        { //max check
            playerDataBase.Static.HP = playerDataBase.Static.MaxHP;
        }
        if (playerDataBase.Static.SP >= playerDataBase.Static.MaxSP)
        {
            playerDataBase.Static.SP = playerDataBase.Static.MaxSP;
        }
        //itemArrayClone[number] = null;
        Destroy(itemArrayClone[number] );
        itemArrayClone[number] = null;
        Destroy(itemV3[number].GetComponentInChildren<Animator>().gameObject) ; // <- item destroy in 3d ui
    }

    public void useItemMaxOnly(itemScript item)
    {
        playerDataBase.Static.HPBuff += item.AddHPMax;
        playerDataBase.Static.SPBuff += item.AddSPMax;
        Destroy(item.gameObject);
    }




    /*
     
        (´◓Д◔`) 哇幹!!
        (っค้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้c )











        (´◓Д◔`) 哇幹!!
        (っค้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้c )

    */

    #region itemBuff Set
    public bool DEFBuffSetUp(int conRound, int DEFAddNumber)
    {
        Debug.Log(playerDataBase.Static.DEFBuff);
        DEFbuffStartRound = roundScript.Static.round;
        DEFContinueRound = conRound;
        if (playerDataBase.Static.DEFBuff <= 0)
        {
            playerDataBase.Static.DEFBuff += (int)((playerDataBase.Static.DEF / 100.0f) * DEFAddNumber ) + 1;
            Debug.Log(playerDataBase.Static.DEFBuff + "def" + playerDataBase.Static.DEF);
        }
        return true;
    }

    public bool ATKBuffSetUp(int conRound, int atkAddNumber)
    {
        Debug.Log(playerDataBase.Static.ATKBuff);
        ATKbuffStartRound = roundScript.Static.round;
        ATKContinueRound = conRound;
        if (playerDataBase.Static.ATKBuff <= 0)
        {
            playerDataBase.Static.ATKBuff += (int)((playerDataBase.Static.ATK / 100.0f ) * atkAddNumber ) + 1;
            Debug.Log(playerDataBase.Static.ATKBuff + "atk" + playerDataBase.Static.ATK);
        }
        return true;
    }

    public bool ATKBuff()
    {
        if (!inATKBuffStatus)
        {
            return false;
        }

        if (roundScript.Static.round - ATKbuffStartRound < ATKContinueRound)
        { // ok 
            //per frame
            return true;
        }

        playerDataBase.Static.ATKBuff = 0;
        return false;
    }
    public bool DEFBuff()
    {
        if (!inDEFBuffStatus)
        {
            return false;
        }

        if (roundScript.Static.round - DEFbuffStartRound < DEFContinueRound)
        { // ok 
            //per frame
            return true;
        }

        playerDataBase.Static.DEFBuff = 0;
        return false;
    }

    private IEnumerator NoCostSpItem(int waitTime)
    {
        roundScript.Static.roundSystem -= subSP;
        yield return new WaitForSeconds(waitTime);
        inSPBuffStatus = false;
        roundScript.Static.roundSystem += subSP;

    }



    #endregion

    bool checkIsAlreadyGetItem(itemType itemName)
    {

        for (int i = 0; i < itemArrayClone.Length; i++)
        {


            if (itemArrayClone[i] != null && itemArrayClone[i].type == itemName )
            {
                levelUpItem(itemArrayClone[i]);
                itemV3[i].GetComponentInChildren<Animator>().SetTrigger("get");
                return true;
            }
        }

            
        return false;
    }

    void levelUpItem(itemScript item)
    {
        item.level++;
    }

    void spawnItemIn3DUI(GameObject item,Transform UI3DitemPos)
    {
        GameObject itemObject = Instantiate(item.gameObject, UI3DitemPos );
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        itemObject.GetComponentInChildren<Animator>().SetTrigger("get");
        itemObject.tag = "3DUI";
    }

    public GameObject hitItem;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        { //hit item
            hitItem = other.gameObject;

            if (hitItem.GetComponent<itemScript>().type == itemType.HPMAX || hitItem.GetComponent<itemScript>().type == itemType.SPMAX) //果實
            {
                if (hitItem.GetComponent<itemScript>().type == itemType.HPMAX)
                {
                    Instantiate(particleManager.Static.Hpmax_up, gameObject.transform);
                }
                else
                {
                    Instantiate(particleManager.Static.Spmax_up, gameObject.transform);
                }
                useItemMaxOnly(hitItem.GetComponent<itemScript>());
                chessMovement.Static.charactor_move.SetTrigger("get");
                return;
            }


            bool alreadyHaveThisItem = checkIsAlreadyGetItem(other.gameObject.GetComponent<itemScript>().type);
            chessMovement.Static.charactor_move.SetTrigger("get");
            for (int i = 0; i < itemArrayClone.Length; i++)
            {
                if (itemArrayClone[i] == null && !alreadyHaveThisItem) // this wor) 
                {
                    getItemSet(i);
                    spawnItemIn3DUI(other.gameObject,itemV3[i].transform);
                    break;
                }
            }

            /*
            if (!itemArrayHaveSpace) {  //滿包都可以用設定
                getItem();
            }
            */
            
            Destroy(other.gameObject);
            
        }


    }
}

