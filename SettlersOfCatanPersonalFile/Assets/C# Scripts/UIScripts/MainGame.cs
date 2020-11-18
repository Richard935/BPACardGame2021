using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    //Stores the settlement clickables or settlement placement points.
    public List<GameObject> settlementClickables;

    //Pulls in the end turn btn and roll dice button. Set in unity editor.
    public Button btnEndTurn;
    public Button btnRollDice;

    //------------------------
    public GameObject tileRobberIsOn;
    public Vector3 refrenceToDeactivatedTile;


    public GameObject stealFromPlayerUI;
    public GameObject gameBoard;
    public Text txtStealIndicator;

    //Pulls in the text boxes for each resource, current player indicator, and victory points indicator. Set in unity editor.
    public Text txtPlayerIndicator;
    public Text[] txtResourceBoxes;
    //Used as a parallel array with txtResourceBoxes.
    string[] textBeforeNum = { "Wood = ", "Brick = ", "Ore = ", "Wheat = ", "Sheep = ", "Total Victory Points = " };

    //Gets the SetupPhase script to be able to refrence the methods in the class.
    public SetupPhase setupScript;
    
    //Class that holds all the players info.
    PlayerVariables playerInfo = new PlayerVariables();

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Initiates player variables when called by the SetupPhase script.
    public void setupResources()
    {
        //Sets each players resource values to the starting value, 0.
        for (int i = 0; i < 6; i++)
        {
            playerInfo.playersInfo[0].Add(0);
            playerInfo.playersInfo[1].Add(0);
            playerInfo.playersInfo[2].Add(0);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Gets and sets currentPlayer variable in playerInfo class.
    public int getCurrentPlayer()
    {
        return playerInfo.currentPlayer;
    }
    public void setCurrentPlayer(int player)
    {
        playerInfo.currentPlayer = player;
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Sets the text boxes that indicate resources, current player, and total victory points.
    public void setIndicators()
    {
        //If current player is equal to four, then change it to one, since they're is only three players.
        if(playerInfo.currentPlayer == 4)
        {
            playerInfo.currentPlayer = 1;
        }

        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer - 1;

        //Loops through setting each text box in txtResourceBoxes array.
        for (int i = 0; i < txtResourceBoxes.Length; i++)
        {
            txtResourceBoxes[i].text = textBeforeNum[i].ToString() + playerInfo.playersInfo[index][i].ToString();
        }

        //Sets player indicator text box.
        txtPlayerIndicator.text = "Player " + playerInfo.currentPlayer + "'s Turn";    
        
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Adds tile access to a player, on a list
    public void addPlayersResourceAccess(Vector3[] tileInfo)
    {
        //Each vector = (resourceNum, number on tile, give amount)

        int index = playerInfo.currentPlayer - 1;

        //Adds vectors to a current players list for use later.
        foreach (var item in tileInfo)
        {
               playerInfo.playersResTiles[index].Add(item);
        }

    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Gives out resources accordingly. 
    public void AllocateResources(int numRolled)
    {
        Vector3 deactivateTile1 = new Vector3(refrenceToDeactivatedTile.x, refrenceToDeactivatedTile.y, 1);
        Vector3 deactivateTile2 = new Vector3(refrenceToDeactivatedTile.x, refrenceToDeactivatedTile.y, 2);

        //For each player.
        for (int i = 0; i < 3; i++)
        {
            //Loop throughs each vector in list.
            foreach(var item in playerInfo.playersResTiles[i])
            {
                //Checks if the y value in vetor is equal to numRolled and if the vecotor is not equal to the tile the robber is on.;
                if(Convert.ToInt32(item.y) == numRolled && item != deactivateTile1 && item != deactivateTile2)
                {
                    //Adds the z(depends on if settelment(1) or city(2)) value in vector to the specific resource the tile gives, indicated by the x)
                    playerInfo.playersInfo[i][Convert.ToInt32(item.x)] += Convert.ToInt32(item.z);
                }
            }
        }

        //Refreshs the text box.
        setIndicators();     
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //The following methods use the players road access list.

    //This method adds the road clickables, that the current player now has access to, to the current players road access list.
    public void addRoadAccess(GameObject[] roads)
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer - 1;

        //Adds all the road clickables passed into this object, to the current players road access list.
        for (int i = 0; i < roads.Length; i++)
        {          
            playerInfo.playersRoadAccess[index].Add(roads[i]);        
        }
    }

    //This method removes the road clickable objects that are passed into this method from all the players road access list to prevent null errors when using the lists.
    public void removeRoadAccess(GameObject removeThis)
    {
        //Loops through all players road access list removing the passed in object.
        for(int i = 0; i < 3; i++)
        {
            playerInfo.playersRoadAccess[i].Remove(removeThis);
        } 

    }

    //Shows the roads the current player has access to. 
    public void showRoadClickables(bool onOrOff)
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer - 1;

        //Activates all objects in current players road access list. Skipping any possible null items in list.
        foreach (var item in playerInfo.playersRoadAccess[index])
        {
            if(item != null)
            {
                item.SetActive(onOrOff);
            }      
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void addSettlementAccess(GameObject pointOne, GameObject pointTwo)
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer - 1;

        if(pointOne != null)
        {
            playerInfo.playersSettlementAccess[index].Add(pointOne);
        }
        if (pointTwo != null)
        {
            playerInfo.playersSettlementAccess[index].Add(pointTwo);
        }
    }
    public void removeSettlementAccess(GameObject obj1, GameObject obj2, GameObject obj3, GameObject obj4)
    {
        for(int i = 0; i < 3; i++)
        {
            playerInfo.playersRoadAccess[i].Remove(obj1); 
            playerInfo.playersRoadAccess[i].Remove(obj2);
            playerInfo.playersRoadAccess[i].Remove(obj3);
            playerInfo.playersRoadAccess[i].Remove(obj4);

            settlementClickables.Remove(obj1);
            settlementClickables.Remove(obj2);
            settlementClickables.Remove(obj3);
            settlementClickables.Remove(obj4);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void activateAllSPP() {

        foreach(GameObject child in settlementClickables)
        {     
            child.gameObject.SetActive(true);
        }
    }
    public void deactivateAllSPP()
    {

        foreach(GameObject child in settlementClickables)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void activatePlayersSPP()
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer;

        foreach(GameObject placementPoint in playerInfo.playersSettlementAccess[index])
        {
            placementPoint.SetActive(true);
        }       
    }
    public void deactivatePlayersSPP()
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer;

        foreach (GameObject placementPoint in playerInfo.playersSettlementAccess[index])
        {
            placementPoint.SetActive(false);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void addSettlement(GameObject settlement)
    {
        //Takes current players number and subtracts one for use as an index value.
        int index = playerInfo.currentPlayer - 1;

        playerInfo.playersSettlements[index].Add(settlement);

    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void increaseLevel(GameObject settlementClicked, GameObject replacement)
    {
        int index;

        //Takes current players number and subtracts one for use as an index value.
        int playerToIndex = playerInfo.currentPlayer - 1;

        List<Vector3> currentPlayersList = playerInfo.playersResTiles[playerToIndex]; 

        index = playerInfo.playersSettlements[playerToIndex].IndexOf(replacement);
        currentPlayersList[index] = new Vector3(currentPlayersList[index].x, currentPlayersList[index].y, 2);

        playerInfo.playersSettlements[playerToIndex].Insert(index, replacement);
        playerInfo.playersSettlements[playerToIndex].Remove(settlementClicked);

    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public GameObject[] btnStealFromPlayer;
    public void StealingResource()
    {
        bool[] playersToStealFrom = FindPlayerOnTile();

        if(playersToStealFrom[0] != false || playersToStealFrom[1] != false || playersToStealFrom[2] != false)
        {
            if(playersToStealFrom[0] && doesPlayerHaveResources(0))
            {
                btnStealFromPlayer[0].SetActive(true);
            }   
            if (playersToStealFrom[1] && doesPlayerHaveResources(1))
            {
                btnStealFromPlayer[1].SetActive(true);
            }
            if (playersToStealFrom[2] && doesPlayerHaveResources(2))
            {
                btnStealFromPlayer[2].SetActive(true);
            }
            stealFromPlayerUI.SetActive(true);
        }

    }
    private bool[] FindPlayerOnTile()
    {
        bool[] canStealFromPlayers = { false, false, false };


        int index = playerInfo.currentPlayer - 1;
        if(index != 0)
        {
            foreach(var vector in playerInfo.playersResTiles[0])
            {
                if(vector.x == refrenceToDeactivatedTile.x && vector.y == refrenceToDeactivatedTile.y)
                {
                    canStealFromPlayers[0] = true;
                    break;
                }
            }
        }
        if (index != 1)
        {
            foreach (var vector in playerInfo.playersResTiles[1])
            {
                if (vector.x == refrenceToDeactivatedTile.x && vector.y == refrenceToDeactivatedTile.y)
                {
                    canStealFromPlayers[1] = true;
                    break;
                }
            }
        }
        if (index != 2)
        {
            foreach (var vector in playerInfo.playersResTiles[2])
            {
                if (vector.x == refrenceToDeactivatedTile.x && vector.y == refrenceToDeactivatedTile.y)
                {
                    canStealFromPlayers[2] = true;
                    break;
                }
            }
        }

        return canStealFromPlayers;
    }
    public bool doesPlayerHaveResources(int index)
    {

        for(int i = 0; i < 5; i++)
        {
            if(playerInfo.playersInfo[index][i] != 0)
            {
                return true;
            }
            
        }
        return false;
    }
    public bool canRemoveResource(int index, int resourceType)
    {
        
        if(playerInfo.playersInfo[index][resourceType] != 0)
        {
            playerInfo.playersInfo[index][resourceType]--;
            playerInfo.playersInfo[playerInfo.currentPlayer - 1][resourceType]++;
            return true;
        }
        else
        {
            return false;
        }
    }
}