using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public AudioSource attackHit;
    public GameObject regularSlash;
    public GameObject finalSlash;

    public bool canHit = true; // This is the "gate"
    public float cooldownTime = 0.8f; // How fast you can swing
    private KnightAI knight;

    private void OnTriggerEnter(Collider other) 
    {
        // Check if the gate is open AND we hit a Knight
        if (canHit && other.CompareTag("Knight")) 
        {
            KnightAI knight = other.GetComponent<KnightAI>();
            // Close the gate immediately
            if (knight.health <= 0) return;
            canHit = false;
            Vector3 hitPoint = other.ClosestPoint(transform.position);
                    

            // Deal damage
            if (knight != null)
            {
                knight.TakeDamage();
                if (knight.health <= 0)
                {
                    Instantiate(finalSlash, hitPoint, Quaternion.identity);

                } else
                {
                    Instantiate(regularSlash, hitPoint, Quaternion.identity);
                    StartCoroutine(ResetAttack());

                }
            }

            // Play sound
            if (attackHit != null) attackHit.Play();

            // Start the timer to open the gate again
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(cooldownTime);
        
        canHit = true; // Open the gate

    }
}