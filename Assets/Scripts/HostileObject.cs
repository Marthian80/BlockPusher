using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileObject : MonoBehaviour
{
    private Rigidbody hostileObjectRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        hostileObjectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Stop movement when no longer colliding with friendly object
    private void OnCollisionExit(Collision collision)
    {
        hostileObjectRigidBody.velocity = Vector3.zero;
    }
}
