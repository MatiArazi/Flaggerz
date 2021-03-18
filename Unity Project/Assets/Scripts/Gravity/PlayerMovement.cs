using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public GameObject camera;
    public float moveSpeed = 10;
    public float rotateSpeed = 30;
    public Rigidbody rb;
    private Vector3 moveDirection;
    float horizontalMove = 0;
    float time = 0f;
    float fasterSpeed = 0.00001f;
    private void Start()
    {
        int minSize = Mathf.Min(Screen.height, Screen.width);
        camera.transform.position = new Vector3(0, 640 / 40 * minSize, 0);
        //rb.centerOfMass = new Vector3(0, 0, 0);
       // transform.position = new Vector3(0, 20, 0);
    }
    void Update()
    {
        int i = 0;
        time += Time.deltaTime;
        
        if (i >= Input.touchCount) { horizontalMove = Input.GetAxis("Horizontal"); }
        //loop over every touch found
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > Screen.width / 2)
            {
                //move right
                horizontalMove = 1;
            }
            if (Input.GetTouch(i).position.x < Screen.width / 2)
            {
                //move left
                horizontalMove = -1;
            }
            ++i;
        }

        if (time > 5 && Time.timeScale != 0)
        {
            moveSpeed += moveSpeed * fasterSpeed;
        }

        moveDirection = new Vector3(0f, 0f, Time.deltaTime * moveSpeed).normalized;
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, horizontalMove * rotateSpeed * Time.deltaTime, 0f);
    }

}
