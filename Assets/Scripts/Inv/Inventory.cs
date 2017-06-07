using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inv = new List<Item>();
    public bool showInv;
    public Item selectedItem;
    public GameManager gm;
    public Vector2 invScrollPosition = Vector2.zero;
    public bool selectingItem = false;

    private int _selectedIndex; // For drag and drop stuff later
    private float scrW;
    private float scrH;

    private Rect inventoryWindowRect = new Rect(0, 0, 0, 0);

    void Start()
    {
        // Add initial items to inventory
        inv.Add(ItemDatabase.createItem(000));
        inv.Add(ItemDatabase.createItem(300));
        inv.Add(ItemDatabase.createItem(400));
    }

    public bool ToggleInv()
    {
        // Toggle the inventory
        if (!showInv)
        {
            gm.inMenu = true;
            showInv = true;
            return true;
        }
        else
        {
            gm.inMenu = false;
            showInv = false;
            return false;
        }
    }

    void Update()
    {
        // If not selecting item set selected item to nothing
        if (!selectingItem)
        {
            selectedItem = null;
        }
        // Show manu if you press tab
        if (Input.GetKeyDown(KeyCode.Tab) && !gm.inPauseMenu && !gm.inTradeMenu)
        {
            inventoryWindowRect = new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8);
            ToggleInv();
        }
    }

    void ItemData(string name)
    {
        // Display itemData at mouse position as a tool tip
        GUI.BeginGroup(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, scrW * 7f, scrH * 4));
        GUI.Box(new Rect(0, 0, scrW * 5, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
        GUI.DrawTexture(new Rect(scrW * 1.5f, scrH * 2, scrW * 2f, scrH * 2f), selectedItem.Icon);
        if (GUI.Button(new Rect(scrW * 11f, 6.5f * scrH, scrW * 4, scrH), name))
        {

        }
        GUI.EndGroup();
    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        // Show inventory if showInv
        if (showInv)
        {
            inventoryWindowRect = ClampToScreen(GUI.Window(0, inventoryWindowRect, InventoryWindow, "Inventory"));
            Background();
        }
    }

    void Background()
    {
        // Full background
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
    }

    void InventoryWindow(int windowID)
    {
        // Wrap text to box
        GUI.skin.box.wordWrap = true;
        // Show item tool tips
        ItemTooltips();
        // For loop list
        invScrollPosition = GUI.BeginScrollView(new Rect(0, 0, scrW * 8, scrH * 8), invScrollPosition, new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 7.5f, inv.Count * scrH + 50));
        for (int i = 0; i < inv.Count; i++)
        {
            // Buttons for each inventory item
            Rect r = new Rect(scrW, scrH + i * (scrH), scrW, scrH);
            Rect b = new Rect(scrW, scrH + i * (scrH), scrW * 6, scrH);
            GUI.Box(b, inv[i].Name);
            GUI.DrawTexture(r, inv[i].Icon);
            // If the box contains the cursor then show tooltip
            if (b.Contains(Event.current.mousePosition))
            {
                selectingItem = true;
                selectedItem = inv[i];
            }
            else
            {
                selectingItem = false;
            }
            // Remove item after clicking drop
            if (GUI.Button(new Rect(scrW * 7, scrH + i * (scrH), scrW, scrH), "Drop"))
            {
                inv.Remove(inv[i]);
            }
        }
        GUI.EndScrollView();
        GUI.DragWindow();
    }

    // Generate item tool tips
    void ItemTooltips()
    {
        if (selectedItem != null)
        {
            if (selectedItem.Type == ItemType.Armour)
            {
                ItemData("Equip");
            }
            else if (selectedItem.Type == ItemType.Coins)
            {
                ItemData("Drop");
            }
            else if (selectedItem.Type == ItemType.Consumable)
            {
                ItemData("Eat");
            }
            else if (selectedItem.Type == ItemType.Craftable)
            {
                ItemData("Craft");
            }
            else if (selectedItem.Type == ItemType.Potion)
            {
                ItemData("Drink");
            }
            else if (selectedItem.Type == ItemType.Quest)
            {
                ItemData("Use");
            }
            else if (selectedItem.Type == ItemType.Weapon)
            {
                ItemData("Equip");
            }
        }
    }

    // Clamp window to the screen
    Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return (r);
    }
}
