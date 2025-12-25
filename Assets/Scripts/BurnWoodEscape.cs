using UnityEngine;

public class BurnWoodEscape : MonoBehaviour
{
    public ParticleSystem burningAnimation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Torch"))
        {
            burningAnimation.Play();
            Destroy(gameObject);
        }
    }
}
