using System.Collections;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(CourDestroy());       
    }

    IEnumerator CourDestroy()
    {
        yield return new WaitForSeconds(2f);

        if (transform.position.y < -50f)
            Destroy(this);
    }

}
