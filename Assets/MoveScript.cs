using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private static float currentMoveSpeed = 7;
    private float moveSpeed = 7f; 
    public float deadZone = -11f;

    void Update()
    {
        
        if (!gameObject.CompareTag("Coin"))
        {
            
            transform.position = transform.position + (Vector3.left * currentMoveSpeed) * Time.deltaTime;

            
            if (transform.position.x < deadZone)
            {
                Debug.Log("Portal Deleted");
                Destroy(gameObject);
            }
        }
        else
        {
            
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            
            if (transform.position.x < deadZone)
            {
                Debug.Log("Portal Deleted");
                Destroy(gameObject);
            }
        }
    }

    
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    
    public static void IncreaseMoveSpeed(float amount)
    {
        currentMoveSpeed += amount;
    }

    
    public static void ResetMoveSpeed()
    {
        currentMoveSpeed = 7;
    }
}
