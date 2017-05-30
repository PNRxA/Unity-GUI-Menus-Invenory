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
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        if (!gm.inMenu && !gm.inPauseMenu)
        {
            UpDisplay();
        }
    }

    void UpDisplay()
    {
        // Health
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, playerStats.curHealth * (4f * scrW) / playerStats.maxHealth, .5f * scrH), "HP: " + playerStats.curHealth);
        // Stamina
        GUI.Box(new Rect(6 * scrW, scrH, playerStats.curStamina * (4f * scrW) / playerStats.maxStamina, .5f * scrH), "SP: " + playerStats.curStamina);
        // Mana
        GUI.Box(new Rect(6 * scrW, 1.75f * scrH, playerStats.curMana * (4f * scrW) / playerStats.maxMana, .5f * scrH), "MP: " + playerStats.curMana);
    }
}
