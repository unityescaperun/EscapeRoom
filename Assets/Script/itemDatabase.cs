using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터베이스. 여기에 필요한 아이템들을 작성하여 적용한다.
public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        items.Add(new Item("Item1", "Item1", 1001, "Key Material"));
        items.Add(new Item("Item2", "Item2", 1002, "Key Material"));
        items.Add(new Item("Key", "Key", 2003, "Key"));
    }
}
