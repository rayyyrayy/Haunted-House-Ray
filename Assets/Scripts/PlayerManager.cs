using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent startGame;

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
    }
}
