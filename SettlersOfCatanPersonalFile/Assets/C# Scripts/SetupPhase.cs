using System.Linq;
using UnityEngine;

public class SetupPhase : MonoBehaviour
{

    public GameObject[] allUIObjectsThatGetDisabled;
    public MainGame MainScript;
    
    public bool roundOne;
    public bool setupPhaseFinished;
    public bool forLastEndTurnClickDuringSetup;

    void Start()
    {
        MainScript.settlementClickables =  GameObject.FindGameObjectsWithTag("SPP").ToList();
        //Intializes bools on start
        setupPhaseFinished = false;
        roundOne = true;
        forLastEndTurnClickDuringSetup = false;

        //Sets current player to 0, will be change to 1 when the setupPhase method is called.
        //MainScript.setCurrentPlayer(0);

        //Disables all the UI buttons except for the end turn and pause buttons.
        for(int i = 0; i < allUIObjectsThatGetDisabled.Length; i++)
        {
            allUIObjectsThatGetDisabled[i].SetActive(false);
        }

        //Calls the setupPhase method.
        setupPhase();
    }   

    public void setupPhase()
    {

        int presentPlayer = MainScript.getCurrentPlayer();

        //Cycles the turns starting with player 1, 2, 3, 3, 2, 1
        if(presentPlayer == 3 && roundOne == true){
            roundOne = false;
        }else if(roundOne == false){
            presentPlayer--;
        }else{
            presentPlayer++;
        }

        //Disables the end turn button and enables the settlement placement points(SPP).
        MainScript.btnEndTurn.enabled = false;
        MainScript.activateAllSPP();
        
        //Sets the main current player variable.
        MainScript.setCurrentPlayer(presentPlayer);

        //Sets all the players resources to 0 the first time this method is called.
        if(presentPlayer == 1 && roundOne == true)
        {
            MainScript.setupResources();
        }
        
        //Sets the UI resource and player text indicators.
        MainScript.setIndicators();

    }

    public void showRoadsAdjacentToNewSettlement(GameObject[] adjacentRoads)
    {
        //Enables the road points adjancent to the newly placed settlement.
        foreach(var roadPoint in adjacentRoads)
        {
            if(roadPoint != null)
            {
                roadPoint.SetActive(true);
            }
            
        }
    }

    public void roadPlaced()
    {

        //Enables the end turn button to be clicked and disables the road clickables.
        MainScript.btnEndTurn.enabled = true;
        MainScript.showRoadClickables(false);

        //Checks if player one just placed his second settlement and road, if so then declare the setup phase to be over.
        if(MainScript.getCurrentPlayer() == 1 && roundOne == false)
        {
            setupPhaseFinished = true;
        }
    }
}
