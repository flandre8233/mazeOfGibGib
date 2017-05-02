using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class testonly : MonoBehaviour
{
    [SerializeField]
    GameObject go;

    /*

    */
    Vector2 mouseToSceen() {
        return new Vector2(Input.mousePosition.x / (Screen.width / GetComponent<CanvasScaler>().referenceResolution.x) , Input.mousePosition.y / (Screen.height / GetComponent<CanvasScaler>().referenceResolution.y));
    }

    // Update is called once per frame
    void Update() {
        //GetComponent<CanvasScaler>().referenceResolution.x

        //Debug.Log(GUIUtility.ScreenToGUIPoint (new Vector2(Input.mousePosition.x, Input.mousePosition.y) ) );
        //Debug.Log(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if ( ( !IsPointerOverUIObject() ) && Input.GetMouseButtonDown(0) && Input.mousePosition.x <= Screen.width * 0.8) {
            go.SetActive(!go.activeSelf);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(mouseToSceen().x - GetComponent<CanvasScaler>().referenceResolution.x/2 , mouseToSceen().y - GetComponent<CanvasScaler>().referenceResolution.y / 2) ;



        }

        //!EventSystem.current.IsPointerOverGameObject() ||
        //GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
        
        go.transform.rotation = Quaternion.Euler(0, 0, -fingerControlRotation.Static.gameObject.transform.rotation.eulerAngles.z);


        //GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x * (1080.0f / Screen.width), Input.mousePosition.y * (1920.0f / Screen.height), 0);
        // * 0.830729167f
        //CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width, Input.mousePosition.y * scaler.referenceResolution.y / Screen.height);
        //Debug.Log(Input.mousePosition.x * (1080.0f / Screen.width) + "lkhj");
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 200, 0);
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }

}
