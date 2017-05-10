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
        GUI.Box(new Rect(scrW * 4, 0, scrW * 4, scrH), "Paused");
    }
}
