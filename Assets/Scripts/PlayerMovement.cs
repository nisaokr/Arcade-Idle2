using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 5f;

    [SerializeField] public float rotationSpeed = 100f;
    Rigidbody rb;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    void Move(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime ;
        transform.Translate(movement);
    }

    void Rotate(){
        
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
    void Jump(){
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}
