using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class BurnWoodEscape : MonoBehaviour
{
    public ParticleSystem burningAnimation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Torch"))
        {
            StartCoroutine(burnWood());
        }
    }


    IEnumerator burnWood()
    {
        burningAnimation.gameObject.SetActive(true);
        burningAnimation.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
