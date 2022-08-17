using System.Runtime.Serialization;
using UnityEngine;
using System;

using LimonadoEntertainment.Data;
using System.Text.Json.Serialization;



[Serializable]
public class GameData
{
    private CharacterData _character_data;



    public CharacterData CharacterData
    {
        get
        {
            return _character_data;
        }

        set
        {
            _character_data = value;
        }
    }



    [JsonIgnore]
    public GameData Clone => new GameData(CharacterData.Clone);



    public GameData(CharacterData characterData)
    {
        CharacterData = characterData;
    }

    public GameData()
    {

    }
}