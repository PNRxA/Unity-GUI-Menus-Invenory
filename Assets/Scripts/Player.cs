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
        // Set the player scale based on the customisation height
        gameObject.transform.localScale = new Vector3(playerWidth, playerHeight, playerWidth);
    }

    void Update()
    {
        Attack();
        Movement();
    }

    void LateUpdate()
    {
        // Ensure all values are correct at all times
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
        // Swing sword
        if (Input.GetMouseButtonDown(0) && !gm.inMenu && !gm.inPauseMenu && !gm.inTradeMenu)
        {
            swing.SetTrigger("Attack");
        }
    }

    void Movement()
    {
        // Move character based on axis movement
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
