using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingOpenClose : MonoBehaviour
{
    [SerializeField] private GameObject     _podlozhkaSetting,
                                    _btnSound,
                                    _logo,
                                    _tapToPlay,
                                    _bestResultForShare, 
                                    _podlozhkaResult;

    public void ClickOpen()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        _bestResultForShare.SetActive(false);
        _podlozhkaResult.GetComponent<ShowResult>().CloseForm();

        if (_logo.activeSelf)
        {
            _logo.GetComponent<ClosedObjects>().SetSettingClosing(true);
            _logo.SetActive(false);
        }

        if (_tapToPlay.activeSelf)
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetSettingClosing(true);
            _logo.SetActive(false);
        }


        _podlozhkaSetting.SetActive(true);
    }

    public void ClickClose()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        _podlozhkaSetting.SetActive(false);

        if (_logo.GetComponent<ClosedObjects>().GetSettingClosing())
        {
            _logo.GetComponent<ClosedObjects>().SetSettingClosing(false);
            _logo.SetActive(true);
        }

        if (_tapToPlay.GetComponent<ClosedObjects>().GetSettingClosing())
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetSettingClosing(false);
            _tapToPlay.SetActive(true);
        }
    }
}
