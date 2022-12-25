using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{

    public Sprite musicOn, musicOff;
    public GameObject fonMusic, socialPodlozhka, socialClose, faceBook, twitter;

    public void Start()
    {
        if (gameObject.name.Equals("Music"))
        {
            if (!PlayerPrefs.GetString("music").Equals("Yes") && PlayerPrefs.GetString("music").Equals("No"))
                GetComponent<Image>().sprite = musicOff;
            else
                GetComponent<Image>().sprite = musicOn;
        }
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

    public void MaxResult()
    {
        PlayerPrefs.SetInt("score", 201);
        StartCoroutine(StartScena("Main"));
    }

    public void OpenSocial()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        socialPodlozhka.SetActive(true);
        socialClose.SetActive(true);
        faceBook.SetActive(true);
        twitter.SetActive(true);
    }

    public void CloseSocial()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
        socialPodlozhka.SetActive(false);
        socialClose.SetActive(false);
        faceBook.SetActive(false);
        twitter.SetActive(false);
    }
}
