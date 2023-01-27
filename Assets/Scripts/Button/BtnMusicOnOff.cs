using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMusicOnOff : MonoBehaviour
{
    [SerializeField] private Sprite         _musicOn,
                                            _musicOff, 
                                            _btnMusicSoundOn, 
                                            _btnMusicSoundOff;

    [SerializeField] private GameObject     _music, 
                                            _btnMusicOnOff,
                                            _fonMusic, 
                                            _btnSound;


    public void Start()
    {
        if (!PlayerPrefs.GetString("music").Equals("Yes"))
        {
            _music.GetComponent<Image>().sprite = _musicOff;
            _btnMusicOnOff.GetComponent<Image>().sprite = _btnMusicSoundOff;
        }
        else
        {
            _music.GetComponent<Image>().sprite = _musicOn;
            _btnMusicOnOff.GetComponent<Image>().sprite = _btnMusicSoundOn;
        }
    }


    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        if (PlayerPrefs.GetString("music").Equals("Yes"))
        {
            PlayerPrefs.SetString("music", "No");
            _music.GetComponent<Image>().sprite = _musicOff;
            _btnMusicOnOff.GetComponent<Image>().sprite = _btnMusicSoundOff;
            _fonMusic.GetComponent<AudioSource>().Stop();
        }
        else
        {
            PlayerPrefs.SetString("music", "Yes");
            _music.GetComponent<Image>().sprite = _musicOn;
            _btnMusicOnOff.GetComponent<Image>().sprite = _btnMusicSoundOn;
            _fonMusic.GetComponent<AudioSource>().Play();
        }
    }

}
