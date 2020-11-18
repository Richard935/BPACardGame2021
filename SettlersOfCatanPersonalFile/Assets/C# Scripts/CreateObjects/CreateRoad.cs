using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    public GameObject AdjacentSPP1, AdjacentSPP2;
    public GameObject[] AdjacentRoads;

    public MainGame MainScript;
    public SetupPhase SetupScript;

    public GameObject[] roadPrefabs;

    public void OnMouseDown()
    {

        int player = MainScript.getCurrentPlayer() - 1;

        var newObject = GameObject.Instantiate(roadPrefabs[player]);
        newObject.transform.position = gameObject.transform.position;
        newObject.transform.rotation = gameObject.transform.rotation;
        newObject.transform.SetParent(MainScript.gameBoard.transform);

        MainScript.removeRoadAccess(gameObject);

        Destroy(gameObject);

        MainScript.addRoadAccess(AdjacentRoads);
        MainScript.addSettlementAccess(AdjacentSPP1, AdjacentSPP2);   

        if(SetupScript.setupPhaseFinished == false)
        {
            SetupScript.roadPlaced();
        }
    }
}
