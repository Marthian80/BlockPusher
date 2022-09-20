using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{
    public GameObject GameBall;    
    public GameObject EndGoal;
    public GameObject[] PointObstacles = new GameObject[3];
    public GameObject[] Obstacles = new GameObject[12];
    public float[] xPositionsGoal = new float[2] { xRangeMax, -49.6f };

    public GameObject GameOverUI;
    public GameObject GameWonUI;
    public TextMeshProUGUI Points;
    public TextMeshProUGUI Timer;

    //Local var for testing
    private bool IsGameActive;

    private int countDown = 61;

    private const float obstaclePositionY = 0.6f;
    private const float ballPosY = 0.7f;
    private const float pointsPosY = 0.15f;
    private const float endGoalPosY = 4.8f;
    private const float xRangeMax = 33.5f;
    private const float zRangeMax = 36.0f;        
    private const int maxNumberOfObjectsOnRow = 10;
        
    // Start is called before the first frame update
    void Start()
    {           
        GenerateGameBall();
        if (GameDataManager.Instance != null)
        {
            UpdateScore();
            GameDataManager.Instance.StartClock();
            GameDataManager.Instance.ActivateGame();
        }
        else
        {
            this.IsGameActive = true;
        }
        //Start scrolling game
        StartCoroutine(GeneratePointsAndObstaclesOnCount());
    }       

    public void ExitGameOnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGameOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGameOnClick()
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.ActivateGame();
        }
        StartCoroutine(GeneratePointsAndObstaclesOnCount());
    }

    public void PauseGameOnClick()
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.SetGameInActive();
        }
    }

    public void GameOver()
    {
        GameOverUI.gameObject.SetActive(true);                
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.SetGameInActive();
            GameDataManager.Instance.SaveGameData();
        }
        else
        {
            this.IsGameActive = false;
        }
    }

    public void GameWon()
    {
        GameWonUI.gameObject.SetActive(true);        
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.SetGameInActive();
            GameDataManager.Instance.SaveGameData();
        }
        else
        {
            this.IsGameActive = false;
        }
    }

    public void UpdateScore()
    {
        Points.text = GameDataManager.Instance.PointsTotal + " Points";
    }

    private void GenerateGameBall()
    {
        var posX = Random.Range(-xRangeMax, xRangeMax);
        var posY = obstaclePositionY;
        var posZ = -zRangeMax+2;         

        Instantiate(GameBall, new Vector3(posX, posY, posZ), GameBall.transform.rotation);
    }

    private IEnumerator GeneratePointsAndObstaclesOnCount()
    {
        //Use local variable for quick testing
        while (GameDataManager.Instance != null && GameDataManager.Instance.IsGameActive || this.IsGameActive)
        {
            yield return new WaitForSeconds(1);

            if (countDown > 0)
            {
                //Spawn EndGoal and Obstacles every 15 seconds that scrolls down on the side of the board
                if (countDown % 15 == 0)
                {
                    SpawnEndGoal();
                    GenerateObjects(Random.Range(1, 7), Obstacles, 12 - countDown / 10, pointsPosY);
                }
                //Spawn Points every 10 seconds, with increasing speed
                if (countDown % 10 == 0)
                {
                    GenerateObjects(Random.Range(1, 5), PointObstacles, 10 - countDown / 10, obstaclePositionY);
                }               

                Timer.text = (countDown--).ToString();
            }            
            else
            {
                GameOver();
            }            
        }
    } 

    private Vector3 CalculatePosition(float xPosition, float offsetX, float yPosition, float zPosition = zRangeMax)
    {
        var posX = Random.Range(xPosition, offsetX);
        var posY = yPosition; 
        var posZ = zPosition;
        return new Vector3(posX, posY, posZ);
    }

    private int GenerateRandomNumberWithExlusion(int minRange, int maxRange, HashSet<int> exclusion)
    {
        var range = Enumerable.Range(minRange, maxRange).Where(i => !exclusion.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(minRange, maxRange - exclusion.Count);
        return range.ElementAt(index);
    }

    private void SpawnEndGoal()
    {
        //Random spawn end left or right        
        Vector3 spawnPos = new Vector3(xPositionsGoal[Random.Range(0, xPositionsGoal.Length)], endGoalPosY, zRangeMax);

        var endGoal = Instantiate(EndGoal, spawnPos, EndGoal.transform.rotation);
        endGoal.GetComponent<MoveForward>().SetSpeed(5.0f);
        endGoal.GetComponent<MoveForward>().SetDirection(Vector3.right);
    }

    private void GenerateObjects(int numberOfObjects, GameObject[] objects, float speed, float yPostition)
    {
        var xOffset = CalculateXDistance(numberOfObjects);
        var xAxisPos = -xRangeMax;

        for(int i = 0; i < numberOfObjects; i++)
        {
            var generatedObject = objects[Random.Range(0, objects.Length)];
            var generatedObstacle = Instantiate(generatedObject, CalculatePosition(xAxisPos, xAxisPos + xOffset, yPostition), generatedObject.transform.rotation);
            if (generatedObstacle.transform.localEulerAngles.y == 0)
            {
                generatedObstacle.GetComponent<MoveForward>().SetDirection(Vector3.back);
            }
            else
            {
                generatedObstacle.GetComponent<MoveForward>().SetDirection(Vector3.right);
            }
            generatedObstacle.GetComponent<MoveForward>().SetSpeed(speed);
            xAxisPos += xOffset;
        }
    }

    private float CalculateXDistance(int divider)
    {
        return (xRangeMax * 2) / divider;
    }    
}
