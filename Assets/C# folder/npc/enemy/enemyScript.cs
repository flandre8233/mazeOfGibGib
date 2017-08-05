using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyScript : enemyDataBase
{
    public bool IsAutoSetType = true;
    public bool killTest = false;
    public bool IsUnderAttack = false;
    public Transform playerTransform;

    public bool thisRoundCompeleAttack;

    float startTime;

    public bool inDead = false;

    Animator enemyAni;

    public virtual void SetUp() {
        
    }

    // Use this for initialization
    void Start () {
        thisRoundCompeleAttack = true;
        //setItemType();
        //Level = 1;
        SetUp();

        if (HP == 0)
        {
            HP = MaxHP;
        }

        cOIN += (int)(COIN / 100.0f * (Random.Range(0, 40) - 20));
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //sensor = GetComponentInChildren<npcSensor>();
        enemyAni = GetComponentInChildren<Animator>();
        roundScript.Static.roundSystem += resetNumberOfActions;
        roundScript.Static.roundSystem += enemyHPCheck;
        roundScript.Static.enemyAttack += enemyAttackPlayerScript;
        //roundScript.Static.roundSystem += move;

    }

    void followPlane() //用唔到 原因：個lerp吃左 大概要轉用localPos
    {
        if (CenterGround == null)
        {
            return;
        }

        if (CenterGround.GetComponent<Spike>() == null)
        {
            return;
        }

        gameObject.transform.parent = CenterGround.GetComponent<Spike>().planeTransform.transform;

    }

    public void resetNumberOfActions()
    {
        NumberOfActions = 1;
    }
    
    public void move()
    {
        if (HP <= 0)
        {
            return;
        }

        CenterGround = getGround();
        if (CenterGround == null)
        {
            return;
        }


        switch (CenterGround.pathdirection)
        {
            case pathDirection.notyet:
                
                center = new Vector3(center.x, center.y, 0);
                break;
            case pathDirection.up:
                center = new Vector3(center.x, center.y+1, 0);
                break;
            case pathDirection.down:
                center = new Vector3(center.x, center.y - 1, 0);
                break;
            case pathDirection.left:
                center = new Vector3(center.x -1, center.y, 0);
                break;
            case pathDirection.right:
                center = new Vector3(center.x +1, center.y, 0);
                break;
            case pathDirection.playerPoint:
                break;
            default:
                break;
        }

        //Debug.Log(equalVector3(chessMovement.Static.center, center));

        //Vector3.Distance(chessMovement.Static.center, center) <= 0.25f
        if (equalVector3(chessMovement.Static.center,center) )//玩家優先
        {
            center = resetCenterV3(CenterGround);
            return;
        }
        for (int i = 0; i < mapThingsGenerator.Static.allEnemyArray.Count; i++) //處理兩隻怪物之間衝突
        {
            if (i != UID)
            {
                if (equalVector3(mapThingsGenerator.Static.allEnemyArray[i].GetComponent<enemyScript>().center, center) && mapThingsGenerator.Static.allEnemyArray[i].GetComponent<enemyScript>().startLerpMovement == true)
                {
                    center = resetCenterV3(CenterGround);
                    return;
                }
            }
        }

        Collider[] hitSomethingColliders = Physics.OverlapSphere(new Vector3(center.x,center.y,-1) , 0.35f);
        //Debug.Log("ddd");


        
        if (hitSomethingColliders.Length >= 0 )
        {
            foreach (var item in hitSomethingColliders)
            {
                if (item.gameObject.tag == "Player" && chessMovement.Static.startLerpMovement == false) //撞到玩家
                {
                    center = resetCenterV3(CenterGround);
                    //Debug.Log("ff");
                    return;
                }
                if (item.gameObject.tag == "enemy" && item.GetComponent<enemyScript>().startLerpMovement == false && item.GetComponent<enemyScript>().HP > 0)//撞到另一隻怪物
                {
                    center = resetCenterV3(CenterGround);
                    return;
                }

            }

        }

        soundEffectManager.staticSoundEffect.play_monster_move();
        startLerpMovement = true;
        NumberOfActions--;
        startTime = Time.time;

    }

    void LerpMove(ref bool isInLerpMovement,Vector3 centerition , float startTime,float lerpSpeed)
    {
        if (HP <= 0)
        {
            return;
        }

        if (isInLerpMovement)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, centerition));
            enemyAni.SetFloat("runFloat", distance);

            transform.position = Vector3.Lerp(transform.position, centerition, (Time.time - startTime) * lerpSpeed);
            if (distance == 0.0f)
            {
                isInLerpMovement = false;
                enemyAni.SetFloat("runFloat", 0.0f);
            }
            else if (distance <= 0.1f)
            {
                transform.position = centerition;
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

    public bool checkPlayerCenterIsInAttackPoint()
    {
        if (HP <= 0)
        {
            return false;
        }

        Vector3[] checkPlayerPointArray = {
            new Vector3(center.x+1 ,center.y ,0),
            new Vector3(center.x-1 ,center.y ,0),
            new Vector3(center.x ,center.y+1 ,0),
            new Vector3(center.x ,center.y-1 ,0)
        };

        foreach (var item in checkPlayerPointArray)
        {

            ////Debug.Log(Mathf.Abs(Vector3.Distance(new Vector3(enemyPoint.x, enemyPoint.y, 0), chessMovement.Static.center))  );
            if (equalVector3(item, chessMovement.Static.center))
            {

                return true;
            }
        }

        return false;
    }

    public virtual void enemyAttackPlayerScript() {
        if (NumberOfActions <= 0)
        {
            return;
        }

        if (!checkPlayerCenterIsInAttackPoint())
        {
            return;
        }

        if (!IsUnderAttack)
        {
            return;
        }
        thisRoundCompeleAttack = false;

        IsUnderAttack = false;

        if (findPlayerRoundNumber < 0) {
                findPlayerRoundNumber = roundScript.Static.round;
            }

        if ((roundScript.Static.round - findPlayerRoundNumber) % CD == 0)
        {//是攻擊的回合才行動 attack
            attackFunction();
            return;
        }
        else
        {
            if (findPlayerRoundNumber >= 0)
            {
                findPlayerRoundNumber = -1;
            }

        }
        thisRoundCompeleAttack = true;
    }

    public void attackFunction()
    {
        enemyAni.SetTrigger("attack");
        roundScript.Static.enemyAttackAniProcessingChecker = true;
        StartCoroutine(AnimationBuffZone("attackAni"));

        switch (Level) // on atk sound
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                soundEffectManager.staticSoundEffect.play_monster4_attack();
                break;
        }
    }

    public void enemyAttack()
    {
        playerMainScript.Static.playerTakeDamge(ATK,false);

    }

    public void NpcTakeDamage(int damage)
    {


        //touch Enemy之後既行動
        gamemanager.Static.spawnNumberDisplay(transform.position, damage, 0);
        GetComponent<enemyDataBase>().HP -= damage;
        GetComponent<enemyScript>().enemyHPCheck();


        //roundScript.Static.enemyAttackAniProcessingChecker = true;
    }

    private void Update() {
        allwayFaceAtPlayer();
        LerpMove(ref startLerpMovement, new Vector3(center.x, center.y,-1) , startTime,0.75f);
    }

    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to) {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ));
        return rotation;
    }

    public void allwayFaceAtPlayer() {
        transform.rotation = ImageLookAt2D(transform.position, playerTransform.position) ;
        //transform.LookAt(playerTransform);
    }

    public void enemyHPCheck() {
        if (inDead)
        {
            return;
        }

        if (HP <= 0 || killTest) {
            //playerDataBase.Static.COIN += (int)(COIN * (playerDataBase.Static.COINBounsPercent / 100));
            playerDataBase.Static.COIN += (int)COIN;
            inDead = true;

            switch (Level) // on dead sound
            {
                case 1:
                    soundEffectManager.staticSoundEffect.play_monster1_dead();
                    break;
                case 2:
                    soundEffectManager.staticSoundEffect.play_monster2_dead();
                    break;
                case 3:
                    soundEffectManager.staticSoundEffect.play_monster3_dead();
                    break;
                case 4:
                    soundEffectManager.staticSoundEffect.play_monster4_dead();
                    break;
            }
            enemyAni.SetTrigger("died");
            gameObject.tag = "Untagged";
            //delEnemy();
        }
    }

    public void delEnemy()
    {
        mapThingsGenerator.Static.allEnemyArray.Remove(gameObject);
        roundScript.Static.roundSystem -= resetNumberOfActions;
        roundScript.Static.roundSystem -= enemyHPCheck;
        roundScript.Static.enemyAttack -= enemyAttackPlayerScript;

        if (playerTargetDisplay.Static.targetObject == gameObject.GetComponent<enemyDataBase>())
        {
            playerTargetDisplay.Static.disableAllTargetDisplay();
        }

        Destroy(gameObject);
    }

    private IEnumerator AnimationBuffZone(string animationTag)
    {
        do
        {
            yield return null;
        } while (!enemyAni.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        StartCoroutine(WaitForAnimation(animationTag));
        //dead here
    }

    private IEnumerator WaitForAnimation(string animationTag)
    {
        do
        {
            yield return null;
        } while (enemyAni.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        thisRoundCompeleAttack = true;
        roundScript.Static.checkALLEnemyIsCompleteAttack();
        //dead here
    }

}
