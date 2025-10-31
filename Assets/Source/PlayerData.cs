using System;

[Serializable]
public class PlayerData
{
    public float sfxVolume;

    public float musicVolume;

    public int money = 0;

    public int carIndex = 0;

    public CarInfo[] carsInfo = new CarInfo[0];



    public PlayerData(float sfxVolume = 1f, float musicVolume = 1f, int money = 0, int carIndex = 0)
    {
        this.sfxVolume = sfxVolume;

        this.musicVolume = musicVolume;

        this.money = money;

        this.carIndex = carIndex;

        carsInfo = new CarInfo[CarSetup.CarsCount];

        carsInfo[0] = new CarInfo(true);

        for(int i = 1; i < CarSetup.CarsCount; i++)
        {
            carsInfo[i] = new CarInfo(false);
        }
    }
}
