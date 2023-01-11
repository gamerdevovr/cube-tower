using UnityEngine;
using TMPro;

public class ShowResult : MonoBehaviour
{

    public GameObject podlozhka, result1, result2, result3, close;

    public GameObject logo, tapToPlay;

    public void CloseForm()
    {
        podlozhka.SetActive(false);

        if (logo.GetComponent<ClosedObjects>().GetResultClosing())
        {
            logo.GetComponent<ClosedObjects>().SetResultClosing(false);
            logo.SetActive(true);
        }

        if (tapToPlay.GetComponent<ClosedObjects>().GetResultClosing())
        {
            tapToPlay.GetComponent<ClosedObjects>().SetResultClosing(false);
            tapToPlay.SetActive(true);
        }

    }

    public void SetResultOnForm()
    {
       
        result1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result1").ToString();
        result2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result2").ToString();
        result3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result3").ToString();

    }

}
