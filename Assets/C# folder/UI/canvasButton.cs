using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasButton : MonoBehaviour {
    public GameObject normalGameCanvas;
    public GameObject bigMapCanvas;
    public GameObject bigMapCamera;
    //public static Object Instantiate(particleManager.Static.pass_particle, parent.transform)


    public void buttonClick() {
        roundScript.Static.OnEnterNextLevel();
    }

    public void OpenMapButtonClick(bool Bool) {
        soundEffectManager.staticSoundEffect.play_Click_Button();
        normalGameCanvas.SetActive(!Bool);
        bigMapCanvas.SetActive(Bool);
        bigMapCamera.SetActive(Bool);
        mapTerrainGenerator.Static.findCenter();
        bigMapCamera.transform.position =new Vector3(mapTerrainGenerator.Static.center.x, mapTerrainGenerator.Static.center.y,bigMapCamera.transform.position.z)  ;
    }

    public void moveButton(string dir) {
        //chessMovement.Static.MovementPart(dir);
    }

    public void useItemButton(int number ) {

        if (playerMainScript.Static.itemArrayClone[number] == null) {
            return;
        }

        playerMainScript.Static.useItem(number);
    }

    public void returnTestButton() {
        if (playerDataBase.Static.currentFloor - roundScript.Static.checkPoint < 0 ) {
            playerDataBase.Static.currentFloor = 0;
        }
        else {
            playerDataBase.Static.currentFloor -= 11;
        }
        roundScript.Static.OnEnterNextLevel();
    }
    public void crystalTestButton() {

    }

    public void passRound()
    {
        roundScript.Static.pastRound();
        soundEffectManager.staticSoundEffect.play_levelPass();

       // .SetParent(canvas.transform);
    }

    /*

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

*/
}
