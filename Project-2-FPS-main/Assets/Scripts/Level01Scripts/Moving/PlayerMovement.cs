using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //Transform target;
    //NavMeshAgent agent;

    public float speed = 12f; //speed
    public float gravity = -9.81f; //gravity
    public float jumpHeight = 3f; //jumping

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity; //gravity
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = speed;
        Jumping();
       
        currentSpeed = Running(currentSpeed);

        Moving(currentSpeed);

        /*if(target != null)
        {
            agent.SetDestination(target.position);
        }*/

    }

    private void Moving(float currentSpeed)
    {
        //moving on x and z
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    private float Running(float currentSpeed)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2;
        }
        else
        {
            currentSpeed = speed;
        }

        return currentSpeed;
    }

    private void Jumping()
    {
        //check if grounded to reset velocity when falling (or lack thereof)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }

    /*public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        target = null; 
    }*/
}
