using UnityEngine;
using TMPro;

public class ShowResult : MonoBehaviour
{

    [SerializeField] private GameObject                 _podlozhka,
                                                        _result1,
                                                        _result2,
                                                        _result3,
                                                        _close,
                                                        _logo, 
                                                        _tapToPlay, 
                                                        _btnSound;

    public void CloseForm()
    {

        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        _podlozhka.SetActive(false);

        if (_logo.GetComponent<ClosedObjects>().GetResultClosing())
        {
            _logo.GetComponent<ClosedObjects>().SetResultClosing(false);
            _logo.SetActive(true);
        }

        if (_tapToPlay.GetComponent<ClosedObjects>().GetResultClosing())
        {
            _tapToPlay.GetComponent<ClosedObjects>().SetResultClosing(false);
            _tapToPlay.SetActive(true);
        }

    }

    public void SetResultOnForm()
    {
       
        _result1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result1").ToString();
        _result2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result2").ToString();
        _result3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result3").ToString();

    }

}
