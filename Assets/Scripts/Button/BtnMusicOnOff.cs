using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMusicOnOff : MonoBehaviour
{
    public Sprite musicOn, musicOff, BtnMusicSoundOn, BtnMusicSoundOff;
    public GameObject Music, _BtnMusicOnOff, fonMusic, BtnSound;


    public void Start()
    {
        if (!PlayerPrefs.GetString("music").Equals("Yes"))
        {
            Music.GetComponent<Image>().sprite = musicOff;
            _BtnMusicOnOff.GetComponent<Image>().sprite = BtnMusicSoundOff;
        }
        else
        {
            Music.GetComponent<Image>().sprite = musicOn;
            _BtnMusicOnOff.GetComponent<Image>().sprite = BtnMusicSoundOn;
        }
    }


    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        if (PlayerPrefs.GetString("music").Equals("Yes"))
        {
            PlayerPrefs.SetString("music", "No");
            Music.GetComponent<Image>().sprite = musicOff;
            _BtnMusicOnOff.GetComponent<Image>().sprite = BtnMusicSoundOff;
            fonMusic.GetComponent<AudioSource>().Stop();
        }
        else
        {
            PlayerPrefs.SetString("music", "Yes");
            Music.GetComponent<Image>().sprite = musicOn;
            _BtnMusicOnOff.GetComponent<Image>().sprite = BtnMusicSoundOn;
            fonMusic.GetComponent<AudioSource>().Play();
        }
    }
}
