using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkManager : MonoBehaviour {
    RectTransform rectTS;

    public float curShrinkTime;
    public float needToShrinkTime;
    public int strong;


    private void Start()
    {
        rectTS = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (curShrinkTime < needToShrinkTime)
        {
            curShrinkTime += Time.deltaTime;
            earthQuake();
        }
        else
        {
            returnPos();
        }
    }

    float startTime;
    public float lerpSpeed = 0.0f;
    Vector2 targetPosition = new Vector2();

    public void startShrink()
    {
        curShrinkTime = 0;
    }

    public void earthQuake()
    {

        rectTS.anchoredPosition = Vector2.Lerp(rectTS.anchoredPosition, targetPosition, (Time.time - startTime) * lerpSpeed);
        float movementDistance = Mathf.Abs(Vector2.Distance(rectTS.anchoredPosition, targetPosition));
        
        if (movementDistance <= 0.1f)
        {
            resetEarthQuake(strong);
        }

    }

    void returnPos()
    {
        rectTS.anchoredPosition = Vector2.Lerp(rectTS.anchoredPosition, new Vector2(), (Time.time - startTime) * lerpSpeed);
    }

    void resetEarthQuake(int strong)
    {
        startTime = Time.time;
        targetPosition = new Vector3((0 + Random.Range(0, strong) - (strong / 2)) / 10.0f, (0 + Random.Range(0, strong) - (strong / 2)) / 10.0f);
    }

}
