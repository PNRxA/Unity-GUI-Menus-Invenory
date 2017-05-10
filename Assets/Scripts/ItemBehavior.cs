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
        gm = FindObjectOfType<GameManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gm.inMenu)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inventory.inv.Add(ItemDatabase.createItem(int.Parse(gameObject.name)));
            Destroy(gameObject);
        }
    }
}
