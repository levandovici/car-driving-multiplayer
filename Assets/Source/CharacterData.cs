using System.Runtime.Serialization;
using UnityEngine;
using System;
using LimonadoEntertainment.Data;
using System.Text.Json.Serialization;



[Serializable]
public class CharacterData
{
    private float _position_x;

    private float _position_y;

    private float _position_z;


    private float _rotation_x;

    private float _rotation_y;

    private float _rotation_z;



    private float _fl_position_x;

    private float _fl_position_y;

    private float _fl_position_z;


    private float _fl_rotation_x;

    private float _fl_rotation_y;

    private float _fl_rotation_z;



    private float _fr_position_x;

    private float _fr_position_y;

    private float _fr_position_z;


    private float _fr_rotation_x;

    private float _fr_rotation_y;

    private float _fr_rotation_z;



    private float _bl_position_x;

    private float _bl_position_y;

    private float _bl_position_z;


    private float _bl_rotation_x;

    private float _bl_rotation_y;

    private float _bl_rotation_z;



    private float _br_position_x;

    private float _br_position_y;

    private float _br_position_z;


    private float _br_rotation_x;

    private float _br_rotation_y;

    private float _br_rotation_z;


    private bool _lights;



    public float PositionX
    {
        get
        {
            return _position_x;
        }

        set
        {
            _position_x = value;
        }
    }

    public float PositionY
    {
        get
        {
            return _position_y;
        }

        set
        {
            _position_y = value;
        }
    }

    public float PositionZ
    {
        get
        {
            return _position_z;
        }

        set
        {
            _position_z = value;
        }
    }


    public float RotationX
    {
        get
        {
            return _rotation_x;
        }

        set
        {
            _rotation_x = value;
        }
    }

    public float RotationY
    {
        get
        {
            return _rotation_y;
        }

        set
        {
            _rotation_y = value;
        }
    }

    public float RotationZ
    {
        get
        {
            return _rotation_z;
        }

        set
        {
            _rotation_z = value;
        }
    }



    public float FLPositionX
    {
        get
        {
            return _fl_position_x;
        }

        set
        {
            _fl_position_x = value;
        }
    }

    public float FLPositionY
    {
        get
        {
            return _fl_position_y;
        }

        set
        {
            _fl_position_y = value;
        }
    }

    public float FLPositionZ
    {
        get
        {
            return _fl_position_z;
        }

        set
        {
            _fl_position_z = value;
        }
    }


    public float FLRotationX
    {
        get
        {
            return _fl_rotation_x;
        }

        set
        {
            _fl_rotation_x = value;
        }
    }

    public float FLRotationY
    {
        get
        {
            return _fl_rotation_y;
        }

        set
        {
            _fl_rotation_y = value;
        }
    }

    public float FLRotationZ
    {
        get
        {
            return _fl_rotation_z;
        }

        set
        {
            _fl_rotation_z = value;
        }
    }



    public float FRPositionX
    {
        get
        {
            return _fr_position_x;
        }

        set
        {
            _fr_position_x = value;
        }
    }

    public float FRPositionY
    {
        get
        {
            return _fr_position_y;
        }

        set
        {
            _fr_position_y = value;
        }
    }

    public float FRPositionZ
    {
        get
        {
            return _fr_position_z;
        }

        set
        {
            _fr_position_z = value;
        }
    }


    public float FRRotationX
    {
        get
        {
            return _fr_rotation_x;
        }

        set
        {
            _fr_rotation_x = value;
        }
    }

    public float FRRotationY
    {
        get
        {
            return _fr_rotation_y;
        }

        set
        {
            _fr_rotation_y = value;
        }
    }

    public float FRRotationZ
    {
        get
        {
            return _fr_rotation_z;
        }

        set
        {
            _fr_rotation_z = value;
        }
    }



    public float BLPositionX
    {
        get
        {
            return _bl_position_x;
        }

        set
        {
            _bl_position_x = value;
        }
    }

