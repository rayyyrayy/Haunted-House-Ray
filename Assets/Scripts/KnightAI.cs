using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KnightAI : MonoBehaviour
{
    private Transform player; // Changed to private since we find it automatically
    public float attackRange = 2; 
    private float attackRate = .8f; 
    private float nextAttackTime = 0f;
    
    public int health = 3;
    
    private NavMeshAgent agent;
    private Animator anim;
    public bool targetTriggered = false;
    public bool isAttacking = false;
    private bool isDead = false;
    public int knightsDefeated;
    private PlayerManager playerManager;
    private KnightEndGame chaseScript;
    private KnightEndGame escapeScript;
    void Start() {
        playerManager=GameObject.Find("XR Rig").GetComponent<PlayerManager>();
        chaseScript=GameObject.Find("Spawn Chase Knights").GetComponent<KnightEndGame>();
        escapeScript=GameObject.Find("Knight Escape Trigger").GetComponent<KnightEndGame>();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // AUTOMATICALLY FIND THE PLAYER
        GameObject playerObj = GameObject.Find("XR Rig");
        if (playerObj != null) {
            player = playerObj.transform;
        } else {
            Debug.LogError("KnightAI: Could not find 'XR Rig' in the scene!");
        }

        agent.isStopped = true;
        // Setting destination to current position to prevent early sliding
        agent.SetDestination(transform.position);
    }

    void Update() 
    {
        if (isDead || player == null) return;

        if (targetTriggered) 
        {

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > attackRange) {
                agent.isStopped = false; 
                agent.SetDestination(player.position);
                anim.SetBool("isWalking", true); 
            } else {
                // STOP SLIDING: Set velocity to zero immediately
                agent.isStopped = true; 
                agent.velocity = Vector3.zero; 
                
                anim.SetBool("isWalking", false);

                // Rotate to face player while attacking
                Vector3 lookDir = player.position - transform.position;
                lookDir.y = 0;
                if (lookDir != Vector3.zero) {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
                }

                if (Time.time >= nextAttackTime) {
                    anim.SetTrigger("attack"); 
                    nextAttackTime = Time.time + attackRate;
                    StartCoroutine(AttackWindow());
                }
            }
        }
    }


    public void TakeDamage() {
        if (isDead) return;
        health--;
        if (health <= 0) {
            Die(); 
        } else {
            anim.SetTrigger("gethit"); 
        }
    }

    void Die() {
    if (isDead) return; // Safety check to prevent the function from running twice
    isDead = true;

    // 1. CLEAR PENDING TRIGGERS
    // This stops the knight from trying to 'finish' an attack or hit flinch
    anim.ResetTrigger("attack");
    anim.ResetTrigger("gethit");

    // 2. TRIGGER DEATH
    anim.SetTrigger("death");

    // 3. LOGIC UPDATES
    if (playerManager != null) {
        playerManager.KnightDied();
    }

    if (chaseScript!=null)
        {
            chaseScript.KnightDied();
        }
    if (escapeScript!=null)
        {
            escapeScript.KnightDied();
        }

    // 4. PHYSICS & NAVIGATION
    agent.isStopped = true;
    agent.enabled = false; 

    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null) {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true; 
    }

    // 5. CLEANUP
    GetComponent<Collider>().enabled = false; 
    }

    IEnumerator AttackWindow() {
        isAttacking = true;
        yield return new WaitForSeconds(attackRate * 0.9f); // Sync with attack rate
        isAttacking = false;
    }

    public void TriggrTarget() {
        targetTriggered = true;
    }

}