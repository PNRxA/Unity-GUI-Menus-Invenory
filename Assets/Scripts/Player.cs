using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator swing;
    public bool controllable = true;
    public float speed = 7.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    public GameManager gm;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Attack();
        Movement();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && gm.inMenu == false && gm.inPauseMenu == false)
        {
            swing.SetTrigger("Attack");
        }
    }

    void Movement()
    {
        if (controller.isGrounded && controllable)
        {
            moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
