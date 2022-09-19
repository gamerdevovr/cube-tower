using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.GetString("music").Equals("No"))
            GetComponent<AudioSource>().Play();
    }
}
