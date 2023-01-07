using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSoundOnOff : MonoBehaviour
{
    public Sprite soundOn, soundOff, BtnMusicSoundOn, BtnMusicSoundOff;
    public GameObject Sound, _BtnSoundOnOff, BtnSound;

    public void Start()
    {
        if (!PlayerPrefs.GetString("sound").Equals("Yes"))
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
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        if (PlayerPrefs.GetString("sound").Equals("Yes"))
        {
            PlayerPrefs.SetString("sound", "No");
            Sound.GetComponent<Image>().sprite = soundOff;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOff;
        }
        else
        {
            PlayerPrefs.SetString("sound", "Yes");
            Sound.GetComponent<Image>().sprite = soundOn;
            _BtnSoundOnOff.GetComponent<Image>().sprite = BtnMusicSoundOn;
        }
    }
}
