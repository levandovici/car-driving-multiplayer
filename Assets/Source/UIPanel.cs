using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public bool Visible
    {
        get
        {
            return this.gameObject.activeSelf;
        }
    }



    public void Show()
    {
        SetActive(true);
    }

    public void Hide()
    {
        SetActive(false);
    }



    private void SetActive(bool visible)
    {
        this.gameObject.SetActive(visible);
    }
}
