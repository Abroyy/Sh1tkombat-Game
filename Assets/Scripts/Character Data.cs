using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterData
{
    public string characterName;
    public int cost;
    public bool isUnlocked;
    public Image kilit;
    public GameObject panel;
    public Button Button;
}
