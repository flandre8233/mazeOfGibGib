using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnLava : MonoBehaviour {
    public float spawnTime = 1.0f;
    bool startDelay = true;
    bool spawnBool = false;

    public GameObject[] lavaObject;

    public List<GameObject> lavaObjectArray = new List<GameObject>();

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnLavaStartDelay() );

    }
	
	// Update is called once per frame
	void Update () {
        if (!spawnBool && !startDelay)
        {
            StartCoroutine(spawnLavaWait() );
        }

	}

    IEnumerator spawnLavaStartDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.0f,15.0f) );
        startDelay = false;
    }

    IEnumerator spawnLavaWait()
    {
        spawnBool = true;
        yield return new WaitForSeconds(spawnTime);
        Vector3 v3= new Vector3(transform.position.x,transform.position.y,-1f);
        lavaObjectArray.Add(Instantiate(lavaObject[Random.Range(0, lavaObject.Length)], v3, Quaternion.identity) );
        StartCoroutine(spawnLavaDestroy ( lavaObjectArray[lavaObjectArray.Count-1] )  );
        spawnBool = false;
    }

    IEnumerator spawnLavaDestroy(GameObject go)
    {
        //GameObject goDestroy = go;
        yield return new WaitForSeconds(5);
        lavaObjectArray.Remove(go);
        Destroy(go);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < lavaObjectArray.Count-1; i++)
        {
            Destroy(lavaObjectArray[i]);

            lavaObjectArray.RemoveAt( i );
        }

    }

}
