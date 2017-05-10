using UnityEngine;

public static class ItemDatabase
{
    public static Item createItem(int ID)
    {
        Item temp = new Item();

        int _id = 0;
        string _name = "";
        string _description = "";
        string _mesh = "";
        string _icon = "";
        ItemType _type = ItemType.Craftable;
        int _value = 0;
        int _heal = 0;
        int _damage = 0;

        switch (ID)
        {
            #region Conumable 000-099
            case 000:
                _name = "Apple";
                _description = "Munchie and Crunchies are in here somewhere";
                _mesh = "Apple_Mesh";
                _icon = "apple";
                _type = ItemType.Consumable;
                _value = 10;
                _heal = 5;
                _damage = 0;
                break;
            case 001:
                _name = "Cheese";
                _description = "It's like milk but not";
                _mesh = "Cheese_Mesh";
                _icon = "Cheese+Icon";
                _type = ItemType.Consumable;
                _value = 16;
                _heal = 7;
                _damage = 0;
                break;
            case 002:
                _name = "Ham";
                _description = "Not the rum ham!";
                _mesh = "Ham_Mesh";
                _icon = "Ham+Icon";
                _type = ItemType.Consumable;
                _value = 20;
                _heal = 10;
                _damage = 0;
                break;
            #endregion
            #region Potions 100-199
            case 100:
                _name = "Weak Health Potion";
                _description = "This might heal me";
                _mesh = "WeakHP_Mesh";
                _icon = "WeakHP+Icon";
                _type = ItemType.Potion;
                _value = 50;
                _heal = 25;
                _damage = 0;
                break;
            case 101:
                _name = "Health Potion";
                _description = "A regular health potion";
                _mesh = "RegHP_Mesh";
                _icon = "RegHP+Icon";
                _type = ItemType.Potion;
                _value = 75;
                _heal = 50;
                _damage = 0;
                break;
            case 102:
                _name = "Major Health Potion";
                _description = "Looks kinda scary";
                _mesh = "MajorHP_Mesh";
                _icon = "Apple+Icon";
                _type = ItemType.Potion;
                _value = 150;
                _heal = 120;
                _damage = 0;
                break;
            #endregion
            #region Craftable 200-299
            case 200:
                _name = "Iron Ore";
                _description = "Useless Rock";
                _mesh = "IronOre_Mesh";
                _icon = "IronOre+Icon";
                _type = ItemType.Craftable;
                _value = 50;
                _heal = 0;
                _damage = 0;
                break;
            case 201:
                _name = "Broken Twig";
                _description = "I'm yelling timber";
                _mesh = "BrokenTwig_Mesh";
                _icon = "BrokenTwig+Icon";
                _type = ItemType.Craftable;
                _value = 75;
                _heal = 0;
                _damage = 0;
                break;
            case 202:
                _name = "Mud";
                _description = "Who would want this?";
                _mesh = "Mud_Mesh";
                _icon = "Mud+Icon";
                _type = ItemType.Craftable;
                _value = 150;
                _heal = 0;
                _damage = 0;
                break;
            #endregion
            #region Armour 300-399
            case 300:
                _name = "Coppor Helm";
                _description = "Might not protect much";
                _mesh = "CopperHelm_Mesh";
                _icon = "A_Armor05";
                _type = ItemType.Armour;
                _value = 25;
                _heal = 0;
                _damage = 0;
                break;
            case 301:
                _name = "Iron Shield";
                _description = "A regular iron shield";
                _mesh = "IronShield_Mesh";
                _icon = "IronShield+Icon";
                _type = ItemType.Armour;
                _value = 60;
                _heal = 0;
                _damage = 0;
                break;
            case 302:
                _name = "Golden Boots";
                _description = "Hermedes once wore these";
                _mesh = "GoldenBoots_Mesh";
                _icon = "GoldenBoots+Icon";
                _type = ItemType.Armour;
                _value = 150;
                _heal = 0;
                _damage = 0;
                break;
            #endregion
            #region Weapon 400-499
            case 400:
                _name = "Iron Sword";
                _description = "Might not hurt much";
                _mesh = "IronSword_Mesh";
                _icon = "W_Sword001";
                _type = ItemType.Weapon;
                _value = 50;
                _heal = 0;
                _damage = 0;
                break;
            case 401:
                _name = "Iron Hammer";
                _description = "It's hammer time";
                _mesh = "IronHammer_Mesh";
                _icon = "IronHammer+Icon";
                _type = ItemType.Weapon;
                _value = 75;
                _heal = 0;
                _damage = 0;
                break;
            case 402:
                _name = "Golden Sword";
                _description = "A rare weapon";
                _mesh = "GoldenSword_Mesh";
                _icon = "GoldenSword+Icon";
                _type = ItemType.Weapon;
                _value = 150;
                _heal = 0;
                _damage = 0;
                break;
            #endregion
            #region Misc 500-599
            case 500:
                _name = "Golden Stick of DOOM";
                _description = "DOOOOOOOM";
                _mesh = "DoomStick_Mesh";
                _icon = "DoomStick+Icon";
                _type = ItemType.Quest;
                _value = 0;
                _heal = 0;
                _damage = 0;
                break;
            case 501:
                _name = "Stick of Trush";
                _description = "Only the best can hold this";
                _mesh = "TruthStick_Mesh";
                _icon = "TruthStick+Icon";
                _type = ItemType.Quest;
                _value = 0;
                _heal = 0;
                _damage = 0;
                break;
            case 502:
                _name = "Vape";
                _description = "420";
                _mesh = "Vape_Mesh";
                _icon = "Vape+Icon";
                _type = ItemType.Quest;
                _value = 0;
                _heal = 0;
                _damage = 0;
                break;
                #endregion
        }

        temp.Name = _name;
        temp.ID = _id;
        temp.Description = _description;
        temp.Type = _type;
        temp.Value = _value;
        temp.Damage = _damage;
        temp.Heal = _heal;
        temp.MeshName = _mesh;
        temp.Icon = Resources.Load("Icons/" + _icon) as Texture2D;


        return temp;
    }
}
