using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public Animator hurt;
    public Inventory inventory;
    public GameObject itemToDrop;

    // On collision enter with player/weapon
    void OnCollisionEnter(Collision col)
    {
        // If weapon then take damage and drop an item
        if (col.collider.tag == "Weapon")
        {
            hurt.SetTrigger("Hurt");
            DropItem("300");
        }
        // If player then deal damage to the player
        if (col.collider.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().curHealth--;
        }
    }

    // Function to drop item based on ID 
    void DropItem(string id)
    {
        // Set spawnposition
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1;
        // Instantiate itemdrop
        GameObject itemDrop = Instantiate(itemToDrop, spawnPos, Quaternion.identity);
        // Render based on id
        itemDrop.name = id;
        Renderer rend = itemDrop.GetComponentInChildren<Renderer>();
        Material mat = rend.material;
        Texture2D tex = ItemDatabase.createItem(int.Parse(id)).Icon;
        mat.SetTexture("_MainTex", tex);
        //inventory.inv.Add(ItemDatabase.createItem(000));
    }
}
