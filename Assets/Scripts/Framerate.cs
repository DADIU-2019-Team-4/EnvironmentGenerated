using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Framerate : MonoBehaviour
{
    private Text Text;
    private float prevFPS;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<Text>();
        prevFPS = 45f;
    }

    // Update is called once per frame
    void Update()
    {
        prevFPS = (prevFPS + 1 / Time.deltaTime) / 2;
        Text.text = "FPS: " + Mathf.Round(prevFPS);
    }
}
