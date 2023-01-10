using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnResult : MonoBehaviour
{
    public GameObject PodlozhkaResult;
    public GameObject BtnSound;
    public ClosedObjects closed;

    public void Click()
    {
        if (PlayerPrefs.GetString("sound").Equals("Yes"))
            BtnSound.GetComponent<AudioSource>().Play();

        PodlozhkaResult.GetComponent<ShowResult>().SetResultOnForm();
        PodlozhkaResult.SetActive(true);

        closed.SetClosing(true);
        closed.SetVisibleObjects();
    }

}
