using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool showPauseMenu;
    public Inventory inv;
    public GameManager gm;
    public GUISkin mainMenuSkin;
    public GUISkin optionsMenuSkin;
    private float scrW;
    private float scrH;
    private bool inOptions = false;
    private bool showResOptions = false;
    private bool fullscreenToggle;
    public Vector2 resScrollPosition = Vector2.zero;
    public AudioSource audi;
    public bool mute;
    public float audioSlider, volMute;
    private string buttonName = Screen.width + "x" + Screen.height; // Resolution drop down menu will be set to screen resolution
    public KeyCode forward, backward, right, left, jump, sprint, crouch; //Keycodes to use
    public KeyCode holdingKey; //Key value to replace

    // Use this for initialization
    void Start()
    {
        // Set keys initially (to be used in future)

        jump = KeyCode.Space;//setting jump to Space

        sprint = KeyCode.LeftShift;//settin sprint to Left Shift

        crouch = KeyCode.LeftControl;//setting crouch to Left Control

        // Set toggle to current fullscreen status
        fullscreenToggle = Screen.fullScreen;

        // If there are player prefs load them in
        if (PlayerPrefs.HasKey("mute"))
        {
            // Set audio
            audi.volume = PlayerPrefs.GetFloat("volume");
            // Set binds
            forward = (KeyCode)PlayerPrefs.GetInt("forwardKeycode", (int)KeyCode.W);
            backward = (KeyCode)PlayerPrefs.GetInt("backwardKeycode", (int)KeyCode.S);
            left = (KeyCode)PlayerPrefs.GetInt("leftKeycode", (int)KeyCode.A);
            right = (KeyCode)PlayerPrefs.GetInt("rightKeycode", (int)KeyCode.D);
            // Set mute
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                mute = false;
                audi.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                mute = true;
                audi.volume = 0;
                volMute = PlayerPrefs.GetFloat("volume");
            }
        }
        else
        {
            // Set up binds if they don't exist 

            forward = KeyCode.W;//setting forward to W

            backward = KeyCode.S;//setting backward to S

            left = KeyCode.A;//setting left to A

            right = KeyCode.D;//setting right to D
        }

        // Slider is equal to current volume
        audioSlider = audi.volume;
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether in game
        if (gm)
        {
            // Enable pause menu or disable it if it's already up
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                showPauseMenu = !showPauseMenu;
                gm.inPauseMenu = !gm.inPauseMenu;
                showResOptions = false;
                if (inOptions)
                {
                    inOptions = false;
                    SaveOptions();
                }
                if (inv.showInv)
                {
                    inv.ToggleInv();
                }
            }
            // Audio slider = volume
            if (audioSlider != audi.volume)
            {
                audi.volume = audioSlider;
            }
        }
        if (audioSlider != audi.volume)
        {
            audi.volume = audioSlider;
        }
    }

    void OnGUI()
    {
        // Ratio
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;

        // Determine what menu to display
        // If in game
        if (gm)
        {
            // Show pause menu if not already in pause menu or options
            if (!inOptions && showPauseMenu)
            {
                ShowPauseMenu();
            }
            // Show options menu if inOptions & in game
            else if (inOptions)
            {
                OptionsMenuFunc();
                // Show res dowpdown when clicked
                if (showResOptions)
                {
                    ResOptionsFunc();
                }
            }
        // If not in game and not inOptions show the main menu
        } else if (!inOptions)
        {
            MainMenuFunc();
        }
        // If not in game and inOptions show options
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
        GUI.skin = mainMenuSkin;
        GUI.Box(new Rect(scrW, scrH, scrW * 6, scrH * 2), "");
        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW * 2, scrH), "Start"))
        {
            // Start game by loading first scene
            SceneManager.LoadScene(1);
        }
        if (GUI.Button(new Rect(scrW, scrH * 5f, scrW * 2, scrH), "Options"))
        {
            // Go into options menu
            inOptions = true;
        }
        if (GUI.Button(new Rect(scrW, scrH * 6.5f, scrW * 2, scrH), "Quit"))
        {
            // Quit game to desktop
            Application.Quit();
        }
        GUI.skin = null;
    }

    void ShowPauseMenu()
    {
        GUI.skin = optionsMenuSkin;
        GUI.BeginGroup(new Rect(scrW * 6, scrH, scrW * 4, scrH * 10));
        GUI.Box(new Rect(0, 0, scrW * 4, scrH), "Paused");

        if (GUI.Button(new Rect(scrW, scrH * 2, scrW * 2, scrH), "Resume"))
        {
            // Hide pause menu
            gm.inPauseMenu = false;
            showPauseMenu = false;
            // Uncomment to debug if first load works by clearing player prefs
            //PlayerPrefs.DeleteAll();
        }

        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW * 2, scrH), "Options"))
        {
            // Show options menu
            inOptions = true;
        }

        if (GUI.Button(new Rect(scrW, scrH * 5, scrW * 2, scrH), "Quit"))
        {
            // Quite to main menu
            SceneManager.LoadScene(0);
        }
        GUI.EndGroup();
        GUI.skin = null;
    }

    void OptionsMenuFunc()
    {
        Event e = Event.current;
        GUI.skin = optionsMenuSkin;
        GUI.Box(new Rect(scrW, scrH, scrW * 14, scrH * 6), "Options");

        GUI.Box(new Rect(scrW * 2, scrH * 2, scrW * 4, scrH * 4), "Inputs");

        GUI.BeginGroup(new Rect(scrW * 2.5f, scrH * 2.5f, scrW * 4, scrH * 4));

        // Buttons for key binding

        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None))
        {
            if (GUI.Button(new Rect(scrW, scrH, scrW, scrH), forward.ToString()))
            {
                holdingKey = forward;//set our holding key to the key of this button

                forward = KeyCode.None;//set this button to none allowing only this one to be edited
            }
        }
        else
        {
            GUI.Box(new Rect(scrW, scrH, scrW, scrH), forward.ToString());//if any other button except for this one is set to none make this one a box so it cant be changed
        }

        if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None))
        {
            if (GUI.Button(new Rect(0, scrH * 2, scrW, scrH), left.ToString()))
            {
                holdingKey = left;//set our holding key to the key of this button

                left = KeyCode.None;//set this button to none allowing only this one to be edited
            }
        }
        else
        {
            GUI.Box(new Rect(0, scrH * 2, scrW, scrH), left.ToString());//if any other button except for this one is set to none make this one a box so it cant be changed
        }

        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None))
        {
            if (GUI.Button(new Rect(scrW, scrH * 2, scrW, scrH), backward.ToString()))
            {
                holdingKey = backward;//set our holding key to the key of this button

                backward = KeyCode.None;//set this button to none allowing only this one to be edited
            }
        }
        else
        {
            GUI.Box(new Rect(scrW, scrH * 2, scrW, scrH), backward.ToString());//if any other button except for this one is set to none make this one a box so it cant be changed
        }

        if (!(forward == KeyCode.None || left == KeyCode.None || backward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None))
        {
            if (GUI.Button(new Rect(scrW * 2, scrH * 2, scrW, scrH), right.ToString()))
            {
                holdingKey = right;//set our holding key to the key of this button

                right = KeyCode.None;//set this button to none allowing only this one to be edited
            }
        }
        else
        {
            GUI.Box(new Rect(scrW * 2, scrH * 2, scrW, scrH), right.ToString());//if any other button except for this one is set to none make this one a box so it cant be changed
        }

        // Key binding

        if (forward == KeyCode.None)//if forward is set to none and

        {

            if (e.isKey)//if an event is triggerent by a key press

            {

                Debug.Log("Detected key code: " + e.keyCode);//write in my console what that key is

                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch))

                //if that key is not the same as any other movements key press than

                {

                    forward = e.keyCode;//set forward to the events key press

                    holdingKey = KeyCode.None;//set the holding key to none

                }

                else //otherwise if it is the same as another key

                {

                    forward = holdingKey;//set forward back to what holding key is now

                    holdingKey = KeyCode.None;//set the holding key to none

                }

            }

        }

        if (backward == KeyCode.None)//if backward is set to none and

        {

            if (e.isKey)//if an event is triggerent by a key press

            {

                Debug.Log("Detected key code: " + e.keyCode);//write in my console what that key is

                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch))

                //if that key is not the same as any other movements key press than

                {

                    backward = e.keyCode;//set backward to the events key press

                    holdingKey = KeyCode.None;

                }

                else//otherwise if it is the same as another key

                {

                    backward = holdingKey;//set backward back to what holding key is now

                    holdingKey = KeyCode.None;//set the holding key to none

                }

            }

        }

        if (left == KeyCode.None)//if left is set to none and

        {

            if (e.isKey)//if an event is triggerent by a key press

            {

                Debug.Log("Detected key code: " + e.keyCode);//write in my console what that key is

                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch))

                //if that key is not the same as any other movements key press than

                {

                    left = e.keyCode;//set left to the events key press

                    holdingKey = KeyCode.None;//set the holding key to none

                }

                else//otherwise if it is the same as another key

                {

                    left = holdingKey;//set left back to what holding key is now

                    holdingKey = KeyCode.None;//set the holding key to none

                }



            }

        }

        if (right == KeyCode.None)//if right is set to none and

        {

            if (e.isKey)//if an event is triggerent by a key press

            {

                Debug.Log("Detected key code: " + e.keyCode);//write in my console what that key is

                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch))

                //if that key is not the same as any other movements key press than

                {

                    right = e.keyCode;//set right to the events key press

                    holdingKey = KeyCode.None;//set the holding key to none

                }

                else//otherwise if it is the same as another key

                {

                    right = holdingKey;//set right back to what holding key is now

                    holdingKey = KeyCode.None;//set the holding key to none

                }

            }

        }
        GUI.EndGroup();

        GUI.Box(new Rect(scrW * 6, scrH * 2, scrW * 4, scrH * 4), "Sound");

        GUI.BeginGroup(new Rect(scrW * 6.5f, scrH * 2.5f, scrW * 4, scrH * 4));
        // If not muted slider = current volume
        if (!mute)
        {
            audioSlider = GUI.HorizontalSlider(new Rect(0, scrH, 3 * scrW, .5f * scrH), audioSlider, 0f, 1f);
        }
        else
        {
            GUI.HorizontalSlider(new Rect(0, scrH, 3 * scrW, .5f * scrH), audioSlider, 0f, 1f);
        }
        // Mute audio
        if (GUI.Button(new Rect(0, scrH * 2, scrW * 2, scrH), "Mute/Unmute"))
        {
            ToggleMute();
        }

        GUI.EndGroup();

        GUI.Box(new Rect(scrW * 10, scrH * 2, scrW * 4, scrH * 4), "Screen");

        GUI.BeginGroup(new Rect(scrW * 10.5f, scrH * 2.5f, scrW * 3, scrH * 5f));

        if (GUI.Button(new Rect(0, 0, scrW * 3, scrH * .5f), buttonName))
        {
            // Show res dropdown menu
            showResOptions = !showResOptions;
        }

        // Only show fullscreen toggle when resolution dropdown isn't shown (to avoid accidental toggling when in dropdown)
        if (!showResOptions)
        {
            fullscreenToggle = GUI.Toggle(new Rect(0, scrH, scrW * 3, scrH * .5f), fullscreenToggle, "Toggle Fullscreen");

            Screen.fullScreen = fullscreenToggle;
        }

        GUI.EndGroup();

        if (GUI.Button(new Rect(scrW * 7f, scrH * 8, scrW * 2, scrH), "Back"))
        {
            // Go back to previous menu and also save options
            SaveOptions();
            inOptions = false;
        }
        GUI.skin = null;
    }

    bool ToggleMute()
    {
        // Un/Mute audio
        if (mute)
        {
            audioSlider = volMute;
            mute = false;
            return false;
        }
        else
        {
            volMute = audioSlider;
            audioSlider = 0;
            mute = true;
            return true;
        }
    }

    void ResOptionsFunc()
    {
        // Set up resolutions for button labels
        string[] res = new string[] { "1024×576", "1152×648", "1280×720", "1280×800", "1366×768", "1440×900", "1600×900", "1680×1050", "1920×1080", "1920×1200", "2560×1440", "2560×1600", "3840×2160" };
        
        // Set up resolution values to set (TODO could be improved)
        int[] resW = new int[] { 1024, 1152, 1280, 1280, 1366, 1440, 1600, 1680, 1920, 1920, 2560, 2560, 3840 };
        int[] resH = new int[] { 576, 648, 720, 800, 768, 900, 900, 1050, 1080, 1200, 1440, 1600, 2160 };

        // Create GUI style solid black (kek) for scrollable resolutions
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
                // Set resolution based on which button was pressed (array[i] name, array[i] width, array[i] height)
                Screen.SetResolution(resW[i], resH[i], Screen.fullScreen);
                buttonName = res[i];
                showResOptions = false;
            }
        }

        GUI.EndScrollView();

        GUI.EndGroup();
    }

    // Save all options
    void SaveOptions()
    {
        // Write PlayerPrefs depending on mute status
        if (!mute)
        {
            PlayerPrefs.SetInt("mute", 0);
            PlayerPrefs.SetFloat("volume", audioSlider);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
            PlayerPrefs.SetFloat("volume", volMute);
        }

        // Save keybinds
        PlayerPrefs.SetInt("forwardKeycode", (int)forward);
        PlayerPrefs.SetInt("backwardKeycode", (int)backward);
        PlayerPrefs.SetInt("leftKeycode", (int)left);
        PlayerPrefs.SetInt("rightKeycode", (int)right);
    }

}
