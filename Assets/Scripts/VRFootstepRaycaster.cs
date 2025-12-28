using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootstepRaycaster : MonoBehaviour
{
    public List<AudioClip> stepSounds;
    public AudioSource audioSource;
    public float stepDistance = 1.5f; // Play a sound every 1.5 meters moved
    public LayerMask floorLayer;      // Set this to the layer your floor is on

    private Vector3 lastStepPosition;
    private float distanceTravelled;

    void Start()
    {
        lastStepPosition = transform.position;
    }

    void Update()
    {
        // 1. Check if we are touching the floor using a Raycast
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2.5f, floorLayer))
        {
            // 2. Calculate how far we've moved since the last frame
            float distanceThisFrame = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), 
                                                       new Vector3(lastStepPosition.x, 0, lastStepPosition.z));
            
            distanceTravelled += distanceThisFrame;
            lastStepPosition = transform.position;

            // 3. If we've walked far enough, play a sound
            if (distanceTravelled >= stepDistance)
            {
                PlayRandomStep();
                distanceTravelled = 0; // Reset the counter
            }
        }
    }

    void PlayRandomStep()
    {
        if (stepSounds.Count > 0)
        {
            int index = Random.Range(0, stepSounds.Count);
            audioSource.PlayOneShot(stepSounds[index]);
        }
    }
}