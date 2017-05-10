using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private float scrW;
    private float scrH;
    private bool inOptions = false;
    private bool showResOptions = false;
    private bool fullscreenToggle;
    public Vector2 resScrollPosition = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        fullscreenToggle = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        // Ratio
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;

        // Determine what menu to display
        if (!inOptions)
        {
            MainMenuFunc();
        }
        else if (inOptions)
        {
            OptionsMenuFunc();
            if (showResOptions)
            {
                ResOptionsFunc();
            }
        }
    }

    void MainMenuFunc()
    {
        GUI.Box(new Rect(scrW, scrH, scrW * 6, scrH * 2), "GAME NAME");
        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW * 2, scrH), "Start"))
        {
            SceneManager.LoadScene(1);
        }
        if (GUI.Button(new Rect(scrW, scrH * 5f, scrW * 2, scrH), "Options"))
        {
            inOptions = true;
        }
        if (GUI.Button(new Rect(scrW, scrH * 6.5f, scrW * 2, scrH), "Quit"))
        {
            Application.Quit();
        }
    }

    void OptionsMenuFunc()
    {
        GUI.Box(new Rect(scrW, scrH, scrW * 14, scrH * 6), "Options");

        GUI.Box(new Rect(scrW * 2, scrH * 2, scrW * 4, scrH * 4), "Inputs");

        GUI.BeginGroup(new Rect(scrW * 2.5f, scrH * 2.5f, scrW * 4, scrH * 4));

        if (GUI.Button(new Rect(scrW, scrH, scrW, scrH), "W"))
        {            

        }
        if (GUI.Button(new Rect(0, scrH * 2, scrW, scrH), "A"))
        {

        }
        if (GUI.Button(new Rect(scrW, scrH * 2, scrW, scrH), "S"))
        {

        }
        if (GUI.Button(new Rect(scrW * 2, scrH * 2, scrW, scrH), "D"))
        {

        }

        GUI.EndGroup();

        GUI.Box(new Rect(scrW * 10, scrH * 2, scrW * 4, scrH * 4), "Screen");

        GUI.BeginGroup(new Rect(scrW * 10.5f, scrH * 2.5f, scrW * 3, scrH * 5f));

        if (GUI.Button(new Rect(0, 0, scrW * 3, scrH * .5f), "Choose Resoltion v"))
        {
            showResOptions = !showResOptions;
        }

        // Only show fullscreen toggle when resolution dropdown isn't shown (to avoid accidental toggling)
        if (!showResOptions)
        {
            fullscreenToggle = GUI.Toggle(new Rect(0, scrH, scrW * 3, scrH * .5f), fullscreenToggle, "Toggle Fullscreen");

            Screen.fullScreen = fullscreenToggle;
        }

        GUI.EndGroup();

        if (GUI.Button(new Rect(scrW * 7f, scrH * 8, scrW * 2, scrH), "Back"))
        {
            inOptions = false;
        }
    }

    void ResOptionsFunc()
    {
        //Old code
        //List<string> res = new List<string>();
        //res.AddMany("1280×800", "1440×900", "1680×1050", "1920×1200", "2560×1600");

        // Set up resolutions for button labels
        string[] res = new string[] { "1024×576", "1152×648", "1280×720", "1280×800", "1366×768", "1440×900", "1600×900", "1680×1050", "1920×1080", "1920×1200", "2560×1440", "2560×1600", "3840×2160" };
        
        // Set up resolution values to set (TODO could be improved)
        int[] resW = new int[] { 1024, 1152, 1280, 1280, 1366, 1440, 1600, 1680, 1920, 1920, 2560, 2560, 3840 };
        int[] resH = new int[] { 576, 648, 720, 800, 768, 900, 900, 1050, 1080, 1200, 1440, 1600, 2160 };

        // Create GUI style solid black for scrollable resolutions
        Texture2D black = new Texture2D(1, 1);
        black.SetPixel(1, 1, Color.black);
        GUIStyle solidBlack = new GUIStyle();
        solidBlack.normal.background = black;


        // Group for the drop down menu
        GUI.BeginGroup(new Rect(scrW * 10.5f, scrH * 3, scrW * 3, scrH * 4));

        resScrollPosition = GUI.BeginScrollView(new Rect(0, 0, scrW * 3, scrH * 4), resScrollPosition, new Rect(0, 0, scrW * 2.6f, scrH * 13));

        GUI.Box(new Rect(0, 0, scrW * 3, scrH * 13), "", solidBlack);

        for (int i = 0; i < 13; i++)
        {
            if (GUI.Button(new Rect(0, scrH * i, scrW * 2.7f, scrH), res[i]))
            {
                Screen.SetResolution(resW[i], resH[i], Screen.fullScreen);
            }
        }

        GUI.EndScrollView();

        GUI.EndGroup();
    }
}
