using System;
using UnityEngine;

[Serializable]
public class CarInfo
{
    public bool isBought = false;



    public CarInfo(bool isBought = false)
    {
        this.isBought = isBought;
    }
}
