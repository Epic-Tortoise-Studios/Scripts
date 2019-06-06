﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData {
    public int level;
    public int health;

    public PlayerData (PlayerSpecs player)
    {
        level = player.level;
        health = player.health;

    }

}
