using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivity : MonoBehaviour
{
    public GameObject[] SwitchObjects;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        OnMouseDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        foreach (var o in SwitchObjects)
            o.SetActive(false);

        index = (++index) % SwitchObjects.Length;

        SwitchObjects[index].SetActive(true);
    }
}
