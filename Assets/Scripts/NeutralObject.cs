using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralObject : MonoBehaviour
{
    private Rigidbody neutralObjectRigidBody;

    private const string player = "Player";
    private const string friendlyObstacle = "FriendlyObstacle";

    // Start is called before the first frame update
    void Start()
    {
        neutralObjectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Stop movement when no longer colliding with friendly object
    private void OnCollisionExit(Collision collision)
    {
        neutralObjectRigidBody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(player))
        {
            neutralObjectRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (collision.gameObject.CompareTag(friendlyObstacle))
        {
            neutralObjectRigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}
