using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject rocketPrefab;
    public GameObject additionalPrefab;
    public Vector2 spawnRangeX = new Vector2(10f, 20f);
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);
    public float initialSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;
    public float intervalIncreaseRate = 0.05f;
    public float additionalPrefabMinY = -4.39f;
    public float additionalPrefabMaxY = 3.14f;

    private float currentSpawnInterval;
    private float lastSpawnTime;
    private int spawnCounter = 0;
    private int spawnPrefabCounter = 0; 

    void Start()
    {
        MoveScript.ResetMoveSpeed();
        currentSpawnInterval = initialSpawnInterval;
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > currentSpawnInterval)
        {
            lastSpawnTime = Time.time;
            SpawnObjectsAtSameTime();
            UpdateSpawnInterval();
        }
    }

    void SpawnObjectsAtSameTime()
    {
        
        float initialChanceToSpawnTogether = 0.7f;

        
        bool spawnTogether = Random.value < initialChanceToSpawnTogether;

       
        if (!spawnTogether)
        {
            float spawnTogetherChanceIncreaseRate = 0.05f; 
            float maxChanceToSpawnTogether = 0.9f; 

            
            initialChanceToSpawnTogether = Mathf.Min(initialChanceToSpawnTogether + spawnTogetherChanceIncreaseRate * Time.deltaTime, maxChanceToSpawnTogether);
        }

        
        spawnTogether = Random.value < initialChanceToSpawnTogether;

        
        if (spawnTogether)
        {
            int numToSpawnTogether = Random.Range(1, 4);
            SpawnRandomObject(numToSpawnTogether);
            SpawnRockets(numToSpawnTogether);
        }
        else
        {
            SpawnRandomObject(1);
            SpawnRockets(1);
        }

        SpawnAdditionalPrefab();

        MoveScript.IncreaseMoveSpeed(0.5f);
    }




    void SpawnRandomObject(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPositionForPrefabToSpawn();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(spawnPosition, new Vector2(1, 1), 0);
            bool spawnAllowed = true;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Floor") || collider.CompareTag("Roof"))
                {
                    spawnAllowed = false;
                    break;
                }
                
                if (collider.CompareTag("prefabToSpawn") && Vector2.Distance(collider.transform.position, spawnPosition) < 2.0f)
                {
                    spawnAllowed = false;
                    break;
                }
            }

            if (spawnAllowed)
            {
                
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(0, 0, 90));
                spawnedObject.tag = "prefabToSpawn";

                
                AudioSource audioSource = spawnedObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }

    void SpawnRockets(int numRockets)
    {
       
        for (int i = 0; i < numRockets; i++)
        {
            
            Vector2 rocketSpawnPosition = GetRandomRocketSpawnPosition();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rocketSpawnPosition, new Vector2(1, 1), 0);
            bool spawnAllowed = true;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Floor") || collider.CompareTag("Roof"))
                {
                    spawnAllowed = false;
                    break;
                }

                if (collider.CompareTag("rocketPrefab") && Vector2.Distance(collider.transform.position, rocketSpawnPosition) < 2.0f)
                {
                    spawnAllowed = false;
                    break;
                }
            }

            if (spawnAllowed)
            {
                
                GameObject rocketObject = Instantiate(rocketPrefab, rocketSpawnPosition, Quaternion.identity);
                rocketObject.tag = "rocketPrefab";

                
                AudioSource audioSource = rocketObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }



    void UpdateSpawnInterval()
    {
        currentSpawnInterval = Mathf.Min(currentSpawnInterval + intervalIncreaseRate, maxSpawnInterval);
    }


    Vector2 GetRandomSpawnPositionForPrefabToSpawn()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool placed = false;
        int attempt = 0;
        float distanceBetweenObjects = 2.0f;

        while (!placed && attempt < 100)
        {
            float randX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randY = Random.Range(-3.37f, 3.40033f);

            spawnPosition = new Vector2(randX, randY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, distanceBetweenObjects);
            bool validPosition = true;

            foreach (var collider in colliders)
            {
                if ((collider.CompareTag("prefabToSpawn") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects) ||
                    (collider.CompareTag("rocketPrefab") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects))
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                placed = true;
            }
            attempt++;
        }

        return spawnPosition;
    }

    Vector2 GetRandomRocketSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool placed = false;
        int attempt = 0;
        float distanceBetweenObjects = 2.0f;

        while (!placed && attempt < 100)
        {
            float randX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randY = Random.Range(-4.31f, 4.34f);

            spawnPosition = new Vector2(randX, randY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, distanceBetweenObjects);
            bool validPosition = true;

            foreach (var collider in colliders)
            {
                if ((collider.CompareTag("prefabToSpawn") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects) ||
                    (collider.CompareTag("rocketPrefab") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects))
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                placed = true;
            }
            attempt++;
        }

        return spawnPosition;
    }


    void SpawnAdditionalPrefab()
    {
        Vector2 spawnPosition = GetUniqueSpawnPosition(new Vector2(additionalPrefabMinY, additionalPrefabMaxY));
        Instantiate(additionalPrefab, spawnPosition, Quaternion.identity);
    }

    Vector2 GetUniqueSpawnPosition(Vector2 yRange)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool placed = false;
        int attempt = 0;
        float distanceBetweenObjects = 2.0f;

        while (!placed && attempt < 100)
        {
            float randX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randY = Random.Range(yRange.x, yRange.y);
            spawnPosition = new Vector2(randX, randY);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, distanceBetweenObjects);
            bool validPosition = true;

            foreach (var collider in colliders)
            {
                if ((collider.CompareTag("prefabToSpawn") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects) ||
                    (collider.CompareTag("rocketPrefab") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects) ||
                    (collider.CompareTag("additionalPrefab") && Mathf.Abs(collider.transform.position.y - spawnPosition.y) < distanceBetweenObjects))
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                placed = true;
            }
            attempt++;
        }

        return spawnPosition;
    }
}
