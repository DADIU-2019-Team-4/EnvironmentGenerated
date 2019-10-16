using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{

    public float MinimumSize;
    public float DistanceSensitivity;

    private bool wasDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!wasDestroyed && gameObject.transform.localScale.x < MinimumSize)
            DestroyMe();
    }



    private void DestroyMe()
    {
        wasDestroyed = true;
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DeathCam(2));
    }

    IEnumerator DeathCam(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void UpdateDistanceTravelled(float magnitude)
    {
        gameObject.transform.localScale -= Vector3.one * DistanceSensitivity * magnitude;
    }
}
