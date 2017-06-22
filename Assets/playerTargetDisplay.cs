using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerTargetDisplay : MonoBehaviour {
    public static playerTargetDisplay Static;
    public Image[] targetDisplay;

    public enemyDataBase targetObject;

    public Text hpDisplay;
    public Text atkDisplay;

    public Image hpBar;

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
        disableAllTargetDisplay();
    }

    public void disableAllTargetDisplay()
    {
        targetObject = null;
        foreach (var item in targetDisplay)
        {
            item.gameObject.SetActive(false);
        }
        hpDisplay.gameObject.SetActive(false);
        atkDisplay.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
    }

    public void DisplayTarget(enemyDataBase enemyData)
    {
        disableAllTargetDisplay();
        targetObject = enemyData;
        switch (targetObject.Level)
        {
            case 0:
                break;
            case 1:
                targetDisplay[3].gameObject.SetActive(true);
                break;
            case 2:
                targetDisplay[0].gameObject.SetActive(true);
                break;
            case 3:
                targetDisplay[4].gameObject.SetActive(true);
                break;
            case 4:
                targetDisplay[5].gameObject.SetActive(true);
                break;
        }
        hpDisplay.gameObject.SetActive(true);
        atkDisplay.gameObject.SetActive(true);
        hpBar.gameObject.SetActive(true);
    }

    public void updateDisplayTargetData()
    {
        if (targetObject == null)
        {
            return;
        }


        if (targetObject.HP > 0)
        {
            hpDisplay.text = targetObject.HP + "/" + targetObject.MaxHP;
        }
        else
        {

            hpDisplay.text = 0 + "/" + targetObject.MaxHP;
        }
        atkDisplay.text = targetObject.ATK + "";
    }



    private void Update()
    {
        updateDisplayTargetData();
    }

}
