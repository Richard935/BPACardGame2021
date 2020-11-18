using UnityEngine;

public class btnEndTurn : MonoBehaviour
{
    public MainGame MainScript;
    public SetupPhase SetupScript;
    public btnRoll RollScript;

    public void OnClick()
    {
        if (SetupScript.setupPhaseFinished == false)
        {
            SetupScript.setupPhase();
        }
        else if (SetupScript.forLastEndTurnClickDuringSetup == false && SetupScript.setupPhaseFinished == true)
        {
            for (int i = 0; i < SetupScript.allUIObjectsThatGetDisabled.Length; i++)
            {
                SetupScript.allUIObjectsThatGetDisabled[i].SetActive(true);
            }
            SetupScript.forLastEndTurnClickDuringSetup = true;
            MainScript.btnEndTurn.enabled = false;
        }
        else
        {
            int presentPlayer = MainScript.getCurrentPlayer();
            presentPlayer++;
            MainScript.setCurrentPlayer(presentPlayer);

            MainScript.btnEndTurn.enabled = false;
            MainScript.btnRollDice.enabled = true;
            MainScript.setIndicators();

            RollScript.imgBoxesForDiceImages[0].sprite = null;
            RollScript.imgBoxesForDiceImages[1].sprite = null;
        }
    }

}
