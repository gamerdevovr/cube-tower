using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject BtnSound;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        StartCoroutine(StartScena("Main"));
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
