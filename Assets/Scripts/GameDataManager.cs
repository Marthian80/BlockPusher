using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
        
    public Color[] GameColors = new Color[4];

    public Color PlayerColor { get; private set; }

    public int PointsTotal { get; private set; }

    public bool IsGameActive { get; private set; }

    private DateTime appStartTime;
    private const string gameName = "Cube Pusher";
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetColors(Color player)
    {
        PlayerColor = player;
    }

    public void AddPoints(int points)
    {
        PointsTotal += points;
    }

    public void StartClock()
    {
        appStartTime = DateTime.Now;
    }

    public void SetGameInActive()
    {
        IsGameActive = false;
    }

    public void ActivateGame()
    {
        IsGameActive = true;
    }

    //Save game data
    public void SaveGameData()
    {        
        SaveData data = new SaveData();
                
        data.GameName = gameName;
        data.TimeStamp = DateTime.Now.TimeOfDay.ToString();
        data.TimePlayed = (DateTime.Now - appStartTime).ToString();
        data.TotalScore = PointsTotal;

        string dataJson = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + @"/cubepushersavefile.json", dataJson);
    }

    [System.Serializable]
    class SaveData
    {
        public string GameName;

        public string TimeStamp;

        public string TimePlayed;

        public int TotalScore;
    }
}
