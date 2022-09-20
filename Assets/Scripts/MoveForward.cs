using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed { get; private set; }

    private Vector3 direction;
    private bool stopMoving;

    private const float zRangeMax = 36.0f;  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMoving)
        {
            transform.Translate(direction * Time.deltaTime * Speed);
        }

        //Remove objects when behind the bottom
        if (gameObject.transform.position.z < -zRangeMax - 2)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void StopMoving()
    {
        stopMoving = true;        
    }

    public void StartMoving()
    {
        stopMoving = false;
    }
}

