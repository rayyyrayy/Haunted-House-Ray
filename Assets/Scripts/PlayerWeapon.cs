using System.Collections;
using System.Collections.Generic; // Added for the list
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public AudioSource attackHit;
    public GameObject regularSlash;
    public GameObject finalSlash;

    public bool isAttacking = false; // Set this to true when the player swings
    public float cooldownTime = 0.7f; 
    
    // This list keeps track of who we already hit in THIS swing
    private List<KnightAI> knightsHitInThisSwing = new List<KnightAI>();

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Knight")) 
        {
            KnightAI hitKnight = other.GetComponent<KnightAI>();

            if (hitKnight != null && !knightsHitInThisSwing.Contains(hitKnight))
            {
                // If knight is already dead, ignore them
                if (hitKnight.health <= 0) return;

                // Add to list so we don't hit the SAME knight twice in one frame
                knightsHitInThisSwing.Add(hitKnight);

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                hitKnight.TakeDamage();

                if (hitKnight.health <= 0)
                {
                    Instantiate(finalSlash, hitPoint, Quaternion.identity);
                } 
                else
                {
                    Instantiate(regularSlash, hitPoint, Quaternion.identity);
                }

                if (attackHit != null) attackHit.Play();
                
                // Start a timer to clear the "already hit" list
                StartCoroutine(ResetHitList());
            }
        }
    }

    IEnumerator ResetHitList()
    {
        yield return new WaitForSeconds(cooldownTime);
        knightsHitInThisSwing.Clear(); 
    }
}