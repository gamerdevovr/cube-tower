using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{

    public Sprite musicOn, musicOff;
    public GameObject fonMusic;

    public void Start()
    {
        if (PlayerPrefs.GetString("music") != "Yes" && gameObject.name == "Music")
            GetComponent<Image>().sprite = musicOff;
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
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartScena("Main"));
        }
        else 
        {
            StartCoroutine(StartScena("Main"));
        }
    }

    public void LoadShop()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartScena("Shop"));
        }
        else 
        {
            SceneManager.LoadScene("Shop");
        }
    }

    public void CloseShop()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartScena("Main"));
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void LoadFacebook()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        Application.OpenURL("https://facebook.com");
    }

    public void MusicWork()
    {
        if (PlayerPrefs.GetString("music") == "No")
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("music","Yes");
            GetComponent<Image>().sprite = musicOn;
            fonMusic.GetComponent<AudioSource>().Play();
        }
        else 
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = musicOff;
            fonMusic.GetComponent<AudioSource>().Stop();
        }
    }

    public void ClearScore()
    {
        PlayerPrefs.SetInt("score", 0);
        StartCoroutine(StartScena("Main"));
    }
}
