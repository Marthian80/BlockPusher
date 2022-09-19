using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
        
    public Color[] GameColors = new Color[4];

    public Color PlayerColor { get; private set; }

    public int PointsTotal { get; private set; }
    
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
}
