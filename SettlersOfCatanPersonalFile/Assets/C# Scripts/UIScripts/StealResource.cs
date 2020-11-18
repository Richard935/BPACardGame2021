using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealResource : MonoBehaviour
{
    
    public MainGame MainScript;
    public GameObject btnConfirm;

    public void OnPlayerSelected()
    {
        int playerSelected;
        playerSelected = System.Convert.ToInt32(gameObject.tag);

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

        MainScript.txtStealIndicator.text = "You have stolen one " + resourceType[intRandom];

        MainScript.btnStealFromPlayer[0].SetActive(false);
        MainScript.btnStealFromPlayer[1].SetActive(false);
        MainScript.btnStealFromPlayer[2].SetActive(false);
        btnConfirm.SetActive(true);
    }
    public void OnConfirmClick()
    {
        btnConfirm.SetActive(false);
        MainScript.stealFromPlayerUI.SetActive(false);
    }
}
