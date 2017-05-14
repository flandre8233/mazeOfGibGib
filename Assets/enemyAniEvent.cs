using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAniEvent : MonoBehaviour {
    enemyScript enemyObject;

    private void Awake()
    {
        enemyObject = GetComponentInParent<Transform>().gameObject.GetComponentInParent<enemyScript>();
    }

    public void attackAnimationUseOnlyFunction()
    {
        enemyObject.enemyAttack();
    }

    public void onAniDied()
    {
        enemyObject.delEnemy();
    }

}