    public float BLPositionY
    {
        get
        {
            return _bl_position_y;
        }

        set
        {
            _bl_position_y = value;
        }
    }

    public float BLPositionZ
    {
        get
        {
            return _bl_position_z;
        }

        set
        {
            _bl_position_z = value;
        }
    }


    public float BLRotationX
    {
        get
        {
            return _bl_rotation_x;
        }

        set
        {
            _bl_rotation_x = value;
        }
    }

    public float BLRotationY
    {
        get
        {
            return _bl_rotation_y;
        }

        set
        {
            _bl_rotation_y = value;
        }
    }

    public float BLRotationZ
    {
        get
        {
            return _bl_rotation_z;
        }

        set
        {
            _bl_rotation_z = value;
        }
    }



    public float BRPositionX
    {
        get
        {
            return _br_position_x;
        }

        set
        {
            _br_position_x = value;
        }
    }

    public float BRPositionY
    {
        get
        {
            return _br_position_y;
        }

        set
        {
            _br_position_y = value;
        }
    }

    public float BRPositionZ
    {
        get
        {
            return _br_position_z;
        }

        set
        {
            _br_position_z = value;
        }
    }


    public float BRRotationX
    {
        get
        {
            return _br_rotation_x;
        }

        set
        {
            _br_rotation_x = value;
        }
    }

    public float BRRotationY
    {
        get
        {
            return _br_rotation_y;
        }

        set
        {
            _br_rotation_y = value;
        }
    }

    public float BRRotationZ
    {
        get
        {
            return _br_rotation_z;
        }

        set
        {
            _br_rotation_z = value;
        }
    }


    public bool Lights
    {
        get
        {
            return _lights;
        }

        set
        {
            _lights = value;
        }
    }



    [JsonIgnore]
    public CharacterData Clone => new CharacterData(PositionX, PositionY, PositionZ, RotationX, RotationY, RotationZ,
                                                    FLPositionX, FLPositionY, FLPositionZ, FLRotationX, FLRotationY, FLRotationZ,
                                                    FRPositionX, FRPositionY, FRPositionZ, FRRotationX, FRRotationY, FRRotationZ,
                                                    BLPositionX, BLPositionY, BLPositionZ, BLRotationX, BLRotationY, BLRotationZ,
                                                    BRPositionX, BRPositionY, BRPositionZ, BRRotationX, BRRotationY, BRRotationZ, Lights);



    public CharacterData()
    {

    }

    public CharacterData(float px, float py, float pz, float rx, float ry, float rz,
                         float flpx, float flpy, float flpz, float flrx, float flry, float flrz,
                         float frpx, float frpy, float frpz, float frrx, float frry, float frrz,
                         float blpx, float blpy, float blpz, float blrx, float blry, float blrz,
                         float brpx, float brpy, float brpz, float brrx, float brry, float brrz, bool lights)
    {
        PositionX = px;

        PositionY = py;

        PositionZ = pz;


        RotationX = rx;

        RotationY = ry;

        RotationZ = rz;



        FLPositionX = flpx;

        FLPositionY = flpy;

        FLPositionZ = flpz;


        FLRotationX = flrx;

        FLRotationY = flry;

        FLRotationZ = flrz;



        FRPositionX = frpx;

        FRPositionY = frpy;

        FRPositionZ = frpz;


        FRRotationX = frrx;

        FRRotationY = frry;

        FRRotationZ = frrz;



        BLPositionX = blpx;

        BLPositionY = blpy;

        BLPositionZ = blpz;


        BLRotationX = blrx;

        BLRotationY = blry;

        BLRotationZ = blrz;



        BRPositionX = brpx;

        BRPositionY = brpy;

        BRPositionZ = brpz;


        BRRotationX = brrx;

        BRRotationY = brry;

        BRRotationZ = brrz;


        Lights = lights;
    }
}