using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Box : MonoBehaviour {
    public Dialogue dialogue;

    public static DialogueTrigger_Box instance = null;

    private void Start() {
        instance = this;
    }

    /*
    public void TriggerDialogue_box() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        // DialogueManager.instance.StartDialogue(dialogue);
    }
    */
}
