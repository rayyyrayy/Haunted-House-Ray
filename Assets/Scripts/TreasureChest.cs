using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject treasure;
    [Header("Rotation Settings")]
    [SerializeField] private Transform lidTransform;
    private float openAngle = 120;
    private float smoothSpeed = 3f;

    private bool isOpen = false;

    void Update()
    {
        float targetAngle = isOpen ? openAngle : 0f;
        Quaternion targetRotation = Quaternion.Euler(targetAngle, 0, 0);

        // We rotate the lidTransform, NOT the object the script is on
        lidTransform.localRotation = Quaternion.Slerp(lidTransform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    public void ToggleChest()
    {
        isOpen = !isOpen;
        treasure.SetActive(true);
        
    }
}
