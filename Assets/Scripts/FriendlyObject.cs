using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyObject : MonoBehaviour
{
    private Rigidbody friendlyObjectRigidBody;

    private const float xRangeMax = 38.0f;
    private const float zRangeMax = 35.0f;
    private const string player = "Player";

    private const string neutralObstacle = "NeutralObstacle";
    private const string hostileObstacle = "HostileObstacle";

    // Start is called before the first frame update
    void Start()
    {
        friendlyObjectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {        
        CheckAndAdjustBoundaries();        
    }

    public void SetColor(Color color)
    {
        foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = color;
        }
    }

    private void CheckAndAdjustBoundaries()
    {
        //Check for left and right bounds
        if (transform.position.x < -xRangeMax)
        {
            transform.position = new Vector3(-xRangeMax, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRangeMax)
        {
            transform.position = new Vector3(xRangeMax, transform.position.y, transform.position.z);
        }

        //Check for top and bottom bounds
        if (transform.position.y < -zRangeMax)
        {
            transform.position = new Vector3(transform.position.x, -zRangeMax, transform.position.z);
        }
        if (transform.position.y > zRangeMax)
        {
            transform.position = new Vector3(transform.position.x, zRangeMax, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Neutral and Hostile obstacles can be pushed by a friendly obstacle
        if (collision.gameObject.CompareTag(neutralObstacle) || collision.gameObject.CompareTag(hostileObstacle))
        {
            Rigidbody obstacleRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            obstacleRigidbody.AddForce(awayFromPlayer * 2, ForceMode.Impulse);
        }
    }

    //Stop movement when no longer colliding with player
    private void OnCollisionExit(Collision collision)
    {   
        friendlyObjectRigidBody.velocity = Vector3.zero;        
    }
}