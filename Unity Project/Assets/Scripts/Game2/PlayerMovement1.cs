using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour {
    public float moveSpeed = 10;
    public float rotateSpeed = 30;
    public float fasterSpeed = 0.001f;
    public Rigidbody rb;
    private Vector3 moveDirection;
    float horizontalMove = 0;
    public bool canMove = false;
    
    private void Start()
    {
        int minSize = Mathf.Min(Screen.height, Screen.width);
        //rb.centerOfMass = new Vector3(0, 0, 0);
       // transform.position = new Vector3(0, 20, 0);
    }
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        int touchCounter = 0;
        
        if (touchCounter >= Input.touchCount) { horizontalMove = Input.GetAxis("Horizontal"); }
        //loop over every touch found
        while (touchCounter < Input.touchCount)
        {
            if (Input.GetTouch(touchCounter).position.x > Screen.width / 2)
            {
                //move right
                horizontalMove = 1;
            }
            if (Input.GetTouch(touchCounter).position.x < Screen.width / 2)
            {
                //move left
                horizontalMove = -1;
            }
            ++touchCounter;
        }

        if (Time.timeScale != 0 && canMove && FindObjectOfType<GameManager2>().jugando)
        {
            moveSpeed += fasterSpeed;
        }

        moveDirection = new Vector3(0f, 0f, Time.deltaTime * moveSpeed).normalized;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * Convert.ToInt32(canMove));
        transform.Rotate(0f, horizontalMove * rotateSpeed * Time.deltaTime * Convert.ToInt32(canMove), 0f);
    }

    void FixeddUpdate() 
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Planet"){
            canMove = true;
        }
    }
    void OnCollisionStay(Collision col)
    {    
        if(col.gameObject.tag == "Planet"){
            canMove = true;
        }    
    }

    private void OnCollisionExit(Collision col)
    {
        if(canMove == true) 
        {
            canMove = false;
        }
    }

}
