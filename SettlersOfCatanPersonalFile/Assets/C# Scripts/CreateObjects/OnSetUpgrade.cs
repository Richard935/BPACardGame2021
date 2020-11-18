using UnityEngine;

public class OnSetUpgrade : MonoBehaviour
{

    public GameObject[] cityPrefabs;
    public MainGame MainScript;

    public void OnMouseDown()
    {

        int player = MainScript.getCurrentPlayer() - 1;


        var newObject = GameObject.Instantiate(cityPrefabs[player]);
        newObject.transform.rotation = gameObject.transform.rotation;

        MainScript.increaseLevel(gameObject, newObject);
    }

}
