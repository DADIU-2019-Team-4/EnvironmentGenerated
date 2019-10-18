using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddParticle : MonoBehaviour
{
    public Text TextField;
    public GameObject ParticleObject;
    private int i = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        i++;
        TextField.text = string.Format("Particle Systems: {0}", i);
        Instantiate(ParticleObject);
    }
}
