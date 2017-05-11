using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameManager gm;
    public Inventory inv;

    private float scrW;
    private float scrH;
    private bool showPauseMenu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showPauseMenu = !showPauseMenu;
            gm.inPauseMenu = !gm.inPauseMenu;
            if (inv.showInv)
            {
                inv.ToggleInv();
            }
        }
    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;

        if (showPauseMenu)
        {
            ShowPauseMenu();
        }

    }

    void ShowPauseMenu()
    {
        GUI.BeginGroup(new Rect(scrW * 6, scrH, scrW * 4, scrH * 10));
        GUI.Box(new Rect(0, 0, scrW * 4, scrH), "Paused");

        if (GUI.Button(new Rect(scrW, scrH * 2, scrW * 2, scrH), "Resume"))
        {
            gm.inPauseMenu = false;
            showPauseMenu = false;
        }

        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW * 2, scrH), ""))
        {

        }

        if (GUI.Button(new Rect(scrW, scrH * 5, scrW * 2, scrH), ""))
        {

        }
        GUI.EndGroup();
    }
}
