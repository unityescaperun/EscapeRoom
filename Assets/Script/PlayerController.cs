using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    GameObject player;
    GameObject Key;
    bool inputKey;
    bool solveProblem;

    private Inventory inven;
    private Rigidbody2D rb2D;
    public Dialogue dialogue;
    public SpriteRenderer render;

    Vector2 MoveVelocity;
    public float Speed = 5f;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        inven = GetComponent<Inventory>();
        render = gameObject.GetComponentInChildren<SpriteRenderer>();

        inputKey = false;
        solveProblem = false;

        player = GameObject.FindGameObjectWithTag("Player");
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        if (Input.GetAxisRaw("Horizontal") < 0) {
            render.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) {
            render.flipX = false;
        }
        MoveVelocity = moveInput.normalized * Speed;

        if (Input.GetKeyDown(KeyCode.W)) {
            inputKey = true;
            Debug.Log("KeyDownK");
        }
        else if (Input.GetKeyUp(KeyCode.W)){
            inputKey = false;
        }
    }

    void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + MoveVelocity * Time.fixedDeltaTime);
    }

    // 충돌체 박스. 태그가 많아질 예정이다.
    // 이것도 스테이지에 따라 구분하여 구현할 예정이다.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Portal") {
            Debug.Log(other.name);
            FindObjectOfType<SpriteController>().ChangeSprite(other);
            FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
            
        }
    }

    void OnTriggerStay2D(Collider2D other) { 
        if (other.tag == "Finish" && inputKey == true) {
            Debug.Log("Finish");
            if (Inventory.instance.inventoryContains(2009))
                GameManager.EndGame();
            else {
                //DialogueTrigger_Warning.instance.TriggerDialogue();
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }
        }

        else if (other.tag == "Box1" && inputKey == true) {
            Debug.Log(other.GetComponent<Message>().dialogue.name);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

            Inventory.instance.AddItem(1001);
            Destroy(other);
        }

        else if (other.tag == "FirePlace" && inputKey == true) {
            Debug.Log(other.GetComponent<Message>().dialogue.name);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

            if (Inventory.instance.inventoryContains(2003) && !Inventory.instance.inventoryContains(2007))
                Inventory.instance.AddItem(2007);

            solveProblem = true;
        }

        else if (other.tag == "Window_1" && inputKey == true && solveProblem == true && Inventory.instance.inventoryContains(2007)) {
            Debug.Log(other.GetComponent<Message>().dialogue.name);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

            solveProblem = false;
        }

        else if (other.tag == "Window_2" && inputKey == true && solveProblem == true && Inventory.instance.inventoryContains(2007)) {
            Debug.Log(other.GetComponent<Message>().dialogue.name);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

            if (!Inventory.instance.inventoryContains(2009))
                Inventory.instance.AddItem(2009);
            
        }

        else if (other.tag == "Box2" && inputKey == true) {
            Debug.Log("Box2");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            //DialogueTrigger.instance.TriggerDialogue();

            Inventory.instance.AddItem(1002);
            Destroy(other);
        }
    }
}

    


