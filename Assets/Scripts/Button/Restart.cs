using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject                 _podlozhka, 
                                                        _btnSound;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();

        _podlozhka.SetActive(true);
    }

    public void BtnYes()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        StartCoroutine(StartScena("Main"));
    }

    public void BtnNo()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            _btnSound.GetComponent<AudioSource>().Play();
        _podlozhka.SetActive(false);
    }

    IEnumerator StartScena(string nameScena)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(nameScena);
        }
    }

}
