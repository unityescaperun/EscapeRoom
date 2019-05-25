using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;
    
    public static DialogueTrigger instance = null;
    

    public void TriggerDialogue() {
        instance = this;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        // DialogueManager.instance.StartDialogue(dialogue);
    }
}
