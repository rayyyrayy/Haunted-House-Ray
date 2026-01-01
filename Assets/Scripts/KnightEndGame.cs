using System.Collections;
using UnityEngine;

public class KnightEndGame : MonoBehaviour
{
    public GameObject knightPrefab;
    public bool isSpawning = false;
    public int maxSpawnTime = 5;
    
    [Header("Spawn Settings")]
    public int maxKnights = 10; 
    public int currentKnightCount = 0; 

    void Start()
    {
        StartCoroutine(SpawnKnightsRoutine());
    }

    IEnumerator SpawnKnightsRoutine()
    {
        while (true) 
        {
            if (isSpawning && currentKnightCount < maxKnights)
            {
                float spawnDelay = Random.Range(1, maxSpawnTime);
                yield return new WaitForSeconds(spawnDelay);

                // Check again to make sure player didn't win DURING the delay
                if (isSpawning && currentKnightCount < maxKnights)
                {
                    Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z - 1);
                    GameObject newKnight = Instantiate(knightPrefab, spawnPos, Quaternion.identity);
                    
                    currentKnightCount++; 

                    KnightAI script = newKnight.GetComponent<KnightAI>();
                    if (script != null) 
                    {
                        script.TriggrTarget();
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f); // Checked slightly faster for better responsiveness
            }
        }
    }

    public void KnightDied()
    {
        currentKnightCount--;
        // Clamp to 0 just in case of any weird double-counting
        if (currentKnightCount < 0) currentKnightCount = 0; 
    }

    public void ClearAllKnights()
    {
        isSpawning = false;
        
        var allKnights = Object.FindObjectsByType<KnightAI>(FindObjectsSortMode.None);
        
        foreach (var k in allKnights)
        {
            if (k != null) // Safety check
            {
                k.gameObject.SetActive(false);
                Destroy(k.gameObject);
            }
        }

        currentKnightCount = 0;
    }

    public void StartEndgame() 
    { 
        isSpawning = true; 
    }

 
}