using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play_Pause : MonoBehaviour
{
    public Sprite spritePlay, spritePause;
    private bool pause;
    public GameObject BtnSound, FonMusic;


    void Start()
    {
        pause = false;
        GetComponent<Image>().sprite = spritePause;
    }

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        if (!pause)
        {
            GetComponent<Image>().sprite = spritePlay;
            Time.timeScale = 0;
            pause = true;
            FonMusic.GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<Image>().sprite = spritePause;
            Time.timeScale = 1;
            pause = false;
            if (PlayerPrefs.GetString("music").Equals("Yes"))
                FonMusic.GetComponent<AudioSource>().Play();
        }
    }

    public bool GetStatusPause()
    {
        return pause;
    }
}
