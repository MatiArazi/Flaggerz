﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour {
    // public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float moveSpeed = 16;
    public float rotateSpeed = 30;
	public float jumpForce = 220;
	public LayerMask groundedMask;
    public bool canMove = false;
	
	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	Rigidbody rigidbody;
	
	
	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();
	}

	void Update() {
		// Calculate movement:
		float inputX = 0;
        int touchCounter = 0;
        if (touchCounter >= Input.touchCount) { inputX = Input.GetAxis("Horizontal"); }
        //loop over every touch found
        while (touchCounter < Input.touchCount)
        {
            if (Input.GetTouch(touchCounter).position.x > Screen.width / 2)
            {
                //move right
                inputX = 1;
            }
            if (Input.GetTouch(touchCounter).position.x < Screen.width / 2)
            {
                //move left
                inputX = -1;
            }
            ++touchCounter;
        }
		
        // Look rotation:
		transform.Rotate(Vector3.up * inputX * rotateSpeed * Convert.ToInt32(canMove) * Time.deltaTime);

        // Moving
		Vector3 moveDir = new Vector3(0,0, 1).normalized;
		Vector3 targetMoveAmount = moveDir * moveSpeed * Convert.ToInt32(canMove);
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,1f);
		
		// Jump
		if (Input.GetButtonDown("Jump")) {
			if (grounded) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}
		
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
			grounded = true;
            //canMove = true;
		}
		else {
			grounded = false;
            //canMove = false;
		}
		
	}
	
	void FixedUpdate() {
        if(!canMove){
            
        }
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
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
        if(col.gameObject.tag == "Planet"){
            canMove = false;
        }  
    }
}