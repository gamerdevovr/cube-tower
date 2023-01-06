using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play_Pause : MonoBehaviour
{
    public Sprite spritePlay, spritePause;
    private bool pause;
    
    void Start()
    {
        pause = false;
        GetComponent<Image>().sprite = spritePause;
    }

    public void Click()
    {
        if (!pause)
        {
            GetComponent<Image>().sprite = spritePlay;
            Time.timeScale = 0;
            pause = true;
        }
        else
        {
            GetComponent<Image>().sprite = spritePause;
            Time.timeScale = 1;
            pause = false;
        }
    }

    public bool GetStatusPause()
    {
        return pause;
    }
}
