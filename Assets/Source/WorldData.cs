using System.Runtime.Serialization;
using UnityEngine;
using System;
using System.Text.Json.Serialization;

using LimonadoEntertainment.Data;



[Serializable]
public class WorldData
{
    public WorldData Clone => new WorldData();



    public WorldData()
    {

    }
}