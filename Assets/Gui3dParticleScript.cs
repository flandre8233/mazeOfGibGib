using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui3dParticleScript : MonoBehaviour {
    public static Gui3dParticleScript Static;

    public GameObject abilityHpmaxparticle;
    public GameObject abilitySpmaxparticle;
    public GameObject abilityAtkmaxparticle;
    public GameObject abilityDefmaxparticle;

    public List<Transform> spawnPosList;

    private void Awake()
    {
        if (Static == null)
        {
            Static = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void spawnHpMaxParticle()
    {
        GameObject obj = Instantiate(abilityHpmaxparticle, spawnPosList[0]);
        obj.transform.localPosition = new Vector3();
    }
    public void spawnSpMaxParticle()
    {
        GameObject obj = Instantiate(abilitySpmaxparticle, spawnPosList[1]);
        obj.transform.localPosition = new Vector3();
    }
    public void spawnAtkMaxParticle()
    {
        GameObject obj = Instantiate(abilityAtkmaxparticle, spawnPosList[2]);
        obj.transform.localPosition = new Vector3();
    }
    public void spawnDefMaxParticle()
    {
        GameObject obj = Instantiate(abilityDefmaxparticle, spawnPosList[3]);
        obj.transform.localPosition = new Vector3();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
