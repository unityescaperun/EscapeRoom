using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    private itemDatabase db;

    public int slotX, slotY;
    public List<Item> slots = new List<Item>();

    private bool showInventory = false;
    public GUISkin skin;

    void Start() {
        for(int i = 0; i < slotX * slotY; i++) {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();
        inventory.Add(db.items[0]);
        inventory[1] = db.items[1];

        for(int i = 0; db.items[i] != null; i++) {
            if (db.items[i] != null) {
                inventory[i] = db.items[i];
            }
            else {
                Debug.Log("Inventory Full");
            }
        }
    }

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
            showInventory = !showInventory;
        }  
    }

    void OnGUI() {
        GUI.skin = skin;

        if (showInventory) {
            DrawInventory();
        }
    }

    void DrawInventory() {
        int k = 0;
        for(int i = 0; i < slotX; i++) {
            for(int j = 0; j < slotY; j++) {
                Rect slotRect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));

                slots[k] = inventory[k];
                if (slots[k].itemName != null) {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                }

                k++;
            }
        }
    }
}
