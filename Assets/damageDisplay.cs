using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDisplay : MonoBehaviour {
    public SpriteRenderer myImage;
    public Sprite[] mySprites;
    public Texture2D texture;
    // Use this for initialization
    void Start () {

        ChangeSprite(2,3);
    }

    void ChangeSprite(int number , int type) {
        myImage.sprite = mySprites[ number+(type*10) ];
    }

    // Update is called once per frame
    void Update () {
        //transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);

        
    }
}
