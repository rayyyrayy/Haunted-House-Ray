using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent startGame;
    public UnityEvent endGame;
    public UnityEvent returnBackground;
    private int playerHealth=6;
    private int  startingHealth=6;
    private int deaths;
    private int totalKnightsDefeated = 0;
    private int totalGhostsEncountered = 0;

    public Canvas livesCanvas;
    public Canvas deathCanvas;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI knightDefeatedText;
    public TextMeshProUGUI ghostEcounteredText;
    public TextMeshProUGUI deathsText;
    [SerializeField] private OnButtonPress triggerbuttonPress;
    public GameObject damageFlash;


    private bool isInvincible = false; // Prevents multi-hits in one swing

    void Awake()
    {
        // Careful with hard-coding (0,0,0) in VR as it might put the player inside the floor
        Time.timeScale = 1f;
        livesText.SetText("Lives: " + playerHealth);
        deathCanvas.gameObject.SetActive(false);
        livesCanvas.gameObject.SetActive(false);
        triggerbuttonPress.enabled=false;
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
        deaths=startingHealth-playerHealth;
        deathsText.SetText("Deaths: "+deaths);
        livesText.SetText("Lives: " + playerHealth);
        
        if (playerHealth <= 0)
        {
            StartCoroutine(DamageFlash());
            deathCanvas.gameObject.SetActive(true);
            triggerbuttonPress.enabled=true;
            Time.timeScale = 0f; // Freezes AI and animations
        }
        else
        {
            StartCoroutine(DamageFlash());
            StartCoroutine(FlashHealth());
        }
    }

    IEnumerator FlashHealth()
    {
        isInvincible = true; 
        livesCanvas.gameObject.SetActive(true);
        
        // Use Realtime so UI works even if Time.timeScale is 0
        yield return new WaitForSecondsRealtime(.8f); 
        
        livesCanvas.gameObject.SetActive(false);
        isInvincible = false;
    } 


    public void easyDiffuculty()
    {
        playerHealth=10;
        startingHealth=playerHealth;
        livesText.SetText("Lives: " + playerHealth);

    }

    public void mediumDiffuculty()
    {
        playerHealth=6;
        startingHealth=playerHealth;
        livesText.SetText("Lives: " + playerHealth);
    }

    public void hardDiffuculty()
    {
        playerHealth=3;
        startingHealth=playerHealth;
        livesText.SetText("Lives: " + playerHealth);
    }

    public void extremeDiffuculty()
    {
        playerHealth=1;
        startingHealth=playerHealth;
        livesText.SetText("Lives: " + playerHealth);
    }

    public void noDeathDiffuculty()
    {
        playerHealth=100;
        startingHealth=playerHealth;
        livesText.SetText("Lives: " + playerHealth);
    }
    public void KnightDied()
    {
        totalKnightsDefeated++; // Add this! Every time any knight dies, this goes up.
        knightDefeatedText.SetText("Knights Defeated : " + totalKnightsDefeated);
    }

    public void GhostEcountered()
    {
        totalGhostsEncountered++;
        ghostEcounteredText.SetText("Ghost Encountered: "+ totalGhostsEncountered);

    }
    public void triggerEnabled()
    {
        triggerbuttonPress.enabled=true;

    }

    public void triggerDisabled()
    {
        triggerbuttonPress.enabled=false;
    }

    IEnumerator DamageFlash()
    {
        damageFlash.SetActive(true);
        yield return new WaitForSeconds(.2f);
        damageFlash.SetActive(false);
    }
}
