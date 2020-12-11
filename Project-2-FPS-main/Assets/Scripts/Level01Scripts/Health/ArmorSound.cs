using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSound : MonoBehaviour
{
    /*
    private AudioSource ArmorCollect;
    public ParticleSystem pickupEffect;


    void Start()
    {
        ArmorCollect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Instantiate(pickupEffect, transform.position, transform.rotation);
            pickupEffect.Play();
            ArmorCollect.Play();
            StartCoroutine("Destroy");
        }
        
        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        
    }*/

    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        Destroy();
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        pickupEffect.SetActive(false);

    }
}
