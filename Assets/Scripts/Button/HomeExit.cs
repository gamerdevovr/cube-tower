using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExit : MonoBehaviour
{

    [SerializeField] private GameObject         _podlozhka, 
                                                _btnSound;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        _podlozhka.SetActive(true);
    }

    public void BtnYes()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void BtnNo()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        _podlozhka.SetActive(false);
    }

}
