using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using TMPro;

public class CanvasButtons : MonoBehaviour
{

    public Sprite musicOn, musicOff, soundOn, soundOff, BtnMusicSoundOn, BtnMusicSoundOff;
    public GameObject fonMusic, Music, Sound, BtnMusicOnOff, BtnSoundOnOff;
    public GameObject bestResult;
    public GameObject dRes;

    public void Start()
    {


    }

    IEnumerator StartScena(string nameScena)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(nameScena);
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetFloat("nowCountCubes", 0);
        if (PlayerPrefs.GetString("sound") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartScena("Main"));
        }
        else 
        {
            StartCoroutine(StartScena("Main"));
        }
    }

    public void LoadFacebook()
    {
        if (PlayerPrefs.GetString("sound") != "No")
            GetComponent<AudioSource>().Play();
        Application.OpenURL("https://facebook.com");
    }

    public void ClearScore()
    {
        PlayerPrefs.SetInt("score", 0);
        StartCoroutine(StartScena("Main"));
    }

    public void MaxResult()
    {
        PlayerPrefs.SetInt("score", 201);
        StartCoroutine(StartScena("Main"));
    }    
}
