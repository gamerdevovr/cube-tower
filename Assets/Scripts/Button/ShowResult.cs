using UnityEngine;
using TMPro;

public class ShowResult : MonoBehaviour
{

    public GameObject podlozhka, result1, result2, result3, close;
    public GameObject Logo;
    public GameObject TapToPlay;

    public void CloseForm()
    {
        podlozhka.SetActive(false);
    }

    public void SetResultOnForm()
    {
       
        result1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result1").ToString();
        result2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result2").ToString();
        result3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("result3").ToString();

    }

}
