using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExit : MonoBehaviour
{

    public GameObject podlozhka;

    public void Click()
    {
        podlozhka.SetActive(true);
    }

    public void BtnYes()
    {
        Application.Quit();
    }

    public void BtnNo()
    {
        podlozhka.SetActive(false);
    }
}
