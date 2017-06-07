using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> chest = new List<Item>();
    public GameManager gm;
    private float scrH, scrW;
    private Inventory inventory;
    public Vector2 invScrollPosition = Vector2.zero;
    public Vector2 transferScrollPosition = Vector2.zero;
    public bool showTransfer = false;
    // Use this for initialization
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        chest.Add(ItemDatabase.createItem(000));
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfClicked();
    }

    public bool ToggleTransfer()
    {
        if (!showTransfer)
        {
            gm.inTradeMenu = true;
            showTransfer = true;
            return true;
        }
        else
        {
            gm.inTradeMenu = false;
            showTransfer = false;
            return false;
        }
    }

    void CheckIfClicked()
    {
        // If pressing E on the chest, then open the chest
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Chest")
                {
                    // Open Chest
                    if (!gm.inMenu && !gm.inPauseMenu)
                    {
                        ToggleTransfer();
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        scrH = Screen.height / 10;
        scrW = Screen.width / 16;
        if (showTransfer)
        {
            TransferMenu();
        }
    }

    void TransferMenu()
    {
        // Background
        GUI.Box(new Rect(0, 0, scrW * 16, scrH * 10), "");
        // INVENTORY
        // Inv background
        GUI.Box(new Rect(scrW, scrH, scrW * 8, scrH * 8), "Inventory");
        // Open chest
        GUI.skin.box.wordWrap = true;
        invScrollPosition = GUI.BeginScrollView(new Rect(scrH, scrW, scrW * 8, scrH * 8), invScrollPosition, new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 7.5f, inventory.inv.Count * scrH + 50));
        for (int i = 0; i < inventory.inv.Count; i++)
        {
            // Buttons for each inventory item
            Rect r = new Rect(scrW, scrH + i * (scrH), scrW, scrH);
            Rect b = new Rect(scrW, scrH + i * (scrH), scrW * 6, scrH);
            GUI.Box(b, inventory.inv[i].Name);
            GUI.DrawTexture(r, inventory.inv[i].Icon);
            if (GUI.Button(new Rect(scrW * 7, scrH + i * (scrH), scrW, scrH), "Transfer"))
            {
                chest.Add(inventory.inv[i]);
                inventory.inv.Remove(inventory.inv[i]);
            }
        }
        GUI.EndScrollView();
        // CHEST
        // Inv background
        GUI.Box(new Rect(scrW * 10, scrH, scrW * 5, scrH * 8), "Chest");
        // Open chest
        GUI.skin.box.wordWrap = true;
        transferScrollPosition = GUI.BeginScrollView(new Rect(scrW * 10, scrH, scrW * 5, scrH * 8), transferScrollPosition, new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 4.5f, chest.Count * scrH + 50));

        for (int i = 0; i < chest.Count; i++)
        {
            // Buttons for each inventory item
            Rect r = new Rect(scrW, scrH + i * (scrH), scrW, scrH);
            Rect b = new Rect(scrW, scrH + i * (scrH), scrW * 4, scrH);
            GUI.Box(b, chest[i].Name);
            GUI.DrawTexture(r, chest[i].Icon);
            if (GUI.Button(new Rect(scrW * 4, scrH + i * (scrH), scrW, scrH), "Transfer"))
            {
                inventory.inv.Add(chest[i]);
                chest.Remove(chest[i]);
            }
        }
        GUI.EndScrollView();
    }
}
