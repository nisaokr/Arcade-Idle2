using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppleCollision : MonoBehaviour
{
   //public GameObject explosionEffect; 
   //public GameObject appleSplat; 
     public GameObject explosionEffect;
     public int TargetBackValue = 100;
     public int TargetFrontValue = 50;
     private AudioSource audioSource;
     private void Start() {
        audioSource = GetComponent<AudioSource>();
        Score.instance.UpdateScoreUI();
     }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("TargetFront")){
            Score.instance.score += TargetFrontValue;
            Score.instance.UpdateScoreUI();
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        else if(collision.gameObject.CompareTag("TargetBack")){
            Score.instance.score += TargetBackValue;
            Score.instance.UpdateScoreUI();
             Instantiate(explosionEffect, transform.position, transform.rotation);
        }
  
        if (collision.gameObject.CompareTag("TargetFront") || collision.gameObject.CompareTag("TargetBack"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }

    }

}
