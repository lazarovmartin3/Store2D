using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public int id;
    public string name;
    public string description;
    public Sprite sprite;
    public int cost;

    [Serializable]
    public enum BodyPart { head, torso, weapon };
    public BodyPart body;
}
