using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KnightAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2; 
    public float attackRate = .8f; // How many seconds between swings
    private float nextAttackTime = 0f;
    
    public int health = 3;
    
    private NavMeshAgent agent;
    private Animator anim;
    public bool targetTriggered = false;
    private bool isDead = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.isStopped = true;
        agent.SetDestination(transform.position);
    }

    void Update() 
    {
    if (isDead) return;

    if (targetTriggered) {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange) {
            agent.isStopped = false; // Move
            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true); 
        } else {
            agent.isStopped = true; // Stop to attack
            anim.SetBool("isWalking", false);
            if (Time.time >= nextAttackTime) {
                anim.SetTrigger("attack"); 
                nextAttackTime = Time.time + attackRate;
            }
        }
    } else {
        // IMPORTANT: If not triggered, ensure the agent and animations are totally still
        agent.isStopped = true; 
        anim.SetBool("isWalking", false);
    }
}

    public void TakeDamage() {
    if (isDead) return;

    health--;

    if (health <= 0) {
        // CALL DEATH FIRST
        Die(); 
    } else {
        // ONLY CALL HIT IF STILL ALIVE
        anim.SetTrigger("gethit"); 
    }
}
    void Die() 
{
    isDead = true;
    
    // 1. Stop the Agent and disable it so he can't move/slide
    agent.isStopped = true;
    agent.enabled = false; 

    // 2. Stop any physics momentum to prevent spinning/sliding
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero; // Use velocity if on older Unity versions
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true; // Makes him ignore physics forces
    }

    // 3. Play animation
    anim.SetTrigger("death");

    // 4. Turn off collider so player can walk through the body
    GetComponent<Collider>().enabled = false; 
}
public void TriggrTarget()
    {
        targetTriggered=true;
    }
}