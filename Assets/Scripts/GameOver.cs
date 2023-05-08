using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void SetNewResult()
    {

        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("result1"))
        {
            PlayerPrefs.SetInt("result3", PlayerPrefs.GetInt("result2"));
            PlayerPrefs.SetInt("result2", PlayerPrefs.GetInt("result1"));
            PlayerPrefs.SetInt("result1", PlayerPrefs.GetInt("score"));
        }

    }
}
