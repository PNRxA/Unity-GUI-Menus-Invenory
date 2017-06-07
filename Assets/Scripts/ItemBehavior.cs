using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameManager gm;

    private Inventory inventory;

    // Use this for initialization
    void Start()
    {
        // Set gm and inventory
        gm = FindObjectOfType<GameManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If not in a menu then rotate the item
        if (!gm.inMenu)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    // On trigger enter let the player collect the item into their inventory
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inventory.inv.Add(ItemDatabase.createItem(int.Parse(gameObject.name)));
            Destroy(gameObject);
        }
    }
}
