using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class DoorAnim : MonoBehaviour
{
    public bool isClosed=true;
    public bool isLocked;
    private Animator doorAnim;
    public GameObject key;
    public GameObject _lock;
    public GameObject _startGameLock;
    public AudioSource doorSound;
    public UnityEvent doorOpened;


    void Awake()
    {
        doorAnim = GetComponent<Animator>();
        if (key == null || _lock == null || _startGameLock==null || doorSound==null )
        {
            Debug.Log("No Key or Lock");
        }
    }
    
    public void ToggleDoor()
    {
        
        {
            if (isClosed && !isLocked)
            {
                doorAnim.Play("Door_Open");
                doorSound.Play();
                isClosed = false;
                doorOpened.Invoke();
            }
            else if (!isClosed)
            {
                doorAnim.Play("Door_Close");
                doorSound.Play();
                isClosed = true;
            }
            else
            {
                Debug.Log("Door is locked!");
            }

        }
        }
    
    public void UnlockDoor()
    {
        isLocked = false;
        Destroy(key);
        Destroy(_lock);
        _startGameLock.gameObject.SetActive(false);
    }

    public void OpenDoor()
    {
        if (isClosed)
        {
            doorAnim.Play("Door_Open");
            doorSound.Play();
            doorOpened.Invoke();
        }
        isClosed=false;
    }

    public void LockkDoor()
    {
        if (!isClosed)
        {
            doorAnim.Play("Door_Close");
            doorSound.Play();
            isClosed=true;
        }
        isLocked = true;
        _lock.SetActive(true);
    }

    public void StartGame()
    {
        if (!isClosed)
        {
            doorAnim.Play("Door_Close");
            doorSound.Play();
            isClosed=true;
        }
        isLocked=true;
       _startGameLock.gameObject.SetActive(true);
        
    }
}
