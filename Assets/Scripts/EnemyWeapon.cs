using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    private Collider weaponCollider;
    public AudioSource weaponHitSound;

    void Start() {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false; // Start off
    }

    public IEnumerator EnableWeapon(float duration) 
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerManager>().TakeDamage();
            weaponHitSound.Play();
            //weaponCollider.enabled = false; // Disable so it doesn't hit twice in one swing
        }
    }
}