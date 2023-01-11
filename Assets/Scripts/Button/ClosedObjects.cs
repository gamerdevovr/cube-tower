using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClosedObjects : MonoBehaviour
{
    [SerializeField] private bool closingResult = false;
    [SerializeField] private bool closingShare = false;
    [SerializeField] private bool closingSetting = false;

    public void SetResultClosing(bool cl)
    {
        closingResult = cl;   
    }

    public bool GetResultClosing()
    {
        return closingResult;
    }

    public void SetShareClosing(bool cl)
    {
        closingShare = cl;
    }

    public bool GetShareClosing()
    {
        return closingShare;
    }

    public void SetSettingClosing(bool cl)
    {
        closingShare = cl;
    }

    public bool GetSettingClosing()
    {
        return closingShare;
    }
}
