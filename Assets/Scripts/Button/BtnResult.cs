using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnResult : MonoBehaviour
{
    public GameObject PodlozhkaResult;
    public GameObject BtnSound;
    public GameObject logo, tapToPlay;
    public GameObject BestResultForShare, settingOpenForm;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        BestResultForShare.SetActive(false);
        settingOpenForm.GetComponent<BtnSettingOpenClose>().ClickClose();

        if (logo.activeSelf)
        {
            logo.GetComponent<ClosedObjects>().SetResultClosing(true);
            logo.SetActive(false);
        }

        if (tapToPlay.activeSelf)
        {
            tapToPlay.GetComponent<ClosedObjects>().SetResultClosing(true);
            logo.SetActive(false);
        }

        PodlozhkaResult.GetComponent<ShowResult>().SetResultOnForm();
        PodlozhkaResult.SetActive(true);
    }

}
