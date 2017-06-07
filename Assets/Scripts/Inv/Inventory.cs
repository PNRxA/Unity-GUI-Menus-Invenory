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
        inv.Add(ItemDatabase.createItem(000));
        inv.Add(ItemDatabase.createItem(300));
        inv.Add(ItemDatabase.createItem(400));
    }

    public bool ToggleInv()
    {
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
        if (!selectingItem)
        {
            selectedItem = null;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !gm.inPauseMenu && !gm.inTradeMenu)
        {
            inventoryWindowRect = new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8);
            ToggleInv();
        }
    }

    void ItemData(string name)
    {
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

        if (showInv)
        {
            inventoryWindowRect = ClampToScreen(GUI.Window(0, inventoryWindowRect, InventoryWindow, "Inventory"));
            // if (!inventoryWindowRect.Contains(Event.current.mousePosition))
            // {
            //     selectedItem = null;
            // }
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
        GUI.skin.box.wordWrap = true;
        ItemTooltips();
        // Background
        //GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8), "");
        // Emphasised header
        //GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 1), "Inventory");
        // For loop list
        // TODO: Need to change the end inv.Count * scrH
        invScrollPosition = GUI.BeginScrollView(new Rect(0, 0, scrW * 8, scrH * 8), invScrollPosition, new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 7.5f, inv.Count * scrH + 50));
        for (int i = 0; i < inv.Count; i++)
        {
            // Old code
            //if (GUI.Button(new Rect(scrW, scrH + i * (scrH*0.5f), 3 * scrW, 0.5f * scrH), inv[i].Name))
            //{
            //    selectedItem = inv[i];
            //}

            // Buttons for each inventory item
            Rect r = new Rect(scrW, scrH + i * (scrH), scrW, scrH);
            Rect b = new Rect(scrW, scrH + i * (scrH), scrW * 6, scrH);
            GUI.Box(b, inv[i].Name);
            GUI.DrawTexture(r, inv[i].Icon);
            if (b.Contains(Event.current.mousePosition))
            {
                selectingItem = true;
                selectedItem = inv[i];
            }
            else
            {
                selectingItem = false;
            }
            if (GUI.Button(new Rect(scrW * 7, scrH + i * (scrH), scrW, scrH), "Drop"))
            {
                inv.Remove(inv[i]);
            }

            // Alternateive code
            //Rect position = new Rect(scrW, scrH + i * (scrH * 0.5f), 3 * scrW, 0.5f * scrH);
            //GUI.DrawTexture(position, inv[i].Icon);
            //if (GUI.Button(position, "", new GUIStyle()))
            //{
            //    selectedItem = inv[i];
            //}
        }
        GUI.EndScrollView();
        GUI.DragWindow();
    }

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

    Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return (r);
    }
}
