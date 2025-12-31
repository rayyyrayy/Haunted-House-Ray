using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent startGame;
    public UnityEvent endGame;
    public UnityEvent returnBackground;
    public int playerHealth = 3;
    public Canvas livesCanvas;
    public Canvas deathCanvas;
    public TextMeshProUGUI livesText;

    private bool isInvincible = false; // Prevents multi-hits in one swing

    void Awake()
    {
        // Careful with hard-coding (0,0,0) in VR as it might put the player inside the floor
        Time.timeScale = 1f;
        livesText.SetText("Lives: " + playerHealth);
        deathCanvas.gameObject.SetActive(false);
        livesCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start Game"))
        {
            startGame.Invoke();
            Destroy(other.gameObject); // Fixed: Destroy the object, not just the collider
        }  
         if (other.CompareTag("End Game"))
        {
            endGame.Invoke();
        }
        if (other.CompareTag("Return Background"))
        {
            returnBackground.Invoke();
        }
    }

    public void TakeDamage()
    {
        playerHealth--;
        livesText.SetText("Lives: " + playerHealth);
        
        if (playerHealth <= 0)
        {
            deathCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f; // Freezes AI and animations
        }
        else
        {
            StartCoroutine(FlashHealth());
        }
    }

    IEnumerator FlashHealth()
    {
        isInvincible = true; 
        livesCanvas.gameObject.SetActive(true);
        
        // Use Realtime so UI works even if Time.timeScale is 0
        yield return new WaitForSecondsRealtime(1f); 
        
        livesCanvas.gameObject.SetActive(false);
        isInvincible = false;
    } 
}