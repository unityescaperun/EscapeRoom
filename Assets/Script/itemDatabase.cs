using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

    void Start() {
        items.Add(new Item("Item1", "Item1", 1001, "Key Material"));
        items.Add(new Item("Item2", "Item2", 1002, "Key Material"));
        items.Add(new Item("Key", "Key", 2003, "Key"));
    }
}
