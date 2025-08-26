using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
   public Transform collectPoint;
   public Transform DroppedApples;
   public Vector3 collectPointSpread = new Vector3(0.5f, 0.5f, 0.5f);
   private List<GameObject> collectedApples = new List<GameObject>();

  /* void Start()
    {
        // Başlangıçta toplama noktasının atanmış olduğundan emin olun
        if (collectPoint == null)
        {
            Debug.LogError("Collect Point atanmadı! Lütfen Inspector'dan atayın.");
        }
    } */

    void OnTriggerEnter(Collider other) {

     // Debug.Log("Trigger girişi algilandi: " + other.gameObject.name);

     if(other.CompareTag("Tree")){
        collectApplesFromTree(other.transform);
       // Debug.Log("Ağaç ile etkileşim algilandi bu: " + other.gameObject.name);
     }
     else if(other.CompareTag("DropZone")){
        DropApples();
        //Debug.Log("Bırakma noktası ile etkileşim algılandı: " + other.gameObject.name);
     }

   }

    private void collectApplesFromTree(Transform treeTransform)
    {
      // Debug.Log("CollectApplesFromTree çağrıldı.");

        foreach(Transform child in treeTransform){
        // Debug.Log("Tree child bulundu: " + child.gameObject.name); 
         if(child.CompareTag("Apple"))
            {
                CollectRbApples(child);
                child.SetParent(collectPoint);
                Vector3 randomOffset = new Vector3(
                   UnityEngine.Random.Range(-collectPointSpread.x, collectPointSpread.x),
                   UnityEngine.Random.Range(-collectPointSpread.y, collectPointSpread.y),
                   UnityEngine.Random.Range(-collectPointSpread.z, collectPointSpread.z)
                );
                child.localPosition = randomOffset;
                //Debug.Log("Elma set parent ve local position ayarlandı: " + child.gameObject.name);
                collectedApples.Add(child.gameObject);
                // Debug.Log("Elma toplandı ve CollectPoint'e taşındı: " + child.gameObject.name);
            }

        }
    }

    private void DropApples()
    {
       // Debug.Log("DropApples çağrıldı.");
        foreach (GameObject apple in collectedApples)
        {

            apple.transform.SetParent(DroppedApples);
            apple.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y, transform.position.z + UnityEngine.Random.Range(-1f, 1f));
            // Debug.Log("Elma bırakıldı: " + apple.gameObject.name);
            DropRbApples(apple);

        }

        collectedApples.Clear();
    }

    static void CollectRbApples(Transform child)
    {
        Rigidbody appleRb = child.GetComponent<Rigidbody>();
        if (appleRb != null)
        {
            Debug.Log("Rigidbody bulundu: " + child.gameObject.name);
            appleRb.isKinematic = true;
            appleRb.useGravity = false;
        }
        else
        {
            Debug.LogWarning("Rigidbody bileşeni bulunamadı: " + child.gameObject.name);
        }
    }

    static void DropRbApples(GameObject apple)
    {
        Rigidbody appleRb = apple.GetComponent<Rigidbody>();
        if (appleRb != null)
        {

            appleRb.useGravity = true;
            appleRb.isKinematic = false;
            Debug.Log("Elma gravity aktif edildi: " + apple.gameObject.name);
        }
        else
        {
            Debug.LogWarning("Rigidbody bileşeni bulunamadı: " + apple.gameObject.name);
        }
        Debug.Log("Elma bırakıldı: " + apple.gameObject.name);
    }
    
}
