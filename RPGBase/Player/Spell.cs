using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public Texture icon;
    public string name;
    public string description;
    public int id;

    public Spell(Spell d)
    {
        icon = d.icon;
        name = d.name;
        description = d.description;
        id = d.id;
    }
}
