using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExit : MonoBehaviour
{

    public GameObject podlozhka, BtnSound;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        podlozhka.SetActive(true);
    }

    public void BtnYes()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void BtnNo()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        podlozhka.SetActive(false);
    }
}
