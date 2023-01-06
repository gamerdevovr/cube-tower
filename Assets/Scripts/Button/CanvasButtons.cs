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

    public void TapShare()
    {
        if (PlayerPrefs.GetString("sound") != "No")
            GetComponent<AudioSource>().Play();

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

    
}
