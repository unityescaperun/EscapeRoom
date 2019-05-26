using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Warning : MonoBehaviour {
    public Dialogue dialogue;
    
    public static DialogueTrigger_Warning instance = null;

    private void Start() {
        instance = this;
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        // DialogueManager.instance.StartDialogue(dialogue);
    }
}
