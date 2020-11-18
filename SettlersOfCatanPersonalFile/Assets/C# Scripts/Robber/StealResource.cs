using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealResource : MonoBehaviour
{
    
    public MainGame MainScript;
    public GameObject btnConfirm;
    public int playerSelected;


    public void OnPlayerSelected()
    {

        string[] resourceType = {"wood", "brick", "ore", "wheat", "sheep"};
        int intRandom;
        System.Random random = new System.Random();
        while (true)
        {
            intRandom = random.Next(0, 5);
            if (MainScript.canRemoveResource(playerSelected, intRandom))
            {
                break;
            }
        }
        MainScript.setIndicators();
        MainScript.txtStealInstructions.SetActive(false);

        MainScript.txtStealIndicator.text = "You have stolen one " + resourceType[intRandom] + " from player " + (playerSelected + 1) + ".";
        MainScript.txtStealIndAsObject.SetActive(true);

        MainScript.btnStealFromPlayer[0].SetActive(false);
        MainScript.btnStealFromPlayer[1].SetActive(false);
        MainScript.btnStealFromPlayer[2].SetActive(false);
        btnConfirm.SetActive(true);
    }
    public void OnConfirmClick()
    {
        btnConfirm.SetActive(false);
        MainScript.txtStealIndAsObject.SetActive(false);
        MainScript.stealFromPlayerUI.SetActive(false);
        MainScript.gameBoard.SetActive(true);
    }
}
