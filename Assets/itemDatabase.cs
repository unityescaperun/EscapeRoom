using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        items.Add(new Item("Item1", "Item1", 1001, "Key Material"));
        items.Add(new Item("Item2", "Item2", 1002, "Key Material"));
        items.Add(new Item("Key", "Key", 2003, "Key"));
        items.Add(new Item("Item5", "Item5", 1004, "Key Material"));
        items.Add(new Item("Item6", "Item6", 1005, "Key Material"));
        items.Add(new Item("Key2", "Key2", 2009, "Key"));
    }
}
