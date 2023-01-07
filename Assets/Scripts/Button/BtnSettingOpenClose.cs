using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingOpenClose : MonoBehaviour
{
    public GameObject PodlozhkaSetting;
    public GameObject BtnSound;

    public void ClickOpen()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        PodlozhkaSetting.SetActive(true);
    }

    public void ClickClose()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        PodlozhkaSetting.SetActive(false);
    }
}
