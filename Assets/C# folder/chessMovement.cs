using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessMovement : MonoBehaviour
{
    public static chessMovement Static;
    public Animator charactor_move;
    public Vector3 center;
    public GameObject model;

    private float startTime;
    public bool startLerpMovement = false;

    public float downTime = 0;
    public float countDown = 0.5f;
    public string faceDirection = "up";

    public bool thisFrameMoved = false;
    bool isHitNpc = false;
    //bool StartedOnce = false;
    GameObject touchEnemy = null;

    [Range(0, 5)]
    public float lerpSpeed = 1;
    float normalLerpSpeed;
    // Use this for initialization
    void Start()
    {
        charactor_move = GetComponentInChildren<Animator>();
        normalLerpSpeed = lerpSpeed;
        Static = this;
        reset();
    }

    // Update is called once per frame

    void Update()
    {
        LerpMove();
        if (thisFrameMoved)
        {
            if (isHitNpc)
            { //這步會打中npc的話
                roundScript.Static.roundSystem -= playerMainScript.Static.subSP;
                roundScript.Static.pastRound();
                roundScript.Static.roundSystem += playerMainScript.Static.subSP;
                roundScript.Static.DoAttackAniProcessingChecker = false;
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
    }


    void reset()
    {
        center = Vector3.zero;
        if (moveCheck())
        {
            movePlayer();
        }
        thisFrameMoved = false;
    }//一開頭設定
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

        if (!roundScript.Static.isProcessingRound)
        {
            switch (moveDirection)
            {
                default:
                    break;

                case "up":
                    center = new Vector3(transform.position.x + 0, transform.position.y + 1, 0); //W
                    model.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case "down":
                    center = new Vector3(transform.position.x + 0, transform.position.y - 1, 0); //S
                    model.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "left":
                    center = new Vector3(transform.position.x - 1, transform.position.y + 0, 0); //A
                    model.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case "right":
                    center = new Vector3(transform.position.x + 1, transform.position.y + 0, 0);
                    model.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                case "up/left":
                    center = new Vector3(transform.position.x - 1, transform.position.y + 1, 0);
                    break;
                case "up/right":
                    center = new Vector3(transform.position.x + 1, transform.position.y + 1, 0);
                    break;
                case "down/left":
                    center = new Vector3(transform.position.x - 1, transform.position.y - 1, 0);
                    break;
                case "down/right":
                    center = new Vector3(transform.position.x + 1, transform.position.y - 1, 0);
                    break;
            }

            if (moveCheck())
            {
                movePlayer();
            }
        }

    }//把已string化的鍵盤方位數值解碼，指揮檢查用vector3先去鍵盤要求前住的那一格方位

    public void autoMovement(float time, string c)
    {

        if (time >= countDown)
        {
            /*
            if (!doOnce) {
                doOnce = true;
                normalLerpSpeed = lerpSpeed;
                lerpSpeed = normalLerpSpeed * 2f;
            }
            */

            Debug.Log("autoMovement");
            MovementPart(c);

        }
    }//自動重復執行MovementPart

    void movePlayer()
    { //真正移動
        if (!roundScript.Static.isProcessingRound)
        {
            if (touchEnemy != null)
            {
                if (!roundScript.Static.DoAttackAniProcessingChecker && touchEnemy.GetComponent<enemyDataBase>().HP > 0)
                {
                    charactor_move.SetTrigger("attack");
                    //Random.Range(0,4);
                    //Debug.Log(Random.Range(0, 4));
                    charactor_move.SetInteger("attack_no.", Random.Range(0, 3));
                    roundScript.Static.DoAttackAniProcessingChecker = true;
                    StartCoroutine(WaitForAnimation("attackAni"));
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
    }

    void LerpMove()
    {
        if (startLerpMovement)
        {

            transform.position = Vector3.Lerp(transform.position, hitObjectPosition, (Time.time - startTime) * lerpSpeed);
            if (Mathf.Abs(Vector3.Distance(transform.position, hitObjectPosition)) == 0.0f)
            {
                startLerpMovement = false;

                lerpSpeed = normalLerpSpeed;
            }
            else if (Mathf.Abs(Vector3.Distance(transform.position, hitObjectPosition)) <= 0.1f)
            {
                roundScript.Static.movementProcessingChecker = false;
                charactor_move.SetBool("run", false);
                charactor_move.SetBool("idle", true);
            }
            else
            {
                charactor_move.SetBool("run", true);
                charactor_move.SetBool("idle", false);
            }

        }
    }

    public Vector3 hitObjectPosition = new Vector3();


    bool moveCheck()
    { //正確是否正確移動
        Collider[] hitColliders = Physics.OverlapSphere(center, 0.25f);
        Collider[] hitEnemyColliders = Physics.OverlapSphere(new Vector3(center.x, center.y, -1), 0.35f);
        touchEnemy = null;
        if (hitColliders.Length != 0)
        {
            hitObjectPosition = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1);
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
                        return true;
                    }
                }
                return true; // is Hititem
            }
            return true;
        }
        return false;
    }//檢查 檢查用vector3 目前所在的方位是否存在方塊(已是說是否有路) 有就移動 無就取消移動動作


    private IEnumerator WaitForAnimation(string animationTag)
    {
        do
        {
            yield return null;
        } while (!charactor_move.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));
        attackNpc(touchEnemy);
        //dead here
    }


    void attackNpc(GameObject touchEnemy)
    {
        //touch Enemy之後既行動

        if (playerDataBase.Static.DEF >= touchEnemy.GetComponent<enemyDataBase>().ATK)
        {
            touchEnemy.GetComponent<enemyDataBase>().HP = 0;
        }
        else
        {
            touchEnemy.GetComponent<enemyDataBase>().HP -= playerDataBase.Static.ATK;
        }
        isHitNpc = true;
        thisFrameMoved = true;



    }

}


