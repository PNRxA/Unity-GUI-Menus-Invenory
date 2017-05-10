using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public Animator hurt;
    public Inventory inventory;
    public GameObject itemToDrop;

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
            DropItem("300");
        }
    }

    void DropItem(string id)
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 1;
        GameObject itemDrop = Instantiate(itemToDrop, spawnPos, Quaternion.identity);
        itemDrop.name = id;
        Renderer rend = itemDrop.GetComponentInChildren<Renderer>();
        Material mat = rend.material;
        Texture2D tex = ItemDatabase.createItem(int.Parse(id)).Icon;
        mat.SetTexture("_MainTex", tex);
        //inventory.inv.Add(ItemDatabase.createItem(000));
    }
}
