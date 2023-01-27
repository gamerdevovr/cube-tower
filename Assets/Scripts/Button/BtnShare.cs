using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class BtnShare : MonoBehaviour
{
    [SerializeField] private GameObject             _bestResult, 
                                                    _dRes, 
                                                    _btnSound,
                                                    _logo, 
                                                    _tapToPlay,
                                                    _settingOpenForm, 
                                                    _podlozhkaResult;


    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        _settingOpenForm.GetComponent<BtnSettingOpenClose>().ClickClose();
        _podlozhkaResult.GetComponent<ShowResult>().CloseForm();

        if (_logo.activeSelf)
        {
            _logo.GetComponent<ClosedObjects>().SetShareClosing(true);
            _logo.SetActive(false);
        }

        if (_tapToPlay.activeSelf)
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetShareClosing(true);
            _logo.SetActive(false);
        }


        _bestResult.SetActive(true);
        _dRes.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("score").ToString();
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

        _bestResult.SetActive(false);
        string Message = "I've had some success in Sky Cubes - " + PlayerPrefs.GetInt("score").ToString() + " cubes uphill!!! Who will beat my record?";

        new NativeShare().AddFile(filePath)
            .SetSubject("New result in Sky Cubes").SetText(Message).SetUrl("https://www.facebook.com/profile.php?id=100088822786759")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        if (_logo.activeSelf)
        {
            _logo.GetComponent<ClosedObjects>().SetShareClosing(false);
            _logo.SetActive(true);
        }

        if (_tapToPlay.activeSelf)
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetShareClosing(false);
            _logo.SetActive(true);
        }
    }

}
