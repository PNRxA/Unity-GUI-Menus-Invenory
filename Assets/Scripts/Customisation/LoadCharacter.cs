using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCharacter : MonoBehaviour
{

    public GameObject player;
    public Renderer character;
    public string charName = "Adventurer";
    public Player playerStats;

    public int skinIndex, hairIndex, mouthIndex, eyeIndex;

    // Use this for initialization
    void Start()
    {
        // Get components on start
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        character = player.GetComponentInChildren<SkinnedMeshRenderer>();
        LoadFunc();

        // Set player stats based on other player stats
        playerStats.maxHealth = 50 * playerStats.strength;
        playerStats.curHealth = playerStats.maxHealth;
        playerStats.maxStamina = 50 * playerStats.agility;
        playerStats.curStamina = playerStats.maxStamina;
        playerStats.maxMana = 50 * playerStats.intelligence;
        playerStats.curMana = playerStats.maxMana;
    }

    void LoadFunc()
    {
        // If playerprefs do not exist don't load them in otherwise load them in 
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
            SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
            SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
            SetTexture("Eyes", PlayerPrefs.GetInt("EyeIndex"));
            charName = PlayerPrefs.GetString("PlayerName");
            player.name = charName;

            playerStats.strength = PlayerPrefs.GetInt("Strength");
            playerStats.agility = PlayerPrefs.GetInt("Agility");
            playerStats.intelligence = PlayerPrefs.GetInt("Intelligence");

            playerStats.playerHeight = PlayerPrefs.GetFloat("Height");
            playerStats.playerWidth = PlayerPrefs.GetFloat("Width");
        }
    }

    void SetTexture(string type, int index)
    {
        // Set texture based on inputs
        Texture2D texture = null;
        int matIndex = 0;

        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 4;
                break;
        }

        Material[] mat = character.materials;
        mat[matIndex].mainTexture = texture;
        character.materials = mat;

    }
}
