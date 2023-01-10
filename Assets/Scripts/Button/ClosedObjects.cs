using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedObjects : MonoBehaviour
{
    public bool closing = false;

    public GameObject logo, tapToPlay;

    public void SetClosing(bool cl)
    {
        closing = cl;   
    }

    public bool GetClosing()
    {
        return closing;
    }

    public void SetVisibleObjects()
    {
        if (closing)
        {
            logo.SetActive(false);
            tapToPlay.SetActive(false);
        }
        else
        {
            logo.SetActive(true);
            tapToPlay.SetActive(true);
        }
    }
}
