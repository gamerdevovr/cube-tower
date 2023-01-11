using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class BtnShare : MonoBehaviour
{
    public GameObject bestResult, dRes, BtnSound;
    public GameObject logo, tapToPlay;
    public GameObject SettingOpenForm, PodlozhkaResult;


    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        SettingOpenForm.GetComponent<BtnSettingOpenClose>().ClickClose();
        PodlozhkaResult.GetComponent<ShowResult>().CloseForm();

        if (logo.activeSelf)
        {
            logo.GetComponent<ClosedObjects>().SetShareClosing(true);
            logo.SetActive(false);
        }

        if (tapToPlay.activeSelf)
        {
            tapToPlay.GetComponent<ClosedObjects>().SetShareClosing(true);
            logo.SetActive(false);
        }


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

        if (logo.activeSelf)
        {
            logo.GetComponent<ClosedObjects>().SetShareClosing(false);
            logo.SetActive(true);
        }

        if (tapToPlay.activeSelf)
        {
            tapToPlay.GetComponent<ClosedObjects>().SetShareClosing(false);
            logo.SetActive(true);
        }
    }
}
