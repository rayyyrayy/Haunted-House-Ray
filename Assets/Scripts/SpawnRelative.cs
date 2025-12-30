using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRelative : MonoBehaviour
{
    public List<GameObject> objectToSpawn;
    private int objectIndex;
    public float distance = 4.5f;
    private float spawnDelay=10;
    private bool isSpawning = false;
    private int gemCount=0;
    public AudioSource gemAudio;
    public enum SpawnLocation { Front, Back, Left, Right, Random, atPos }
    public SpawnLocation location;
    private bool hasGrabbedFirstItem = false;
    private bool ghostStorm=false;

    void Start()
    {
      if (gemAudio ==null)
        {
            Debug.Log("no gem audio equipped");
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSpawning)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Spawn(other.transform.position, other.transform.forward, other.transform.right));
            } 
        }
        
        if (other.CompareTag("Gem"))
        {
            Vector3 gemPos = other.transform.position;
            Destroy(other.gameObject);
            if(gemAudio) gemAudio.Play();
            gemCount += 1;
            if (gemCount == 3)
            {
                StartCoroutine(Spawn(gemPos, Vector3.forward, Vector3.right));
                gemCount = 0;
            }
        }
    }

    public void SpawnItemGrabbed()
    {
        // If we haven't done this yet...
        if (!hasGrabbedFirstItem)
        {
            // ...do the spawn...
            StartCoroutine(Spawn(transform.position, Vector3.forward, Vector3.right));
            
            // ...and lock the door forever.
            hasGrabbedFirstItem = true;
        }
    }

// Update the function signature to take Vectors instead of a Transform
    IEnumerator Spawn(Vector3 targetPos, Vector3 targetForward, Vector3 targetRight)
    {
        isSpawning = true;
        objectIndex=Random.Range(0,objectToSpawn.Count);
        Vector3 spawnPos = Vector3.zero;
        SpawnLocation selectedLocation = location;

        if (location == SpawnLocation.Random)
        {
            selectedLocation = (SpawnLocation)Random.Range(0, 4);
        }

        switch (selectedLocation)
        {
            case SpawnLocation.Front:
                spawnPos = targetPos + (targetForward * distance);
                break;
            case SpawnLocation.Back:
                spawnPos = targetPos + (-targetForward * distance);
                break;
            case SpawnLocation.Left:
                spawnPos = targetPos + (-targetRight * distance);
                break;
            case SpawnLocation.Right:
                spawnPos = targetPos + (targetRight * distance);
                break;
            case SpawnLocation.atPos:
                spawnPos = targetPos;
                break;
        }

        spawnPos.y = targetPos.y; 
        Vector3 directionToTarget = targetPos - spawnPos;
        directionToTarget.y = 0; 

        // Safety check: if spawning at the exact same spot, use default rotation
        Quaternion spawnRotation = Quaternion.identity;
        if (directionToTarget != Vector3.zero)
        {
            spawnRotation = Quaternion.LookRotation(directionToTarget);
        }

        Instantiate(objectToSpawn[objectIndex], spawnPos, spawnRotation);
        yield return new WaitForSeconds(spawnDelay);
        isSpawning=false;
    }

    IEnumerator EndGameGhost()
    {
        while (ghostStorm==true)
        {
            
            spawnDelay= Random.Range(0.5f, 3.0f);
            StartCoroutine(Spawn(transform.position, Vector3.forward, Vector3.right));
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

    public void toggleStorm() // Made public so other scripts can see it
    {
        ghostStorm = !ghostStorm; // Shorthand to flip true/false
        
        if (ghostStorm) 
        {
            StartCoroutine(EndGameGhost());
        }
    }


}