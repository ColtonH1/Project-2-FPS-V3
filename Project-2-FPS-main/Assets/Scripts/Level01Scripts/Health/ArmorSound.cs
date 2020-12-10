using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSound : MonoBehaviour
{

    private AudioSource ArmorCollect;


    void Start()
    {
        ArmorCollect = GetComponent<AudioSource>();
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        ArmorCollect.Play();
    }*/

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            ArmorCollect.Play();
            StartCoroutine("Destroy");
        }
        
        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
        
    }
}
