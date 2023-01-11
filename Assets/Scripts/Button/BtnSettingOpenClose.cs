using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingOpenClose : MonoBehaviour
{
    public GameObject PodlozhkaSetting;
    public GameObject BtnSound;
    public GameObject logo, tapToPlay;
    public GameObject BestResultForShare, PodlozhkaResult;

    public void ClickOpen()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        BestResultForShare.SetActive(false);
        PodlozhkaResult.GetComponent<ShowResult>().CloseForm();

        if (logo.activeSelf)
        {
            logo.GetComponent<ClosedObjects>().SetSettingClosing(true);
            logo.SetActive(false);
        }

        if (tapToPlay.activeSelf)
        {
            tapToPlay.GetComponent<ClosedObjects>().SetSettingClosing(true);
            logo.SetActive(false);
        }


        PodlozhkaSetting.SetActive(true);
    }

    public void ClickClose()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();
        PodlozhkaSetting.SetActive(false);

        if (logo.GetComponent<ClosedObjects>().GetSettingClosing())
        {
            logo.GetComponent<ClosedObjects>().SetSettingClosing(false);
            logo.SetActive(true);
        }

        if (tapToPlay.GetComponent<ClosedObjects>().GetSettingClosing())
        {
            tapToPlay.GetComponent<ClosedObjects>().SetSettingClosing(false);
            tapToPlay.SetActive(true);
        }
    }
}
