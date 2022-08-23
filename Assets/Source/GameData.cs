using UnityEngine;
using System;

using LimonadoEntertainment.Data;



[Serializable]
public class GameData
{
    public CharacterData character_data;



    public CharacterData CharacterData
    {
        get
        {
            return character_data;
        }

        set
        {
            character_data = value;
        }
    }



    public GameData Clone => new GameData(CharacterData.Clone);



    public GameData(CharacterData characterData)
    {
        CharacterData = characterData;
    }

    public GameData()
    {

    }
}