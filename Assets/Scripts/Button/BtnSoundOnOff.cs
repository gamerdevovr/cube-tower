using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSoundOnOff : MonoBehaviour
{
    public Sprite soundOn, soundOff, BtnMusicSoundOn, BtnMusicSoundOff;
    public GameObject Sound, _BtnSoundOnOff;

    public void Start()
    {
        if (!PlayerPrefs.GetString("music").Equals("Yes"))
        {
            Sound.GetComponent<Image>().sprite = soundOff;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOff;
        }
        else
        {
            Sound.GetComponent<Image>().sprite = soundOn;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOn;
        }
    }


    public void Click()
    {
        if (PlayerPrefs.GetString("music").Equals("Yes"))
        {
            PlayerPrefs.SetString("music", "No");
            Sound.GetComponent<Image>().sprite = soundOff;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOff;
        }
        else
        {
            PlayerPrefs.SetString("music", "Yes");
            Sound.GetComponent<Image>().sprite = soundOn;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOn;
        }
    }
}
