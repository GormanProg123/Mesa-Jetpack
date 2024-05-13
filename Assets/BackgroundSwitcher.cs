using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour
{
    public Sprite background1; 
    public Sprite background2; 

    private SpriteRenderer backgroundRenderer; 

    public float scrollSpeed = 1.0f; 

    private bool isSwitching = false; 
    private float targetY; 

    private void Start()
    {
        
        backgroundRenderer = GetComponent<SpriteRenderer>();

        
        if (background1 != null)
        {
            backgroundRenderer.sprite = background1;
        }
        else
        {
            Debug.LogError("Необходимо присвоить спрайты!");
        }
    }

    private void Update()
    {
        
        if (isSwitching)
        {
            
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * scrollSpeed), transform.position.z);

            
            if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
            {
                
                backgroundRenderer.sprite = backgroundRenderer.sprite == background1 ? background2 : background1;
                targetY = -backgroundRenderer.bounds.size.y;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                isSwitching = false;
            }
        }
    }

    
    public void SwitchBackground()
    {
        if (!isSwitching)
        {
            targetY = -backgroundRenderer.bounds.size.y;
            isSwitching = true;
        }
    }
}


