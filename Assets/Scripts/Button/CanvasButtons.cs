using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using TMPro;

public class CanvasButtons : MonoBehaviour
{

    IEnumerator StartScena(string nameScena)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(nameScena);
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
}
