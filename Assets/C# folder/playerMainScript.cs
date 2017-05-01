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



    public void checkLife()
    {
        if (roundScript.Static.isExitTouchPlayer)
        {
            return;
        }

        int hpNumber=0;

        if (playerDataBase.Static.SP > 0) {
            if (playerDataBase.Static.HP < playerDataBase.Static.MaxHP) {
                if ((int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 5.0f)) > 1) {
                    hpNumber = (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f)); ;
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
            if (playerDataBase.Static.HP > 0) {
                if ((int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 10.0f)) > 1) {
                    hpNumber =  (int)(Mathf.Round(playerDataBase.Static.MaxHP / 100.0f * 15.0f));
                }
                else {
                    hpNumber = 1;
                }
            }
            playerDataBase.Static.HP -= hpNumber;
            gamemanager.Static.spawnNumberDisplay(transform.position, hpNumber, 5);
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

        //deadAliveCheck();

    }

    public void OnPlayerDead()
    {
        if (playerDataBase.Static.maxFloor < playerDataBase.Static.currentFloor)
        {
            playerDataBase.Static.maxFloor = playerDataBase.Static.currentFloor;
        }

    }

    public void deadAliveCheck()
    {
        /*
        if (playerDataBase.Static.HP <= 0) {
            if (playerDataBase.Static.SP <= 0) {

            }
        }
        */

        if (playerDataBase.Static.HP <= 0)
        {
            //event : hp = 0  gameover
            playerDataBase.Static.HP = 0;
            roundScript.Static.IsDead = true;
            OnPlayerDead();
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


    public void getItem() {
        if (hitItem == null) {
            return;
        }
        playerDataBase.Static.HP += hitItem.gameObject.GetComponent<itemScript>().AddHP;
        playerDataBase.Static.SP += hitItem.gameObject.GetComponent<itemScript>().AddSP;
        playerDataBase.Static.MaxHP += hitItem.gameObject.GetComponent<itemScript>().AddHPMax;
        playerDataBase.Static.MaxSP += hitItem.gameObject.GetComponent<itemScript>().AddSPMax;
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


        itemArrayClone[saveIn] = DeepCopyType(hitItem.GetComponent<itemScript>().itemName, saveIn);
        itemArrayClone[saveIn] = DeepCopyItem(hitItem.gameObject.GetComponent<itemScript>(), saveIn);
        //itemArray[saveIn] = DeepCopyItem(hitItem.gameObject.GetComponent<itemScript>(), saveIn);
        hitItem = null;
    }


    itemScript DeepCopyType(string name, int saveIn)
    {
        switch (name)
        {
            case "SP":
                return gameObject.AddComponent<SP>();

            case "SPNoCost":
                return gameObject.AddComponent<SPNoCost>();

            case "DEFBuff":
                return gameObject.AddComponent<DEFBuff>();

            case "ATKBuff":
                return gameObject.AddComponent<ATKBuff>();
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


        playerDataBase.Static.HP += itemArrayClone[number].AddHP;
        playerDataBase.Static.SP += itemArrayClone[number].AddSP;
        playerDataBase.Static.MaxHP += itemArrayClone[number].AddHPMax;
        playerDataBase.Static.MaxSP += itemArrayClone[number].AddSPMax;
        playerDataBase.Static.COIN += itemArrayClone[number].AddCOIN;

        if (itemArrayClone[number].SPNoCostTime != 0) {
            // spnocost buff item          
            inSPBuffStatus = true;
            StartCoroutine(NoCostSpItem(itemArrayClone[number].SPNoCostTime ) );
        }

        if (itemArrayClone[number].ContinueRound != 0)
        {
            if (itemArrayClone[number].AddATK != 0)
            { // atk buff item
                inATKBuffStatus = ATKBuffSetUp(itemArrayClone[number].ContinueRound, itemArrayClone[number].AddATK);
            }
            else
            { // def buff item
                inDEFBuffStatus = DEFBuffSetUp(itemArrayClone[number].ContinueRound, itemArrayClone[number].AddDEF);
            }

        }

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






    /*
     
        (´◓Д◔`) 哇幹!!
        (っค้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้c )











        (´◓Д◔`) 哇幹!!
        (っค้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้้c )

    */

#region itemBuff Set
    public bool DEFBuffSetUp(int conRound, int DEFAddNumber)
    {
        DEFbuffStartRound = roundScript.Static.round;
        DEFContinueRound = conRound;
        originalDEFNumber = playerDataBase.Static.DEF;
        if (originalDEFNumber > 0)
        {
            playerDataBase.Static.DEF += (int)(playerDataBase.Static.DEF / (100 / DEFAddNumber) );
        }
        return true;
    }

    public bool ATKBuffSetUp(int conRound, int atkAddNumber)
    {
        ATKbuffStartRound = roundScript.Static.round;
        ATKContinueRound = conRound;
        originalATKNumber = playerDataBase.Static.ATK;
        Debug.Log((100.0f / originalATKNumber));
        if (originalATKNumber > 0)
        {
            Debug.Log((100.0f / atkAddNumber));
            playerDataBase.Static.ATK += (int)(playerDataBase.Static.ATK / (100.0f / atkAddNumber) );
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

        playerDataBase.Static.ATK = originalATKNumber;
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

        playerDataBase.Static.DEF = originalDEFNumber;
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

    bool checkIsAlreadyGetItem(string itemName)
    {

        for (int i = 0; i < itemArrayClone.Length; i++)
        {


            if (itemArrayClone[i] != null && itemArrayClone[i].itemName == itemName )
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

            bool itemArrayHaveSpace = false;
            bool alreadyHaveThisItem = checkIsAlreadyGetItem(other.gameObject.GetComponent<itemScript>().itemName);
            chessMovement.Static.charactor_move.SetTrigger("get");
            for (int i = 0; i < itemArrayClone.Length; i++)
            {
                if (itemArrayClone[i] == null && !alreadyHaveThisItem) // this wor) 
                {
                    getItemSet(i);
                    spawnItemIn3DUI(other.gameObject,itemV3[i].transform);
                    itemArrayHaveSpace = true;
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

