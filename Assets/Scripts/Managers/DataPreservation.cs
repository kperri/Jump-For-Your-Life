using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataPreservation : MonoBehaviour
{

    public static DataPreservation data;

    int coins;

    public int _coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
        }
    }

    int boots;

    public int _boots
    {
        get
        {
            return boots;
        }
        set
        {
            boots = value;
        }
    }

    int[] highscores;

    public int[] _highscores
    {
        get
        {
            return highscores;
        }
        set
        {
            highscores = value;
        }
    }

    int deathCount;

    public int _deathCount
    {
        get
        {
            return deathCount;
        }
        set
        {
            deathCount = value;
        }
    }

    bool watchedBootsAD;

    public bool _watchedBootsAD
    {
        get
        {
            return watchedBootsAD;
        }
        set
        {
            watchedBootsAD = value;
        }
    }

    bool watchedCoinAD;

    public bool _watchedCoinAD
    {
        get
        {
            return watchedCoinAD;
        }
        set
        {
            watchedCoinAD = value;
        }
    }

    void Awake ()
    {
        if (data == null)
        {
            DontDestroyOnLoad (gameObject);
            data = this;
        }
        else if (data != this)
        {
            Destroy (gameObject);
        }
        highscores = new int[5];
        //for (int i = 0; i < highscores.Length; i++) {
        //    highscores[i] = 0;
        //}
        //Save ();
        LoadData ();
    }

    public void SaveData ()
    {
        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create (Application.persistentDataPath + "/playerData.dat");

        PlayerData data = new PlayerData (coins, boots, highscores, deathCount, watchedBootsAD, watchedCoinAD);

        bf.Serialize (file, data);
        file.Close ();
    }

    public void LoadData ()
    {
        if (File.Exists (Application.persistentDataPath + "/playerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open (Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize (file);
            file.Close ();

            coins = data.coinsCollected;
            boots = data.bootsAcquired;
            highscores = data.highscores;
            deathCount = data.deathCount;
            watchedBootsAD = data.watchedBootsAD;
            watchedCoinAD = data.watchedCoinsAD;
        }
    }

    public void ResetData (bool resetAll)
    {
        coins = 0;
        boots = 0;
        if (resetAll)
        {
            for (int i = 0; i < highscores.Length; i++)
            {
                highscores[i] = 0;
            }
            watchedBootsAD = false;
            watchedCoinAD = false;
        }
        SaveData ();
    }
}

[Serializable]
struct PlayerData
{

    public int coinsCollected;
    public int bootsAcquired;
    public int[] highscores;
    public int deathCount;
    public bool watchedBootsAD;
    public bool watchedCoinsAD;

    public PlayerData (int coins, int boots, int[] _highscores, int _deathCount, bool _watchedBootsAD, bool _watchedCoinsAD)
    {
        coinsCollected = coins;
        bootsAcquired = boots;
        highscores = _highscores;
        deathCount = _deathCount;
        watchedBootsAD = _watchedBootsAD;
        watchedCoinsAD = _watchedCoinsAD;
    }
}
