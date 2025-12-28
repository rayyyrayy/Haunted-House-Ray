using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorScript : MonoBehaviour
{
    public List<AudioClip> dropSounds;
    public AudioSource dropAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int randomIndex=Random.Range(0, dropSounds.Count-1);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player not tracked");
        }else
        {
            dropAudio.PlayOneShot(dropSounds[randomIndex]);
        }
    }

}

