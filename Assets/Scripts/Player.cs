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
    public int agility, intelligence, strength;
    public float curHealth, maxHealth, curStamina, maxStamina, curMana, maxMana;
    public float playerHeight, playerWidth;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        gameObject.transform.localScale = new Vector3(playerWidth, playerHeight, playerWidth);
    }

    void Update()
    {
        Attack();
        Movement();
    }

    void LateUpdate()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < 0)
        {
            curHealth = 0;
        }

        if (curStamina > maxStamina)
        {
            curStamina = maxStamina;
        }
        if (curStamina < 0)
        {
            curStamina = 0;
        }

        if (curMana > maxMana)
        {
            curMana = maxMana;
        }
        if (curMana < 0)
        {
            curMana = 0;
        }
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
