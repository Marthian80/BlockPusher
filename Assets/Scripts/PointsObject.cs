using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsObject : MonoBehaviour
{
    public int PointsValue;

    private MainGameController mainGameController;

    private const string player = "Player";

    // Start is called before the first frame update
    void Start()
    {
        mainGameController = GameObject.Find("Board").GetComponent<MainGameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(player))
        {
            Destroy(gameObject);
            if (GameDataManager.Instance != null && mainGameController != null)
            {
                GameDataManager.Instance.AddPoints(PointsValue);
                mainGameController.UpdateScore();
            }            
        }
    }
}
