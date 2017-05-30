using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private float scrW, scrH;
    private Player playerStats;
    public GameManager gm;
    public RawImage rImage;

    private Color playerIcon;
    // Use this for initialization
    void Start()
    {
        // Grab the player so can write/read stats
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make more red when lower health
        playerIcon = new Color(255, Mathf.RoundToInt(playerStats.curHealth / 255 * (playerStats.curHealth * 6)), Mathf.RoundToInt(playerStats.curHealth / 255 * (playerStats.curHealth * 6)), 100);
        // Doesn't work as intended but changes icon red when dead
        rImage.color = playerIcon;
    }

    void OnGUI()
    {
        // Define the ratio
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        // If not in a menu, display the GUi
        if (!gm.inMenu && !gm.inPauseMenu)
        {
            UpDisplay();
        }
    }

    void UpDisplay()
    {
        float i = .25f;
        // Health Bar
        GenerateBar(playerStats.curHealth, playerStats.maxHealth, "HP", i);
        i++;
        // Stamina Bar
        GenerateBar(playerStats.curStamina, playerStats.maxStamina, "SP", i);
        i++;
        // Mana Bar
        GenerateBar(playerStats.curMana, playerStats.maxMana, "MP", i);
    }

    void GenerateBar(float curValue, float maxValue, string barType, float pos)
    {
        // Generate a status bar based on inputs
        GUI.Box(new Rect(6 * scrW, pos * scrH, curValue * (4f * scrW) / maxValue, .5f * scrH), barType + ": " + curValue);
    }
}
