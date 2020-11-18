using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{

    public Text txtFPS;
    public float deltaTime;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        txtFPS.text = "FPS = " + Mathf.Ceil(fps).ToString();
    }
}
