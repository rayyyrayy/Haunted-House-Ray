using NUnit.Framework;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    public bool isClosed=true;
    public bool isLocked;
    private Animator doorAnim;
    public GameObject key;
    public GameObject _lock;
    public GameObject _startGameLock;

    void Awake()
    {
        doorAnim = GetComponent<Animator>();
        if (key == null || _lock == null || _startGameLock==null)
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
                isClosed = false;
            }
            else if (!isClosed)
            {
                doorAnim.Play("Door_Close");
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
        }
        isClosed=false;
    }

    public void LockkDoor()
    {
        if (!isClosed)
        {
            doorAnim.Play("Door_Close");
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
            isClosed=true;
        }
        isLocked=true;
       _startGameLock.gameObject.SetActive(true);
        
    }
}
