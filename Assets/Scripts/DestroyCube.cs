using System.Collections;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(CourDestroy(gameObject));       
    }

    IEnumerator CourDestroy(GameObject thisGameObject)
    {
        yield return new WaitForSeconds(2f);
        //Debug.Log(thisGameObject.name + " - " + thisGameObject.transform.transform.position.y);
        if (thisGameObject.transform.transform.position.y < -50f)
            Destroy(thisGameObject);
    }
}
