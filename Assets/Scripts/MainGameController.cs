using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject GameBall;

    private const float gamePiecePositionZ = -1;
    private const float topPositionY = 35.5f;
    private const float maxLeftXPosition = -38f;
    private const float maxRightXPosition = 38.2f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGameBall();
    }       

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGameBall()
    {
        Instantiate(GameBall, CalculatePosition(topPositionY), GameBall.transform.rotation);
    }

    private Vector3 CalculatePosition(float minimumYPosition)
    {
        var posX = Random.Range(maxLeftXPosition, maxRightXPosition);
        var posY = Random.Range(minimumYPosition, topPositionY);
        var posZ = gamePiecePositionZ;
        return new Vector3(posX, posY, posZ);
    }
}
