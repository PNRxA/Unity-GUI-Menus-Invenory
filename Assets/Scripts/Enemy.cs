using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public Animator hurt;
    public Inventory inventory;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Weapon")
        {
            hurt.SetTrigger("Hurt");
            DropItem();
        }
    }

    void DropItem()
    {
        inventory.inv.Add(ItemDatabase.createItem(000));
    }
}
