using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pathDirection
{
    notyet,
    up,
    down,
    left,
    right,
    playerPoint
    
}

public class pathfinding : MonoBehaviour {
    public static pathfinding Static;

    private void Awake()
    {
        if (Static != null)
        {
            Destroy(this);
        }
        else
        {
            Static = this;
        }
    }

    public void BakeAllFloor(Vector3 playerGoundVector3,groundScript playerCenterGround)
    {
        if (roundScript.Static.isEnterCheckPoint())
        {
            return;
        }

        resetAllFloorPathMark();
        playerCenterGround.pathdirection = pathDirection.playerPoint;

        List < groundScript > startPoint = new List<groundScript>();
        startPoint.Add(playerCenterGround);
        Recursive(startPoint);
    }

    List<groundScript> Recursive(List<groundScript> loopArray)
    {
        if (loopArray.Count <= 0)
        {
            return null;
        }
        List<groundScript> nextLoopGroundArray = new List<groundScript>();
        foreach (var item in loopArray)
        {
            nextLoopGroundArray.AddRange(getNeighbor(item) );
        }

        return Recursive(nextLoopGroundArray);
    }

    public List<groundScript> getNeighbor(groundScript objectPos)
    {
        Vector3 point = objectPos.transform.position;
        List<groundScript> neighborArray = new List<groundScript>();
        Vector3[] dirArray = new Vector3[4];
        dirArray[0] = new Vector3(point.x,point.y+1,point.z);
        dirArray[1] = new Vector3(point.x, point.y -1, point.z);
        dirArray[2] = new Vector3(point.x -1, point.y, point.z);
        dirArray[3] = new Vector3(point.x + 1, point.y , point.z);
        for (int i = 0; i < dirArray.Length; i++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(dirArray[i], 0.25f);
            if (hitColliders.Length <= 0 )
            {
                continue;
            }
            if (hitColliders[0].GetComponent<groundScript>().pathdirection != pathDirection.notyet )
            {
                continue;
            }
            neighborArray.Add(hitColliders[0].GetComponent<groundScript>()) ;
            switch (i)
            {
                case 0:
                    hitColliders[0].GetComponent<groundScript>().pathdirection = pathDirection.down;
                    break;
                case 1:
                    hitColliders[0].GetComponent<groundScript>().pathdirection = pathDirection.up;
                    break;
                case 2:
                    hitColliders[0].GetComponent<groundScript>().pathdirection = pathDirection.right;
                    break;
                case 3:
                    hitColliders[0].GetComponent<groundScript>().pathdirection = pathDirection.left;
                    break;

                default:
                    break;
            }
        }

        objectPos.alreadyFindAllNeighbor = true;
        return neighborArray;
    }

    public void resetAllFloorPathMark()
    {
        foreach (var item in mapTerrainGenerator.Static.thisLevelAllFloor)
        {
            item.GetComponent<groundScript>().pathdirection = pathDirection.notyet;
            item.GetComponent<groundScript>().alreadyFindAllNeighbor = false;
        }

        
    }


    private void Start()
    {
        roundScript.Static.roundSystem += RoundUseOnly;
    }

    private void OnDestroy()
    {

        roundScript.Static.roundSystem -= RoundUseOnly;
    }

    private void Update()
    {

    }

    void RoundUseOnly()
    {
        BakeAllFloor(chessMovement.Static.center, chessMovement.Static.playerCenterGround);
    }

}
