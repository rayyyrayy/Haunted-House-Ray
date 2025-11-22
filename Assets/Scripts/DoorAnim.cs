using NUnit.Framework;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    public bool isClosed=true;
    public bool isLocked;
    private Animator doorAnim;
    public GameObject key;
    public GameObject _lock;

    void Awake()
    {
        doorAnim = GetComponent<Animator>();
        if (key == null || _lock == null)
        {
            Debug.Log("No Key or Lock");
        }

        if (_lock!= null)
        {
            isLocked=true;
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
    }
}
