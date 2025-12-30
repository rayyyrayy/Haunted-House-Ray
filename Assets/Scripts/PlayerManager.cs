using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent startGame;
    public UnityEvent endGame;
    public UnityEvent returnBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     transform.position= new Vector3(0,0,0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start Game"))
        {
            startGame.Invoke();
            Destroy(other);
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
}
