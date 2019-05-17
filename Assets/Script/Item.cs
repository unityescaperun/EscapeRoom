using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    public string itemName;
    public int itemID;
    public string itemDes;
    public Texture2D itemIcon;

    public Item() {

    }

    public Item(string img, string name, int id, string desc) {
        itemName = name;
        itemID = id;
        itemDes = desc;

        itemIcon = Resources.Load<Texture2D>("itemIcons/" + img);
    }
}
