using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCustomisation : MonoBehaviour
{

    public GUISkin mainMenuSkin;

    public Renderer character;
    public string charName = "Adventurer";

    public bool Rotating = true;
    public Rotate RotatingScript;

    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();

    public string[] classes = new[] { "Warrior", "Rogue", "Wizard" };
    public int strength, agility, intelligence;

    public int skinIndex, hairIndex, mouthIndex, eyeIndex, classIndex;
    public int skinMax, hairMax, mouthMax, eyeMax;
    public float playerHeight = 1;
    public float playerWidth = 1;

    private float scrW;
    private float scrH;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        RotatingScript = GameObject.FindObjectOfType<Rotate>();

        for (int i = 0; i < skinMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(textureTemp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(textureTemp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(textureTemp);
        }
        for (int i = 0; i < eyeMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(textureTemp);
        }

        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);

    }

    void Update()
    {
        if (Rotating)
        {
            RotatingScript.ySpeed = .5f;
        }
        else
        {
            RotatingScript.ySpeed = 0;
            GameObject.FindGameObjectWithTag("Player").gameObject.transform.rotation = Quaternion.Euler(0, 221, 0);
        }
        // Set the player model to the new height and width values
        RotatingScript.gameObject.transform.localScale = new Vector3(playerWidth, playerHeight, playerWidth);
    }

    void Save()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyeIndex", eyeIndex);
        PlayerPrefs.SetString("PlayerName", charName);
        PlayerPrefs.SetInt("Class", classIndex);
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Agility", agility);
        PlayerPrefs.SetInt("Intelligence", intelligence);
        PlayerPrefs.SetFloat("Height", playerHeight);
        PlayerPrefs.SetFloat("Width", playerWidth);
    }

    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0;
        Texture2D[] textures = new Texture2D[0];
        int matIndex = 0;

        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Eyes":
                index = eyeIndex;
                max = eyeMax;
                textures = eyes.ToArray();
                matIndex = 4;
                break;
            default:
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = character.materials;
        mat[matIndex].mainTexture = textures[index];
        character.materials = mat;

        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyeIndex = index;
                break;
        }
    }

    void OnGUI()
    {
        // Define the ratio
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        // Set the Gui menu skin
        GUI.skin = mainMenuSkin;
        Customisation();
        StatsView();
        GUI.skin = null;
    }
    // Fuction to generate customisation options 
    void Customisation()
    {
        // Int used for positioning elements
        int i = 0;
        // Toggle rotating character
        Rotating = GUI.Toggle(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), Rotating, "Rotate Character?");
        i++;
        // Elements for customisation with forward and back buttons
        TripleCustomisation("Skin", i, true);
        i++;
        TripleCustomisation("Hair", i, true);
        i++;
        TripleCustomisation("Mouth", i, true);
        i++;
        TripleCustomisation("Eyes", i, true);
        i++;
        // Choose class  
        TripleCustomisation("Classes", i, false);
        i++;
        // Character name
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
        i++;
        // Random and reset functions for customisations
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Random"))
        {
            TextureFunc("Random");
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Reset"))
        {
            TextureFunc("Reset");
        }
        i++;
        // Save and load into the game
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Create Character"))
        {
            Save();
            SceneManager.LoadScene(1);
        }
        i++;
        // For playerHeight and player Width, set the bar to the value and let the value change the bar to a max of 2f
        GUI.Box(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, scrH), "Height: " + Mathf.RoundToInt(playerHeight));
        i++;
        playerHeight = GUI.HorizontalSlider(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), playerHeight, 1.0F, 2.0F);
        i++;
        GUI.Box(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, scrH), "Width: " + Mathf.RoundToInt(playerWidth));
        i++;
        playerWidth = GUI.HorizontalSlider(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), playerWidth, 1.0F, 2.0F);
    }

    // Generate a triple GUI element to select a cusomisation texture
    void TripleCustomisation(string texture, int pos, bool isTexture)
    {
        // If you're a class changing thing display the class name
        if (!isTexture)
        {
            texture = classes[classIndex];
        }

        if (GUI.Button(new Rect(0.25f * scrW, scrH + pos * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            if (isTexture)
            {
                SetTexture(texture, -1);
            }
            else
            {
                SetClass(-1);
            }
        }

        GUI.Box(new Rect(0.75f * scrW, scrH + pos * (0.5f * scrH), scrW, 0.5f * scrH), texture);

        if (GUI.Button(new Rect(1.75f * scrW, scrH + pos * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            if (isTexture)
            {
                SetTexture(texture, 1);
            }
            else
            {
                SetClass(1);
            }
        }
    }

    void StatsView()
    {
        // Determine stats based on selected class
        switch (classIndex)
        {
            case 0:
                strength = 2;
                agility = 1;
                intelligence = 1;
                break;
            case 1:
                strength = 1;
                agility = 2;
                intelligence = 1;
                break;
            case 2:
                strength = 1;
                agility = 1;
                intelligence = 2;
                break;
        }
        GUI.Box(new Rect(scrW * 6, scrH, scrW * 4, scrH * 3), "Stats");
        GUI.Box(new Rect(scrW * 6, scrH * 2, scrW * 4, scrH * 2), "\n\nStrength: " + strength + "\nIntelligence: " + intelligence + "\nAgility: " + agility);
    }

    // Extra functions for dealing with textures
    void TextureFunc(string Decider)
    {
        switch (Decider)
        {
            // Randomise textures
            case "Random":
                SetTexture("Skin", Random.Range(0, skinMax - 1));
                SetTexture("Hair", Random.Range(0, hairMax - 1));
                SetTexture("Mouth", Random.Range(0, mouthMax - 1));
                SetTexture("Eyes", Random.Range(0, eyeMax - 1));
                break;
            // Reset textures to default values
            case "Reset":
                skinIndex = 0;
                hairIndex = 0;
                mouthIndex = 0;
                eyeIndex = 0;
                SetTexture("Skin", 0);
                SetTexture("Hair", 0);
                SetTexture("Mouth", 0);
                SetTexture("Eyes", 0);
                break;
        }
    }

    void SetClass(int dir)
    {
        // Scroll through classes based on dir
        classIndex += dir;
        if (classIndex < 0)
        {
            classIndex = 3 - 1;
        }
        if (classIndex > 3 - 1)
        {
            classIndex = 0;
        }
    }
}
