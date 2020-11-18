using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMove : MonoBehaviour 
{

    public MainGame MainScript;

    //x = index of resource type, y = number on tile, z = addition amount (1 or 2)
    public Vector3 TileInfo;

    public GameObject parentOfNumSprites;
    public GameObject robber;

    public void OnMouseDown()
    {

        //Debug.Log("Hello");
        //Tell Player to move Robber

        if(MainScript.tileRobberIsOn != null)
        {
            MainScript.tileRobberIsOn.SetActive(true);
        }     

        robber.transform.position = gameObject.transform.position;

        MainScript.refrenceToDeactivatedTile = TileInfo;

        //check if other players has same vector, if so then allow player to steal from that player

        BoxCollider[] numSpritesBoxColliders = parentOfNumSprites.GetComponentsInChildren<BoxCollider>();
        foreach (var boxCollider in numSpritesBoxColliders)
        {
            boxCollider.enabled = false;
        }

        MainScript.tileRobberIsOn = gameObject;
        MainScript.StealingResource();

        gameObject.SetActive(false);
        MainScript.btnEndTurn.enabled = true;
    }

}
