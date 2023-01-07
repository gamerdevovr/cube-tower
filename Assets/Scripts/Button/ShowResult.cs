using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{

    public GameObject podlozhka, result1, result2, result3, close;

    public void Start()
    {

    }

    public void CloseForm()
    {
        podlozhka.SetActive(false);
    }

}
