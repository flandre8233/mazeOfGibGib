using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyScript : enemyDataBase {
    npcSensor sensor;
    public bool IsAutoSetType = true;
    public bool killTest = false;

    public Transform playerTransform;

    public bool startLerpMovement = false;
    Vector3 targetPos;
    float startTime;

    public virtual void SetUp(short monsterLevel) {
        
    }

    // Use this for initialization
    void Start () {
        //setItemType();
        Level = 1;
        HP = MaxHP;

        cOIN += (int)(COIN / 100.0f * (Random.Range(0, 40) - 20));
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sensor = GetComponentInChildren<npcSensor>();
        roundScript.Static.roundSystem += enemyAttackPlayerScript;
        roundScript.Static.roundSystem += enemyHPCheck;
        //roundScript.Static.roundSystem += move;
        
    }

    private void OnDestroy()
    {
        
    }

    public void testFunction()
    {
        Debug.Log(Vector3.Distance(chessMovement.Static.transform.position,gameObject.transform.position) );
    }

    public void move()
    {
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0) , 0.25f);
        if (hitColliders.Length <= 0)
        {
            return;
        }

        switch (hitColliders[0].GetComponent<groundScript>().pathdirection)
        {
            case pathDirection.notyet:
                
                targetPos = new Vector3(transform.position.x, transform.position.y, -1);
                break;
            case pathDirection.up:
                targetPos = new Vector3(transform.position.x, transform.position.y+1, -1);
                break;
            case pathDirection.down:
                targetPos = new Vector3(transform.position.x, transform.position.y - 1, -1);
                break;
            case pathDirection.left:
                targetPos = new Vector3(transform.position.x -1, transform.position.y, -1);
                break;
            case pathDirection.right:
                targetPos = new Vector3(transform.position.x +1, transform.position.y, -1);
                break;
            case pathDirection.playerPoint:
                break;
            default:
                break;
        }

        if (Vector3.Distance(chessMovement.Static.hitObjectPosition, targetPos) <= 0.25f)//玩家優先
        {
            targetPos = new Vector3(transform.position.x, transform.position.y, -1); //還原
            return;
        }
        for (int i = 0; i < mapThingsGenerator.Static.allEnemyArray.Count; i++) //處理兩隻怪物之間衝突
        {
            if (i != UID)
            {
                if (Vector3.Distance(mapThingsGenerator.Static.allEnemyArray[i].GetComponent<enemyScript>().targetPos, targetPos) <= 0.25f && mapThingsGenerator.Static.allEnemyArray[i].GetComponent<enemyScript>().startLerpMovement == true)
                {
                    targetPos = new Vector3(transform.position.x, transform.position.y, -1);
                    return;
                }
            }
        }

        Collider[] hitSomethingColliders = Physics.OverlapSphere(targetPos, 0.35f);
        //Debug.Log(chessMovement.Static.hitObjectPosition+"   "+ targetPos + startLerpMovement);


        
        if (hitSomethingColliders.Length >= 0 )
        {
            foreach (var item in hitSomethingColliders)
            {
                if (item.gameObject.tag == "Player" && chessMovement.Static.startLerpMovement == false) //撞到玩家
                {
                    targetPos = new Vector3(transform.position.x, transform.position.y, -1);
                    Debug.Log("ff");
                    return;
                }
                if (item.gameObject.tag == "enemy" && item.GetComponent<enemyScript>().startLerpMovement == false && item.GetComponent<enemyScript>().HP > 0)//撞到另一隻怪物
                {
                    targetPos = new Vector3(transform.position.x, transform.position.y, -1);
                    return;
                }

            }

        }
        

        startLerpMovement = true;
        startTime = Time.time;
    }

    void LerpMove(ref bool isInLerpMovement,Vector3 targetPosition , float startTime,float lerpSpeed)
    {
        if (isInLerpMovement)
        {

            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) * lerpSpeed);
            if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) == 0.0f)
            {
                isInLerpMovement = false;

            }
            else if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) <= 0.1f)
            {
                transform.position = targetPosition;
                //charactor_move.SetBool("run", false);
                //charactor_move.SetBool("idle", true);
            }
            else
            {
                //charactor_move.SetBool("run", true);
                //charactor_move.SetBool("idle", false);
            }

        }
    }

    public int findPlayerRoundNumber = -1;
    //public GameObject damageDisplayObject;

    public void enemyAttackPlayerScript() {
        if (chessMovement.Static.startLerpMovement)
        {
            return;
        }

        if (sensor.isFindPlayer) {
            if (findPlayerRoundNumber < 0) {
                findPlayerRoundNumber = roundScript.Static.round;
            }

            if ( (roundScript.Static.round - findPlayerRoundNumber) % CD == 0) {//是攻擊的回合才行動
                
                if (playerDataBase.Static.DEF <= ATK) {
                    gamemanager.Static.spawnNumberDisplay(chessMovement.Static.gameObject.transform.position, (ATK - playerDataBase.Static.DEF), 5);
                    playerDataBase.Static.HP -= (ATK - playerDataBase.Static.DEF);
                }
            }


        }
        else {
            if (findPlayerRoundNumber >= 0) {
                findPlayerRoundNumber = -1;
            }

        }
    }

    private void Update() {
        allwayFaceAtPlayer();
        LerpMove(ref startLerpMovement, targetPos, startTime,1.5f);
    }

    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to) {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ));
        return rotation;
    }

    public void allwayFaceAtPlayer() {
        float Angle = ImageLookAt2D(transform.position, playerTransform.position).eulerAngles.z;
        transform.rotation = ImageLookAt2D(transform.position, playerTransform.position) ;
        //transform.LookAt(playerTransform);
    }

    public void enemyHPCheck() {
        if (HP <= 0 || killTest) {

            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyAttackPlayerScript;
            //roundScript.Static.roundSystem -= roundScript.Static.enemyList[DataBase.UID].GetComponent<enemyScript>().enemyHPCheck;


            playerDataBase.Static.COIN += (COIN * (playerDataBase.Static.COINBounsPercent / 100));
            delEnemy();
        }

    }

    public void delEnemy()
    {
        mapThingsGenerator.Static.allEnemyArray.Remove(gameObject);
        roundScript.Static.roundSystem -= enemyAttackPlayerScript;
        roundScript.Static.roundSystem -= enemyHPCheck;
        Destroy(gameObject);
    }

}
