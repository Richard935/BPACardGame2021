using UnityEngine;
using UnityEngine.UI;

public class btnRoll : MonoBehaviour
{

    public Sprite[] diceImages;
    public Image[] imgBoxesForDiceImages;
    public GameObject parentOfNumSprites;

    public MainGame MainScript;
    
    public void OnRollClick()
    {

        System.Random random = new System.Random();
        int roll;
        int sumOfRoll = 0;

        roll = random.Next(1, 7);
        sumOfRoll += roll;
        imgBoxesForDiceImages[0].sprite = diceImages[(roll - 1)];

        roll = random.Next(1, 7);
        sumOfRoll += roll;
        imgBoxesForDiceImages[1].sprite = diceImages[(roll - 1)];


        if (sumOfRoll == 7)
        {
            BoxCollider[] numSpritesBoxColliders = parentOfNumSprites.GetComponentsInChildren<BoxCollider>();
            foreach(var boxCollider in numSpritesBoxColliders)
            {
                boxCollider.enabled = true;
            }
        }
        else
        {
            roll--;
            MainScript.AllocateResources(sumOfRoll);
            MainScript.btnEndTurn.enabled = true;
        }

        MainScript.btnRollDice.enabled = false;
        
    }

}
