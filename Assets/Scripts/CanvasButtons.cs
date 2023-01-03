using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using TMPro;

public class CanvasButtons : MonoBehaviour
{

    public Sprite musicOn, musicOff;
    public GameObject fonMusic;
    public GameObject bestResult;
    public GameObject dRes;

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

    public void TapShare()
    {
        bestResult.SetActive(true);
        dRes.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("score").ToString();
        StartCoroutine(TakeScreenshotAndShare());    
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        bestResult.SetActive(false);
        string Message = "I've had some success in Sky Cubes - " + PlayerPrefs.GetInt("score").ToString() + " cubes uphill!!! Who will beat my record?";

        new NativeShare().AddFile(filePath)
            .SetSubject("New result in Sky Cubes").SetText(Message).SetUrl("https://www.facebook.com/profile.php?id=100088822786759")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

    }

    //private IEnumerable ShowBestResultforShare(GameObject bRes)
    //{
    //    Debug.Log(bRes.name);
    //    //bRes.SetActive(true);
    //    //yield return new WaitForSeconds(2);
    //    //yield return StartCoroutine(TakeScreenshotAndShare(_message));
    //    //bRes.SetActive(false);
    //}
}
