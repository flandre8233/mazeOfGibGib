using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class testonly : MonoBehaviour {
    [SerializeField]
    GameObject go;



	// Update is called once per frame
	void Update () {

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) ) {
            go.SetActive(!go.activeSelf);
            go.transform.position = Input.mousePosition;
        }


        //GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
        
        go.transform.rotation = Quaternion.Euler(0, 0, fingerControlRotation.Static.gameObject.transform.rotation.eulerAngles.z);
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x * (1080.0f / Screen.width), Input.mousePosition.y * (1920.0f / Screen.height), 0);
        // * 0.830729167f
        //CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width, Input.mousePosition.y * scaler.referenceResolution.y / Screen.height);
        //Debug.Log(Input.mousePosition.x * (1080.0f / Screen.width) + "lkhj");
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 200, 0);
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
