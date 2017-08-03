using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box : MonoBehaviour {
    public static box Static;
    public int itemProbability;
    Animator ani;

    public Text AD_Coin;

    public int existItemType;
    public int coin;
    public int extraCoin;
    public int watchAD_coin;
    public bool alreadyWatchADS = false;


    public bool get_coin = false;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        transform.rotation = new Quaternion();
    }

    private void Start()
    {
        extraCoin = coinReward(934) * (1 + playerDataBase.Static.currentFloor / 5);
        coin = ((coinReward(11) + coinReward(56) + coinReward(304) ) * (1 + playerDataBase.Static.currentFloor / 5));
        existItemType = itemAndEnemyProcessor.RandomProbabilitySystem(ref itemGenerator.Static.ProbabilityArray) - 1;
        watchAD_coin = coin + extraCoin;
    }

    int coinReward(int coin)
    {
        return coin + (coin / 100 * (Random.Range(0, 40)-20) );
    }

    public void openChest()
    {
        //spawn Item

        if (Random.Range(0,100) <= itemProbability )
        {
            spawnExItem();
        }

        //open chest ani
        roundScript.Static.IsOpeningChest = true;
        ani.Play("open");
        //get_coin = true;
        StartCoroutine(WaitForAnimation("openChest"));
        soundEffectManager.staticSoundEffect.play_characterOpenChest();
        GetCoin();
        ////Debug.Log(watchAD_coin);
        uiScript.Static.AD_coin.text = watchAD_coin.ToString();
    }

    private IEnumerator WaitForAnimation(string animationTag)
    {
        do
        {
            yield return null;
        } while (ani.GetCurrentAnimatorStateInfo(0).IsTag(animationTag));

        roundScript.Static.IsOpeningChest = false;
        Destroy(gameObject);
        //dead here
    }

    public void watchAD()
    {
        alreadyWatchADS = true;
        GetCoin();
        chessMovement.Static.money_chest_off();
    }
    public void GetCoin()
    {
        if (alreadyWatchADS==true)
        {
            playerDataBase.Static.COIN += coin + extraCoin;
        }
        else
        {
            playerDataBase.Static.COIN += coin;
        }

    }

    public void spawnExItem()
    {
        GameObject InstantiateItem = Instantiate(mapThingsGenerator.Static.itemArray[existItemType], transform.position, Quaternion.Euler(-90, 0, 0));
        InstantiateItem.transform.position = new Vector3(InstantiateItem.transform.position.x, InstantiateItem.transform.position.y, -0.2f);

        //
    }



    public Quaternion ImageLookAt2D(Vector3 from, Vector3 to)
    {
        Vector3 difference = to - from;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = (Quaternion.Euler(0.0f, 0.0f, rotationZ-90));
        return rotation;
    }

    public void allwayFaceAtPlayer()
    {
        //float Angle = ImageLookAt2D(transform.position, chessMovement.Static.transform.position).eulerAngles.y;
        transform.rotation = ImageLookAt2D(transform.position, chessMovement.Static.transform.position);
    }

}
