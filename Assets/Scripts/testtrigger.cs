using NUnit.Framework;
using UnityEngine;

public class testtrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private PlayerManager playerManager;
    private KnightAI knightAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager=GameObject.Find("XR Rig").GetComponent<PlayerManager>();
        knightAI = GetComponentInParent<KnightAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& knightAI.isAttacking)
        {
            audioSource.Play();
            // playerManager.TakeDamage();
        }
    }
}
