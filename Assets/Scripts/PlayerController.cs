using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float speed = 8.0f;
    private Rigidbody playerRigidBody;
    private MainGameController mainGameController;

    private const string friendlyObstacle = "FriendlyObstacle";
    private const string hostileObstacle = "HostileObstacle";

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        mainGameController = GameObject.Find("Board").GetComponent<MainGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainGameController != null && mainGameController.IsGameActive)
        {
            MovePlayer();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (mainGameController != null && mainGameController.IsGameActive)
        {
            //Friendly objects can be pushed
            if (collision.gameObject.CompareTag(friendlyObstacle))
            {
                Rigidbody friendlyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

                friendlyRigidbody.AddForce(awayFromPlayer, ForceMode.Impulse);
            }
            //When hitting hostile object directly, the game ends
            else if (collision.gameObject.CompareTag(hostileObstacle))
            {
                mainGameController.GameOver();
            }
        }
    }
}
