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

    private int _selectedIndex; // for drag and drop stuff later
    private float scrW;
    private float scrH;

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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            return true;
        }
        else
        {
            gm.inMenu = false;
            showInv = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            return false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
    }

    void ItemData(string name)
    {
        GUI.DrawTexture(new Rect(scrW * 12f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
        GUI.Box(new Rect(scrW * 10.5f, 4 * scrH, scrW * 5, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
        if (GUI.Button(new Rect(scrW * 11f, 6.5f * scrH, scrW * 4, scrH), name))
        {

        }
    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;

        GUI.skin.box.wordWrap = true;

        if (showInv)
        {
            //full background
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //background
            GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8), "");
            //emphasised header
            GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 1), "Inventory");
            //for loop list
            //TODO: Need to change the end inv.Count * scrH
            invScrollPosition = GUI.BeginScrollView(new Rect(scrW * 0.5f, scrH * 1.5f, scrW * 8, scrH * 7), invScrollPosition, new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 7, inv.Count * scrH + 50));
            for (int i = 0; i < inv.Count; i++)
            {
                //old code
                //if (GUI.Button(new Rect(scrW, scrH + i * (scrH*0.5f), 3 * scrW, 0.5f * scrH), inv[i].Name))
                //{
                //    selectedItem = inv[i];
                //}

                //buttons for each inventory item
                Rect r = new Rect(scrW, scrH + i * (scrH), scrW, scrH);
                Rect b = new Rect(scrW, scrH + i * (scrH), scrW * 6, scrH);
                GUI.Box(b, inv[i].Name);
                GUI.DrawTexture(r, inv[i].Icon);
                if (Input.GetMouseButton(0))
                {
                    if (b.Contains(Event.current.mousePosition))
                    {
                        selectedItem = inv[i];
                    }
                }
                if (GUI.Button(new Rect(scrW * 7, scrH + i * (scrH), scrW, scrH), "Drop"))
                {
                    inv.Remove(inv[i]);
                }

                //alternateive code
                //Rect position = new Rect(scrW, scrH + i * (scrH * 0.5f), 3 * scrW, 0.5f * scrH);
                //GUI.DrawTexture(position, inv[i].Icon);
                //if (GUI.Button(position, "", new GUIStyle()))
                //{
                //    selectedItem = inv[i];
                //}
            }
            GUI.EndScrollView();

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
    }
}
