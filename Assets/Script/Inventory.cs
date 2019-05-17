using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();
    private itemDatabase db;

    public int slotX, slotY;
    public List<Item> slots = new List<Item>();

    private bool showInventory = false;
    public GUISkin skin;

    private bool showTooltip;
    private string tooltip;

    private bool dragItem = false;
    private Item draggedItem;
    private int prevIndex;

    private bool CombItem;
    GameObject player;

    private void Awake() {
        instance = this;
    }

    void Start() {
        for(int i = 0; i < slotX * slotY; i++) {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
            showInventory = !showInventory;
        }  
    }

    void OnGUI() {
        tooltip = "";
        GUI.skin = skin;

        if (showInventory) {
            DrawInventory();
            if (showTooltip) {
                GUI.Box(new Rect(Event.current.mousePosition.x+5, 
                    Event.current.mousePosition.y + 2, 200, 200), 
                    tooltip, skin.GetStyle("tooltip"));
            }
            if (dragItem) {
                GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 5, 
                    Event.current.mousePosition.y - 5, 50, 50),
                    draggedItem.itemIcon);
            }
        }

        if (showTooltip) {
            GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200)
                , tooltip, skin.GetStyle("tooltip"));
        }
    }

    void DrawInventory() {
        int k = 0;
        Event e = Event.current;
        for (int j = 0; j < slotY; j++) {
            for (int i = 0; i < slotX; i++) {
                Rect slotRect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));

                slots[k] = inventory[k];

                if (slots[k].itemName != null) {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                    if (slotRect.Contains(e.mousePosition)) {
                        tooltip = CreateTooltip(slots[k]);

                        showTooltip = true;

                        if(e.button == 0&& e.type == EventType.MouseDrag && !dragItem) {
                            dragItem = true;
                            prevIndex = k;
                            draggedItem = slots[k];
                            inventory[k] = new Item();
                        }
                        if(e.type == EventType.MouseUp && dragItem) {
                            if (CombineItem(draggedItem, inventory[k])) {
                                AddItem(inventory[k].itemID + draggedItem.itemID);
                                inventory[k] = new Item();
                                dragItem = false;
                                draggedItem = null;
                            }
                            else {
                                inventory[prevIndex] = inventory[k];
                                inventory[k] = draggedItem;
                                dragItem = false;
                                draggedItem = null;
                            }
                        }                    
                    }
                }
                else {
                    if (slotRect.Contains(e.mousePosition)) {
                        if(e.type == EventType.MouseUp && dragItem) {
                            inventory[k] = draggedItem;
                            dragItem = false;
                            draggedItem = null;
                        }
                    }
                }

                if(tooltip == "") {
                    showTooltip = false;
                }

                k++;
            }
        }
    }

    string CreateTooltip(Item item) {
        tooltip = "Item name : " + item.itemName + "\nitem Desc : " + item.itemDes;
        return tooltip;
           
    }

    bool CombineItem(Item item1, Item item2) {
        if(item1.itemID == 1001 && item2.itemID == 1002 || item1.itemID == 1002 && item2.itemID == 1001) {
            return true;
        }
        return false;
    }
    
    public void AddItem(int id) {
        for (int i = 0; i < inventory.Count; i++) {
            if(inventory[i].itemName == null) {
                for(int j = 0; j < db.items.Count; j++) {
                    if(db.items[j].itemID == id) {
                        inventory[i] = db.items[j];
                        return;
                    }
                }
            }
        }
        Debug.Log("Add item");
    }

    public bool inventoryContains(int id) {
        for(int i = 0; i < inventory.Count; i++) {
            if (inventory[i].itemID == id)
                return true;
        }
        return false;
    }

    void RemoveItem(int id) {
        for(int i = 0; i < inventory.Count; i++) {
            if(inventory[i].itemID == id) {
                inventory[i] = new Item();
                break;
            }
        }
    }
}
