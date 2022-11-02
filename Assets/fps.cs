
using UnityEngine;

public class fps : MonoBehaviour
{
    public float updateInterval = 0.5f; //How often should the number update

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float frameps;

    GUIStyle textStyle = new GUIStyle();

    // Use this for initialization
    private void Start()
    {
        timeleft = updateInterval;

        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }

    // Update is called once per frame
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        
        if (timeleft <= 0.0)
        {
            frameps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 25), frameps.ToString("F2") + "FPS", textStyle);
    }
}