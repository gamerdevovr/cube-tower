using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{

    public Sprite musicOn, musicOff;

    public void Start()
    {
        if (PlayerPrefs.GetString("music") != "Yes" && gameObject.name == "Music")
            GetComponent<Image>().sprite = musicOff;
    }

    IEnumerator StartScenaShop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Shop");
        }
    }

    IEnumerator CloseScenaShop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator RestartScena()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetFloat("nowCountCubes", 0);
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(RestartScena());
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadShop()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(StartScenaShop());
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
            StartCoroutine(CloseScenaShop());
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
        }
        else 
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = musicOff;
        }
    }
}
