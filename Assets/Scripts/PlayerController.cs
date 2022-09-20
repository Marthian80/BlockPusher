using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private Rigidbody playerRigidBody;
    private MainGameController mainGameController;

    private const string friendlyObstacle = "FriendlyObstacle";
    private const string hostileObstacle = "HostileObstacle";
    private const string neutralObstacle = "NeutralObstacle";
    private const string endGoal = "EndGoal";
    private const float yRangeMin = -0.5f;
    private const float zRange = 39f;
    private const float xRange = 39.5f;
    private const float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        mainGameController = GameObject.Find("Board").GetComponent<MainGameController>();
        if (GameDataManager.Instance != null)
        {
            SetColor(GameDataManager.Instance.PlayerColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDataManager.Instance != null && GameDataManager.Instance.IsGameActive)
        {
            MovePlayer();
            CheckBoundaries();
        }
        else if (GameDataManager.Instance != null && GameDataManager.Instance.IsGameActive == false)
        {
            playerRigidBody.velocity = Vector3.zero;
        }
    }

    // Player movement
    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRigidBody.AddForce(Vector3.forward * speed * verticalInput);
        playerRigidBody.AddForce(Vector3.right * speed * horizontalInput);
    }

    //When player is out of bounds, the game is over
    private void CheckBoundaries()
    {
        //Check for left and right bounds
        if (transform.position.z < -zRange || transform.position.z > zRange)
        {
            mainGameController.GameOver();
        }       
        else if (transform.position.x < -xRange || transform.position.x > xRange)
        {
            mainGameController.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameDataManager.Instance != null && GameDataManager.Instance.IsGameActive)
        {
            //Friendly objects can be pushed
            if (collision.gameObject.CompareTag(friendlyObstacle))
            {
                collision.gameObject.GetComponent<MoveForward>().StopMoving();

                Rigidbody friendlyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

                friendlyRigidbody.AddForce(awayFromPlayer, ForceMode.Impulse);
            }
            //When hitting hostile object directly, the game ends
            else if (collision.gameObject.CompareTag(hostileObstacle))
            {
                mainGameController.GameOver();
            }
            else if (collision.gameObject.CompareTag(endGoal))
            {
                mainGameController.GameWon();
            }
        }
    }

    private void SetColor(Color color)
    {
        foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = color;
        }
    }
}
