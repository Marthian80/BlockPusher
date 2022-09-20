using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyObject : MonoBehaviour
{
    private Rigidbody friendlyObjectRigidBody;    

    private const string neutralObstacle = "NeutralObstacle";
    private const string hostileObstacle = "HostileObstacle";

    // Start is called before the first frame update
    void Start()
    {
        friendlyObjectRigidBody = GetComponent<Rigidbody>();
        if (GameDataManager.Instance != null)
        {
            SetColor(GameDataManager.Instance.PlayerColor);
        }
    }    
    
    private void SetColor(Color color)
    {
        foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = color;
        }
    }       

    private void OnCollisionEnter(Collision collision)
    {
        //Neutral and Hostile obstacles can be pushed by a friendly obstacle
        if (collision.gameObject.CompareTag(neutralObstacle) || collision.gameObject.CompareTag(hostileObstacle))
        {
            collision.gameObject.GetComponent<MoveForward>().StopMoving();
            Rigidbody obstacleRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            obstacleRigidbody.AddForce(awayFromPlayer * 2, ForceMode.Impulse);
        }
    }

    //Start scrolling when no longer colliding with player
    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<MoveForward>().StartMoving();
    }
}