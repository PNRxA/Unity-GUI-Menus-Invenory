using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inv = new List<Item>();
    public bool showInv;
    public Item selectedItem;
    public GameManager gm;

    private int _selectedIndex; // for drag and drop stuff later

    void Start()
    {
        inv.Add(ItemDatabase.createItem(000));
        inv.Add(ItemDatabase.createItem(300));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
        inv.Add(ItemDatabase.createItem(400));
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

    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 10;

        if (showInv)
        {
            //full background
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            //background
            GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8), "Inventory");
            //for loop list
            for (int i = 0; i < inv.Count; i++)
            {
                //old code
                //if (GUI.Button(new Rect(scrW, scrH + i * (scrH*0.5f), 3 * scrW, 0.5f * scrH), inv[i].Name))
                //{
                //    selectedItem = inv[i];
                //}

                //buttons for each inventory item
                Rect r = new Rect(scrW, scrH + i * (scrH), scrW,  scrH);
                GUI.DrawTexture(r, inv[i].Icon);
                if (Input.GetMouseButton(0))
                {
                    if (r.Contains(Event.current.mousePosition))
                    {
                        selectedItem = inv[i];
                    }
                }
                
                //alternateive code
                //Rect position = new Rect(scrW, scrH + i * (scrH * 0.5f), 3 * scrW, 0.5f * scrH);
                //GUI.DrawTexture(position, inv[i].Icon);
                //if (GUI.Button(position, "", new GUIStyle()))
                //{
                //    selectedItem = inv[i];
                //}
            }
            if (selectedItem != null)
            {
                if (selectedItem.Type == ItemType.Armour)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Equip"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Coins)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Drop"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Consumable)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Eat"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Craftable)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Craft"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Potion)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Drink"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Quest)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Use"))
                    {

                    }
                }
                else if (selectedItem.Type == ItemType.Weapon)
                {
                    GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                    GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                    if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Equip"))
                    {

                    }
                }
            }
        }
    }
}
