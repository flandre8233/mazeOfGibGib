using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopUpStatus : MonoBehaviour {
    public static shopUpStatus Static;
    public GameObject[] showUpStatus;
    public Text[] StatusText;

    public int spTimeLeft;
    public int atkLeftRound;
    public int defLeftRound;

    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }

    }

    private void Start()
    {
        roundScript.Static.roundSystem += roundSystemOnly;
        checkStatus();
    }

    public void UpDataSpTime (){
        spTimeLeft = (int)playerMainScript.Static.spTimeLeft;
        StatusText[0].text = spTimeLeft.ToString();
    }

    public void updateStatusInfo()
    {
        atkLeftRound = playerMainScript.Static.ATKbuffStartRound - playerMainScript.Static.ATKContinueRound;
        defLeftRound = playerMainScript.Static.DEFbuffStartRound - playerMainScript.Static.DEFContinueRound;

        StatusText[1].text = atkLeftRound.ToString();
        StatusText[2].text = defLeftRound.ToString();
    }

    public void checkStatus()
    {
        updateStatusInfo();
        showUpStatus[0].SetActive(playerMainScript.Static.inSPBuffStatus);
        showUpStatus[1].SetActive(playerMainScript.Static.inATKBuffStatus);
        showUpStatus[2].SetActive(playerMainScript.Static.inDEFBuffStatus);
    }

    public void roundSystemOnly()
    {
        checkStatus();
    }

}
