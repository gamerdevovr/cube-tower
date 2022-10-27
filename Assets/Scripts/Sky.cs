using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{

    public GameObject sky;

    public void SetActive() 
    {
        if (sky.activeSelf)
        {
            sky.SetActive(false);
        }
        else 
        {
            sky.SetActive(true);
        }
    }
}
