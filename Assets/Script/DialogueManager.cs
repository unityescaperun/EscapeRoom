using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance = null;
    private Queue<string> sentences;

    public Text nameText;
    public Text dialogueText;

    public Animator ani;

    bool hintOn;

    private void Awake() {
        instance = this;
    }

    void Start() {
        sentences = new Queue<string>();
        hintOn = false;
    }

    void Update() {
        if (Input.GetButtonDown("Hint")) {
            hintOn = !hintOn;
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        ani.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartDialogue2(Message message) {
        ani.SetBool("isOpen", true);
        nameText.text = "aa";
        sentences.Clear();

        foreach (string sentence in message.dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue() {
        ani.SetBool("isOpen", false);
    }

}
