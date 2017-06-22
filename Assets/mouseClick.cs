using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClick : MonoBehaviour {

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<npcSensor>() != null  )
            {

                playerTargetDisplay.Static.DisplayTarget(hit.collider.gameObject.GetComponentInParent<enemyDataBase>());
            }
            Debug.Log(hit.transform.name);
        }
    }

}
