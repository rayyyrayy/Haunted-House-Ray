using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public AudioSource attackHit;
    private bool canHit = true; // This is the "gate"
    public float cooldownTime = 0.8f; // How fast you can swing

    private void OnTriggerEnter(Collider other) 
    {
        // Check if the gate is open AND we hit a Knight
        if (canHit && other.CompareTag("Knight")) 
        {
            // Close the gate immediately
            canHit = false;

            // Deal damage
            KnightAI knight = other.GetComponent<KnightAI>();
            if (knight != null)
            {
                knight.TakeDamage();
            }

            // Play sound
            if (attackHit != null) attackHit.Play();

            // Start the timer to open the gate again
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(cooldownTime);
        canHit = true; // Open the gate
    }
}