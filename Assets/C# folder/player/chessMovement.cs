﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessMovement : GeneralMovementSystem
{
    public static chessMovement Static;
    public Animator charactor_move;
    public GameObject model;
    public GameObject damageDisplayObject;

    private float startTime;

    public float downTime = 0;
    public float countDown = 0.5f;
    public string faceDirection = "up";

    public bool thisFrameMoved = false;
    bool isHitNpc = false;
    //bool StartedOnce = false;
    public GameObject touchEnemy = null;

    [Range(0, 5)]
    public float lerpSpeed = 1;
    float normalLerpSpeed;
    // Use this for initialization
    void Start()
    {
        charactor_move = GetComponentInChildren<Animator>();
        normalLerpSpeed = lerpSpeed;
        Static = this;

    }

    // Update is called once per frame

    void Update()
    {


        LerpMove(ref startLerpMovement, hitObjectPosition,startTime,lerpSpeed);
        if (thisFrameMoved && !roundScript.Static.IsProcessingRound )
        {
            if (isHitNpc)
            { //這步會打中npc的話
                if (playerMainScript.Static.inSPBuffStatus)
                {
                    roundScript.Static.pastRound();
                }
                else
                {
                    roundScript.Static.pastRound();

                    // roundScript.Static.roundSystem -= playerMainScript.Static.subSP; //<--攻擊時唔扣SP部份
                    //roundScript.Static.pastRound();
                    //roundScript.Static.roundSystem += playerMainScript.Static.subSP;
                }

            }
            else
            {//不是要打npc
                //charactor_move.Play("run", -1, 0f) );
                roundScript.Static.pastRound();
            }
            isHitNpc = false;
            thisFrameMoved = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            roundScript.Static.pastRound();
        }

        movementInput(ref faceDirection);//確定面對方向
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            MovementPart(faceDirection);
        }

        // w係+ s係-   a係-  d係+        
        //idling();
    }
    /*void idling()
    {
        player_idle_check();
    }
    */

    /*
    void reset()
    {
        center = Vector3.zero;
        if (moveCheck())
        {
            movePlayer();
        }
        thisFrameMoved = false;
    }
    */
    //一開頭設定
    void movementInput(ref string DefaultDirection)
    {

        /*
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.A)) {
            return "up/left";
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.D)) {
            return "up/right";
        }

        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.A)) {
            return "down/left";
        }
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            return "down/right";
        }

        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.W)) {
            return "up/left";
        }
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.S)) {
            return "down/left";
        }

        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.W)) {
            return "up/right";
        }
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.S)) {
            return "down/right";
        }
        */


        if (Input.GetKeyDown(KeyCode.W))
        {
            DefaultDirection = "up";

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DefaultDirection = "down";
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            DefaultDirection = "left";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DefaultDirection = "right";
        }

    }//找出鍵盤輸入的方位是什麼 並string化輸入數值
    public void MovementPart(string moveDirection)
    {
        if (roundScript.Static.IsProcessingRound)
        {
            return;
        }

        if (roundScript.Static.IsDead)
        {
            return;
        }

            switch (moveDirection)
            {
                default:
                    break;

                case "up":
                    center = new Vector3(center.x + 0, center.y + 1, 0); //W
                    model.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case "down":
                    center = new Vector3(center.x + 0, center.y - 1, 0); //S
                    model.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "left":
                    center = new Vector3(center.x - 1, center.y + 0, 0); //A
                    model.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case "right":
                    center = new Vector3(center.x + 1, center.y + 0, 0);
                    model.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                case "up/left":
                    center = new Vector3(center.x - 1, center.y + 1, 0);
                    break;
                case "up/right":
                    center = new Vector3(center.x + 1, center.y + 1, 0);
                    break;
                case "down/left":
                    center = new Vector3(center.x - 1, center.y - 1, 0);
                    break;
                case "down/right":
                    center = new Vector3(center.x + 1, center.y - 1, 0);
                    break;
            }

        if (moveCheck())
        {
            movePlayer();
        }
        

    }//把已string化的鍵盤方位數值解碼，指揮檢查用vector3先去鍵盤要求前住的那一格方位

    public bool isInAutoMovement = false;

    public void autoMovement(float time, string c)
    {

        if (time >= countDown)
        {
            //Debug.Log("autoMovement");
            MovementPart(c);

        }
    }//自動重復執行MovementPart

    GameObject TouchChest = null;

    void movePlayer()
    { //真正移動
        if (roundScript.Static.IsProcessingRound)
        {
            return;
        }
            roundScript.Static.IsProcessingRound = true;


        if (!roundScript.Static.DoAttackAniProcessingChecker  && TouchChest != null)
        {

            charactor_move.SetTrigger("attack");
            charactor_move.SetInteger("attack_no.", Random.Range(0, 4));
            roundScript.Static.DoAttackAniProcessingChecker = true;

            StartCoroutine(WaitForAnimationForChest("attackAni"));

            return;
        }
            
                if (touchEnemy != null)
            {


            Debug.Log("lksai fuck u jgfhlkajfhs     dlkjh   " + roundScript.Static.DoAttackAniProcessingChecker);
            if (!roundScript.Static.DoAttackAniProcessingChecker && touchEnemy.GetComponent<enemyDataBase>().HP > 0)
                {
                    charactor_move.SetTrigger("attack");
                    charactor_move.SetInteger("attack_no.", Random.Range(0, 4));
                    roundScript.Static.DoAttackAniProcessingChecker = true;
                    StartCoroutine(AnimationBuffZone("attackAni"));
                }



                startLerpMovement = false;
            }
            else
            {
                startLerpMovement = true;
                roundScript.Static.movementProcessingChecker = true;
                startTime = Time.time;
                thisFrameMoved = true;
            }
        
    }

    void LerpMove(ref bool isInLerpMovement, Vector3 targetPosition, float startTime, float lerpSpeed)
    {
        if (isInLerpMovement)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) * lerpSpeed);
            float movementDistance = Mathf.Abs(Vector3.Distance(transform.position, targetPosition) ) ;
            charactor_move.SetFloat("movementFloat", movementDistance);
            if (movementDistance  == 0.0f)
            {
                isInLerpMovement = false;
                Debug.Log("passhere");



                lerpSpeed = normalLerpSpeed;
            }
            else if (movementDistance <= 0.15f)
            {

                roundScript.Static.movementProcessingChecker = false;
            }

        }
        else
        {
            charactor_move.SetFloat("movementFloat", 0.0f);
        }
    }

    public Vector3 hitObjectPosition = new Vector3();

    public void returnToBeforeCheckPoint() {
        if (playerDataBase.Static.currentFloor - roundScript.Static.checkPoint < 0) {
            playerDataBase.Static.currentFloor = 0;
        }
        else {
            playerDataBase.Static.currentFloor -= 11;
        }
        roundScript.Static.OnEnterNextLevel();
    }


    public bool moveCheck()
    { //正確是否正確移動
        Collider[] hitColliders = Physics.OverlapSphere(center, 0.25f);
        Collider[] hitEnemyColliders = Physics.OverlapSphere(new Vector3(center.x, center.y, -1), 0.35f);
        touchEnemy = null;
        TouchChest = null;
        if (hitColliders.Length != 0)
        {
            CenterGround = hitColliders[0].gameObject.GetComponent<groundScript>();
            hitObjectPosition = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1);

            if (hitColliders[0].gameObject.tag == "returnCheckPoint") {
                returnToBeforeCheckPoint();
            }
        }
        if (hitColliders.Length >= 1)
        {
            if (hitEnemyColliders.Length >= 1)
            {
                foreach (var item in hitEnemyColliders)
                {
                    if (item.tag == "enemy")
                    {
                        touchEnemy = item.gameObject;
                        touchEnemy.GetComponent<enemyScript>().IsUnderAttack = true;

                        hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), 0.25f); //還原center
                        CenterGround = hitColliders[0].gameObject.GetComponent<groundScript>();
                        hitObjectPosition = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1);

                        center = resetCenterV3(CenterGround);
                        return true;
                    }
                    if (item.tag == "chest")
                    {
                        TouchChest = item.gameObject;

                        hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), 0.25f); //還原center
                        CenterGround = hitColliders[0].gameObject.GetComponent<groundScript>();
                        hitObjectPosition = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1);
                        
                        center = resetCenterV3(CenterGround);
                        return true; // maybe not work
                    }
                    if (item.tag == "crystal")
                    {
                        revive_script.Static.yn_show.gameObject.SetActive(!revive_script.Static.yn_show.gameObject.activeSelf);

                        hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), 0.25f); //還原center
                        CenterGround = hitColliders[0].gameObject.GetComponent<groundScript>();
                        hitObjectPosition = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1);

                        center = resetCenterV3(CenterGround);
                        return false; // maybe not work
                    }
                }
                return true; // is Hititem
            }
            return true;
        }
        center = resetCenterV3(CenterGround);
        return false;
    }//檢查 檢查用vector3 目前所在的方位是否存在方塊(已是說是否有路) 有就移動 無就取消移動動作

    private IEnumerator AnimationBuffZone(string animationTag)
    {
        do
        {
            yield return null;
        } while (!charactor_move.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        StartCoroutine(WaitForAnimation(animationTag));
        //dead here
    }

    private IEnumerator AnimationBuffZone2(string animationTag)
    {
        do
        {
            yield return null;
        } while (!charactor_move.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        StartCoroutine(AnimationBuffZone2(animationTag));
        //dead here
    }

    private IEnumerator WaitForAnimation(string animationTag)
    {
        do
        {

            yield return null;
        } while (charactor_move.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        roundScript.Static.DoAttackAniProcessingChecker = false;
        //dead here
    }

    private IEnumerator WaitForAnimationForChest(string animationTag)
    {
        do
        {
            yield return null;
        } while (charactor_move.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));

        TouchChest.GetComponent<box>().allwayFaceAtPlayer();
        TouchChest.GetComponent<box>().openChest();
        roundScript.Static.DoAttackAniProcessingChecker = false;
        thisFrameMoved = true;
        //dead here
    }



    public void attackNpc(GameObject touchEnemy)
    {
        if (touchEnemy == null)
        {
            return;
        }

        //touch Enemy之後既行動
        gamemanager.Static.spawnNumberDisplay(touchEnemy.transform.position, playerDataBase.Static.ATK, 0);
        touchEnemy.GetComponent<enemyDataBase>().HP -= playerDataBase.Static.ATK;
        touchEnemy.GetComponent<enemyScript>().enemyHPCheck();

        isHitNpc = true;
        thisFrameMoved = true;

    }
}


