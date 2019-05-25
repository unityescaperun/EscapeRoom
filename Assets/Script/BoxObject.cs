using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour {
    public int PosX;
    public int PosY;

    void Update() {
        this.transform.position = new Vector2(PosX, PosY);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }
}

