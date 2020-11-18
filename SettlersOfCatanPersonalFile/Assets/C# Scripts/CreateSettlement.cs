using System.Linq;
using UnityEngine;

public class CreateSettlement : MonoBehaviour
{

    public GameObject AdjacentSPP1;
    public GameObject AdjacentSPP2;
    public GameObject AdjacentSPP3;
    public GameObject[] AdjacentRoads;

    public Vector3[] resourceAccess;

    public MainGame MainScript;
    public SetupPhase SetupScript;

    public GameObject[] settlementPrefabs;

    //public MainGame mainScript;

    public void OnMouseDown()
    {
        Vector3 position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);

        int player = MainScript.getCurrentPlayer() - 1;

        var newObject = GameObject.Instantiate(settlementPrefabs[player]);
        newObject.transform.position = position;

        MainScript.removeSettlementAccess(AdjacentSPP1, AdjacentSPP2, AdjacentSPP3, gameObject);
        MainScript.addSettlement(newObject);
        MainScript.addRoadAccess(AdjacentRoads);

        Destroy(AdjacentSPP1);
        Destroy(AdjacentSPP2);
        Destroy(AdjacentSPP3);
        Destroy(gameObject);

        MainScript.addPlayersResourceAccess(resourceAccess); 

        //---------------------------------------------------------------------------------------------------------
        if (SetupScript.setupPhaseFinished == false)
        {
            SetupScript.showRoadsAdjacentToNewSettlement(AdjacentRoads);
            MainScript.deactivateAllSPP();
        }
        else
        {
            MainScript.deactivatePlayersSPP();
        }

    }
}
