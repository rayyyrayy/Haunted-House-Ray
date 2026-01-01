using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerKnight : MonoBehaviour
{
        public UnityEvent knightTriggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
        {
            knightTriggered.Invoke();
        }  
    }
}
