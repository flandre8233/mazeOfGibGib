using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessMovement : MonoBehaviour {
    public static chessMovement Static;
    [SerializeField]
    Vector3 center;

    public float downTime = 0;
    public float countDown = 0.5f;
    public bool ready = false;
    public string faceDirection = "up";

    public bool thisFrameMoved=false;
    bool isHitNpc = false;
    bool StartedOnce = false;

    [Range (1,5)]
    public float lerpSpeed = 1;
    // Use this for initialization
    void Start () {
        Static = this;
        reset();
    }

    // Update is called once per frame

    //你自己有能力就先控我部機研究啦

    void Update() {
        LerpMove();
        if (thisFrameMoved) {
            if (isHitNpc) { //這步會打中npc的話
                roundScript.Static.roundSystem -= roundScript.Static.playerMainScript.subSP;
                roundScript.Static.pastRound();
                roundScript.Static.roundSystem += roundScript.Static.playerMainScript.subSP;
            }
            else {//不是要打npc
                roundScript.Static.pastRound();
            }
            isHitNpc = false;
            thisFrameMoved = false;
        }

        if (!ready) {
            //faceDirection = movementInput(faceDirection);
        }

        if (Input.GetKeyDown(KeyCode.E) ) {
            roundScript.Static.pastRound();
        }


        faceDirection = movementInput(faceDirection);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            if (!ready) { //autoMovementParts
                downTime = 0;
                ready = true;
            }
            else {
                ready = false;
            } //problem
            MovementPart(faceDirection);
            
        }

        if (ready) {
            //autoMovement(faceDirection);
        }
        // w係+ s係-   a係-  d係+
    }

    
    void reset() {
        center = Vector3.zero;
        moveCheck();
        thisFrameMoved = false;
    }//一開頭設定
    string movementInput(string DefaultDirection) {

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


        if (Input.GetKeyDown(KeyCode.W)) {
            return "up";

        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            return "down";
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            return "left";
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            return "right";
        }

        return DefaultDirection;
    }//找出鍵盤輸入的方位是什麼 並string化輸入數值
    void MovementPart(string moveDirection) {

        switch (moveDirection) {
            default:
                break;

            case "up":
                center = new Vector3(transform.position.x + 0, transform.position.y + 1, 0); //W
                moveCheck();
                break;
            case "down":
                center = new Vector3(transform.position.x + 0, transform.position.y - 1, 0); //S
                moveCheck();
                break;
            case "left":
                center = new Vector3(transform.position.x - 1, transform.position.y + 0, 0); //A
                moveCheck();
                break;
            case "right":
                center = new Vector3(transform.position.x + 1, transform.position.y + 0, 0);
                moveCheck();
                break;
            case "up/left":
                center = new Vector3(transform.position.x - 1, transform.position.y + 1, 0);
                moveCheck();
                break;
            case "up/right":
                center = new Vector3(transform.position.x + 1, transform.position.y + 1, 0);
                moveCheck();
                break;
            case "down/left":
                center = new Vector3(transform.position.x - 1, transform.position.y - 1, 0);
                moveCheck();
                break;
            case "down/right":
                center = new Vector3(transform.position.x + 1, transform.position.y - 1, 0);
                moveCheck();
                break;
        }
    }//把已string化的鍵盤方位數值解碼，指揮檢查用vector3先去鍵盤要求前住的那一格方位
    void autoMovement(string direction) {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            downTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            downTime = 0;
            ready = false;
        }
        if (downTime >= countDown) {
            MovementPart(direction);
        }
    }//自動重復執行MovementPart

    void movePlayer(Collider[] hitColliders) { //真正移動
        //
        //transform.parent = groundBox.transform; //change object parent
        groundBox = hitColliders[0].gameObject;
        groundBoxPosition = new Vector3(groundBox.transform.position.x, groundBox.transform.position.y, -2);
        startLerpMovement = true;
        startTime = Time.time;

        //transform.localPosition = Vector3.zero;
        //ansform.localPosition = new Vector3(0, 0, -2); //"<--"
        thisFrameMoved = true;
    }

    private float startTime;
    public bool startLerpMovement=false;
    Vector3 groundBoxPosition;
    GameObject groundBox = null;
    void LerpMove() {
        if (startLerpMovement) {
            transform.position = Vector3.Lerp(transform.position, groundBoxPosition, (Time.time - startTime) * lerpSpeed);
            //Debug.Log(groundBoxPosition);
            //Debug.Log(Vector3.Distance(transform.position, groundBoxPosition));
            if (Vector3.Distance(transform.position, groundBoxPosition) == 0) {
                startLerpMovement = false;
                
            }
        }
    }

    void moveCheck() { //正確是否正確移動
        Collider[] hitColliders = Physics.OverlapSphere(center, 0.25f);
        Collider[] hitEnemyColliders = Physics.OverlapSphere(new Vector3(center.x,center.y,-2), 0.15f);

        if (hitColliders.Length >= 1) {
            if (hitEnemyColliders.Length >= 1) {
               

                bool TouchEnemy = false;
                GameObject touchEnemy=null;
                foreach (var item in hitEnemyColliders) {
                    if (item.tag == "enemy") {
                        touchEnemy = item.gameObject;
                        TouchEnemy = true;
                    }
                }


                if (!TouchEnemy) {
                    movePlayer(hitColliders);
                }
                else {
                    //touch Enemy之後既行動
                    if (playerDataBase.Static.DEF >= touchEnemy.GetComponent<enemyDataBase>().ATK) {
                        touchEnemy.GetComponent<enemyDataBase>().HP = 0;
                    }
                    else {
                        touchEnemy.GetComponent<enemyDataBase>().HP -= playerDataBase.Static.ATK;
                    }
                    isHitNpc = true;
                    thisFrameMoved = true;
                }

            }
            else {
                movePlayer(hitColliders);
            }

            
        }
    }//檢查 檢查用vector3 目前所在的方位是否存在方塊(已是說是否有路) 有就移動 無就取消移動動作
}
