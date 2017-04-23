using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDisplay : MonoBehaviour {
    public SpriteRenderer myImage;
    public Sprite[] mySprites;
    public Texture2D texture;
    public float textDistance = 0.75f;
    // Use this for initialization
    void Start () {

        //ChangeSprite(2,3);
        //spawnDamageDisplay(1345);
    }

    public void spawnDamageDisplay(int damage,int type) {
        short digits = (short)getDigits(damage,0);
        float spawnXAxisLimit = ( (textDistance * digits)/ 2 )- (textDistance / 2);
        Debug.Log(spawnXAxisLimit);
        int number = damage;
        for (int i = 0; i < digits; i++) {
            GameObject emptyGameObject = new GameObject();
            //Instantiate(emptyGameObject,transform.position,Quaternion.identity);
            myImage = emptyGameObject.AddComponent<SpriteRenderer>();
            emptyGameObject.transform.parent = transform;
            emptyGameObject.transform.localPosition = new Vector3( spawnXAxisLimit-(textDistance * i ),0,0);
            Debug.Log(emptyGameObject.transform.localPosition);
            ChangeSprite(number % 10, type );
            number  /= 10;
        }
    }

    public static int getDigits(int n1, int nodigits) {
        if (n1 == 0)
            return nodigits;

        return getDigits(n1 / 10, ++nodigits);
    }


public void ChangeSprite(int number , int type) {
        myImage.sprite = mySprites[ number+(type*10) ];
    }

    // Update is called once per frame
    void Update () {
        //transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);

        
    }
}
