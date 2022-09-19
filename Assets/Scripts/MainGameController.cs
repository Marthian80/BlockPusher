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
    public GameObject FriendlyObstacle;
    public GameObject NeutralObstacle;
    public GameObject HostileObstacle;

    public GameObject GameOverUI;   

    public bool IsGameActive { get; private set; }

    private const float gamePiecePositionY = 1.6f;
    private const float ballPosY = 0.7f;
    private const float xRangeMax = 38.0f;
    private const float zRangeMax = 35.0f;
    private const int maxNumberOfObjectsOnRow = 10;

    // Start is called before the first frame update
    void Start()
    {

        //GenerateObstacles();
        this.IsGameActive = true;
        GenerateGameBall();
    }       

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGameOnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGameOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        GameOverUI.gameObject.SetActive(true);        
        IsGameActive = false;
    }

    private void GenerateGameBall()
    {
        var posX = Random.Range(-xRangeMax, xRangeMax);
        var posY = gamePiecePositionY;
        var posZ = -zRangeMax+2; //Random.Range(yPosition, yPosition);
        

        Instantiate(GameBall, new Vector3(posX, posY, posZ), GameBall.transform.rotation);
    }

    private void GenerateObstacles()
    {
        float xRangeMin = -xRangeMax;

        //Skip lowest row
        for (float row = -zRangeMax - 4; row < zRangeMax; row += 8.75f)
        {
            var friendlyObstacleSpot = Random.Range(0, maxNumberOfObjectsOnRow);
            var hostileObstacleSpot = GenerateRandomNumberWithExlusion(0, maxNumberOfObjectsOnRow, new HashSet<int>() { friendlyObstacleSpot }); ;
            for(int i = 0; i <= maxNumberOfObjectsOnRow; i++)
            {
                if (i == friendlyObstacleSpot)
                {
                    var friendlyObstacle = Instantiate(FriendlyObstacle, CalculatePosition(row, xRangeMin), FriendlyObstacle.transform.rotation).GetComponent<FriendlyObject>();
                    if (GameDataManager.Instance != null)
                    {                        
                        friendlyObstacle.SetColor(GameDataManager.Instance.PlayerColor);
                    }
                }
                else if (i == hostileObstacleSpot)
                {
                    Instantiate(HostileObstacle, CalculatePosition(row, xRangeMin), HostileObstacle.transform.rotation);
                }
                else
                {
                    Instantiate(NeutralObstacle, CalculatePosition(row, xRangeMin), NeutralObstacle.transform.rotation);
                }
                xRangeMin += 7.6f;
            }
            xRangeMin = -xRangeMax;
        }
    }

    private Vector3 CalculatePosition(float zPosition, float xPosition = xRangeMax)
    {
        var posX = Random.Range(xPosition-2, xPosition+2);
        var posY = gamePiecePositionY; 
        var posZ = zPosition; //Random.Range(yPosition, yPosition);
        return new Vector3(posX, posY, posZ);
    }

    private int GenerateRandomNumberWithExlusion(int minRange, int maxRange, HashSet<int> exclusion)
    {
        var range = Enumerable.Range(minRange, maxRange).Where(i => !exclusion.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(minRange, maxRange - exclusion.Count);
        return range.ElementAt(index);
    }
}
