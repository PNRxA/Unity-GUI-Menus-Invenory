using UnityEngine;

public class Item
{

    private int _id;
    private string _name;
    private string _description;
    private string _mesh;
    private Texture2D _icon;
    private ItemType _type;
    private int _value;
    private int _heal;
    private int _damage;

    public void Init()
    {
        _name = "Unknown";
        _description = "Unknown";
        _value = 0;
        _type = ItemType.Craftable;
    }

    public void Init(string name, string description, int value, string meshName, ItemType type)
    {
        _name = name;
        _description = description;
        _value = value;
        _mesh = meshName;
        _type = type;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string MeshName
    {
        get { return _mesh; }
        set { _mesh = value; }
    }

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int Heal
    {
        get { return _heal; }
        set { _heal = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public ItemType Type
    {
        get { return _type; }
        set { _type = value; }
    }

}

public enum ItemType
{
    Armour,
    Weapon,
    Craftable,
    Coins,
    Potion,
    Consumable,
    Quest
}
