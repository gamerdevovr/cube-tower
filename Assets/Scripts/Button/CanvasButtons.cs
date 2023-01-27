using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using TMPro;

public class CanvasButtons : MonoBehaviour
{

    [SerializeField] private GameObject _victory;

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

    public void SetVictory()
    {
        StartCoroutine(StartVictory());
    }

    IEnumerator StartVictory()
    {
        _victory.SetActive(true);
        _victory.GetComponent<AudioSource>().Play();
        while (true)
        {
            _victory.GetComponent<AudioSource>().volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
            if (_victory.GetComponent<AudioSource>().volume > 0.95f)
                break;
        }
        yield return new WaitForSeconds(11);
        _victory.SetActive(false);
        _victory.GetComponent<AudioSource>().volume = 0f;
    }

}
