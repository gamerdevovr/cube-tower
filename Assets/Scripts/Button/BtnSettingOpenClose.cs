using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingOpenClose : MonoBehaviour
{
    public GameObject PodlozhkaSetting;

    public void ClickOpen()
    {
        PodlozhkaSetting.SetActive(true);
    }

    public void ClickClose()
    {
        PodlozhkaSetting.SetActive(false);
    }
}
