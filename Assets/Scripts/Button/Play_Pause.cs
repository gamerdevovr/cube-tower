using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play_Pause : MonoBehaviour
{
    [SerializeField] private Sprite                 _spritePlay, 
                                                    _spritePause;
    
    [SerializeField] private GameObject             _btnSound, 
                                                    _fonMusic;
    
    private bool                                    _pause;


    void Start()
    {
        _pause = false;
        GetComponent<Image>().sprite = _spritePause;
    }

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        if (!_pause)
        {
            GetComponent<Image>().sprite = _spritePlay;
            Time.timeScale = 0;
            _pause = true;
            _fonMusic.GetComponent<AudioSource>().Stop();
        }
        else
        {
            GetComponent<Image>().sprite = _spritePause;
            Time.timeScale = 1;
            _pause = false;
            if (PlayerPrefs.GetString("music").Equals("Yes"))
                _fonMusic.GetComponent<AudioSource>().Play();
        }
    }

    public bool GetStatusPause()
    {
        return _pause;
    }

}
