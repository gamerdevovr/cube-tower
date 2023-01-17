using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClosedObjects : MonoBehaviour
{
    [SerializeField] private bool           _closingResult = false,
                                            _closingShare = false;

    public bool                             _clicked = false;

    public void SetResultClosing(bool cl)
    {
        _closingResult = cl;   
    }

    public bool GetResultClosing()
    {
        return _closingResult;
    }

    public void SetShareClosing(bool cl)
    {
        _closingShare = cl;
    }

    public bool GetShareClosing()
    {
        return _closingShare;
    }

    public void SetSettingClosing(bool cl)
    {
        _closingShare = cl;
    }

    public bool GetSettingClosing()
    {
        return _closingShare;
    }

    public void SetClicked(bool cl)
    {
        _clicked = cl;
    }

    public bool GetClicked()
    {
        return _clicked;
    }
}
