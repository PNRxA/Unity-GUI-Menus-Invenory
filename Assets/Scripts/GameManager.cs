using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool inMenu = false;
    public bool inPauseMenu = false;
    public bool inTradeMenu = false;

    // Update is called once per frame
    void Update()
    {
        // If in any menu then show the cursor and freezetime otherwise lock/hid/resume time
        if (inMenu || inPauseMenu || inTradeMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
