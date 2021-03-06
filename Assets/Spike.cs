﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : groundScript {
    public int perRoundShowUpSpike = 5;
    public int curRoundCountDown = 0;

    public bool inShowSpike ;

    private void Start()
    {

        setupQuakeSpeed();
    }

    public Transform spikeObjectTransform;
    public Transform planeTransform;

    public int damage = 3;

    public int damagePercentage = 50;


    float startTime;
    public float lerpSpeed = 0.0f;
    public float lerpOrlSpeed = 1.0f;
    Vector3 targetPosition = new Vector3();

    public void countSpikeRound()
    {
        if (inShowSpike)
        {
            curRoundCountDown = 1;
            inShowSpike = false;
            serializeSpike();
        }
        else
        {
            curRoundCountDown++;
            if (curRoundCountDown >= perRoundShowUpSpike)
            {
                inShowSpike = true;
                if (haveSomethingInHereObject != null)
                {
                    switch (haveSomethingInHereObject.tag)
                    {
                        case "Player":
                            AttackPlayer();
                            break;

                        case "enemy":
                            AttackEnemy(haveSomethingInHereObject.GetComponent<enemyScript>() );
                            break;


                        default:
                            break;
                    }
                }
                serializeSpike();
            }
        }
        setupQuakeSpeed();

    }

    public void setupQuakeSpeed()
    {
        if (curRoundCountDown == perRoundShowUpSpike - 1)
        {
            lerpSpeed = lerpOrlSpeed * ((curRoundCountDown + 1) * 1.25f);
        }
        else
        {
            lerpSpeed = lerpOrlSpeed * ((curRoundCountDown + 1) * 0.5f);
        }
    }

    public void earthQuake()
    {
        if (!inShowSpike)
        {
            planeTransform.localPosition = Vector3.Lerp(planeTransform.localPosition, targetPosition, (Time.time - startTime) * lerpSpeed);
            float movementDistance = Mathf.Abs(Vector3.Distance(planeTransform.localPosition, targetPosition));
            if (movementDistance <= 0.1f)
            {
                resetEarthQuake(3);
            }
        }

    }
    public void AttackEnemy(enemyScript enemy)
    {

        int outputDamage = (int)((enemy.MaxHP / 100.0f) * damagePercentage);

        soundEffectManager.staticSoundEffect.play_characterHurtWithThorn();
        enemy.NpcTakeDamage(outputDamage);
    }

    public void AttackPlayer()
    {

        int outputDamage = (int)((playerDataBase.Static.MaxHP / 100.0f) * damagePercentage);

        soundEffectManager.staticSoundEffect.play_characterHurtWithThorn();
        playerMainScript.Static.playerTakeDamge(outputDamage, true);
    }
    

    float spikeLerpStartTime;
    IEnumerator spikeLerp(Vector3 targetPosition)
    { 
        float movementDistance;
        do
        {
            spikeObjectTransform.localPosition = Vector3.Lerp(spikeObjectTransform.localPosition, targetPosition, (Time.time - spikeLerpStartTime) * 6);
            movementDistance = Mathf.Abs(Vector3.Distance(spikeObjectTransform.localPosition, targetPosition));
            yield return null;
        } while ( !(movementDistance <= 0.1f) );

    }

    void resetEarthQuake(int strong)
    {
        startTime = Time.time;
        targetPosition = new Vector3((0 + Random.Range(0, strong) - (strong / 2)) / 10.0f, (0 + Random.Range(0, strong) - (strong / 2)) / 10.0f, 0);
    }
    public Animator ani;
    public  void serializeSpike()
    {
        ani.SetBool("open", inShowSpike);
    }


    public override void OnDestroy()
    {
            roundScript.Static.groundCheckSystem -= roundSystemUseOnly;
        
        roundScript.Static.spikeSystem -= countSpikeRound;
    }
}

