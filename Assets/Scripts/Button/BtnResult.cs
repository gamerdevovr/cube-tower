using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnResult : MonoBehaviour
{
    [SerializeField] private GameObject     _podlozhkaResult,
                                            _btnSound,
                                            _logo,
                                            _tapToPlay,
                                            _bestResultForShare,
                                            _settingOpenForm;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        _bestResultForShare.SetActive(false);
        _settingOpenForm.GetComponent<BtnSettingOpenClose>().ClickClose();

        if (_logo.activeSelf)
        {
            _logo.GetComponent<ClosedObjects>().SetResultClosing(true);
            _logo.SetActive(false);
        }

        if (_tapToPlay.activeSelf)
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetResultClosing(true);
            _logo.SetActive(false);
        }

        _podlozhkaResult.GetComponent<ShowResult>().SetResultOnForm();
        _podlozhkaResult.SetActive(true);
    }

}
