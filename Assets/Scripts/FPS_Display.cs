using TMPro;
using UnityEngine;

public class FPS_Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FPS;
    private float updateInterval = 0.5f;

    private float accumulatedFPS = 0f;
    private int frames = 0;
    private float timeLeft;

    private void Start()
    {
        timeLeft = updateInterval;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accumulatedFPS += Time.timeScale / Time.deltaTime;
        frames++;

        if (timeLeft <= 0f)
        {
            int fps = (int)(float)(accumulatedFPS / frames);
            FPS.text = "FPS: " + fps.ToString();
            // Reset values for next interval
            timeLeft = updateInterval;
            accumulatedFPS = 0f;
            frames = 0;
        }
    }
}
