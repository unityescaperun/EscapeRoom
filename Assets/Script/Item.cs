using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 적용하는곳. 이미지 이름, 이름, id, 설명이 필요하다.
// 이미지의 경로는 /resources/itemicon/
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
