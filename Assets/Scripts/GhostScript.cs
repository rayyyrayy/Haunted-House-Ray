using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public List<AudioClip> ghostSounds;
    int randomIndex;
    private float speed=2f;
    Vector3 direction;
    private AudioSource audioSource;
    public enum GhostType {Ballon, Regular}
    public GhostType ghostType; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        randomIndex = UnityEngine.Random.Range(0, ghostSounds.Count);
        GhostMovement();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction* speed * Time.deltaTime);
        if (transform.position.y>10 || transform.position.x <-6 || transform.position.x >40|| transform.position.z >50 || transform.position.z <0    )
        {
            Destroy(gameObject);
        }

    }

    void GhostMovement()
    {
        audioSource.PlayOneShot(ghostSounds[randomIndex]);
        switch (ghostType)
        {
            case GhostType.Regular:
            direction = Vector3.forward;
            break;

            case GhostType.Ballon:
            direction= Vector3.up;
            break; 
        }
    }
    
}
