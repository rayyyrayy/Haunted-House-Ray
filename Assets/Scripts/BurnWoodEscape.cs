using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Events;

public class BurnWoodEscape : MonoBehaviour
{
    public UnityEvent ghostChaos;
    public ParticleSystem burningAnimation;

    void Start()
    {
      if (ghostChaos==null)
        {
            Debug.Log("No ghost invoked");
        }  
    }
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
        ghostChaos.Invoke();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
