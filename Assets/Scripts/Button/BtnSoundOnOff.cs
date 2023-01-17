using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSoundOnOff : MonoBehaviour
{
    [SerializeField] private Sprite             _soundOn, 
                                                _soundOff, 
                                                _btnMusicSoundOn, 
                                                _btnMusicSoundOff;
    
    [SerializeField] private GameObject         _sound, 
                                                _btnSoundOnOff, 
                                                _btnSound;

    public void Start()
    {
        if (!PlayerPrefs.GetString("sound").Equals("Yes"))
        {
            _sound.GetComponent<Image>().sprite = _soundOff;
            _btnSoundOnOff.GetComponent<Image>().sprite = _btnMusicSoundOff;
        }
        else
        {
            _sound.GetComponent<Image>().sprite = _soundOn;
            _btnSoundOnOff.GetComponent<Image>().sprite = _btnMusicSoundOn;
        }
    }


    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        if (PlayerPrefs.GetString("sound").Equals("Yes"))
        {
            PlayerPrefs.SetString("sound", "No");
            _sound.GetComponent<Image>().sprite = _soundOff;
            _btnSoundOnOff.GetComponent<Image>().sprite = _btnMusicSoundOff;
        }
        else
        {
            PlayerPrefs.SetString("sound", "Yes");
            _sound.GetComponent<Image>().sprite = _soundOn;
            _btnSoundOnOff.GetComponent<Image>().sprite = _btnMusicSoundOn;
        }
    }
}
