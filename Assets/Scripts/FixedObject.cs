using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedObject : MonoBehaviour
{
    private Rigidbody fixedObjectRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        fixedObjectRigidBody = GetComponent<Rigidbody>();
        fixedObjectRigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
