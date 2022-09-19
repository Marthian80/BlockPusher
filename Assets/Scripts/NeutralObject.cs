using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralObject : MonoBehaviour
{
    private Rigidbody neutralObjectRigidBody;

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
}
