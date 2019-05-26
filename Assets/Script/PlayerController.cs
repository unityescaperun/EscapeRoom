using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    GameObject player;
    GameObject Key;

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
    }

    void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + MoveVelocity * Time.fixedDeltaTime);
    }

    // 충돌체 박스. 태그가 많아질 예정이다.
    // 이것도 스테이지에 따라 구분하여 구현할 예정이다.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PortalUp") {
            Debug.Log("UP");
            FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
        }

        else if (other.tag == "PortalDown") {
            Debug.Log("DOWN");
            //player.transform.position = new Vector3(1, 0, 0);
            FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
        }

        else if (other.tag == "Finish") {
            Debug.Log("Finish");
            if (Inventory.instance.inventoryContains(2003))
                GameManager.EndGame();
            else
            {
                //DialogueTrigger_Warning.instance.TriggerDialogue();
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }
        }
       
        else if (other.tag == "Portal") {
            Debug.Log(other.name);
            FindObjectOfType<SpriteController>().ChangeSprite(other);
            FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
            
        }

        else if (other.tag == "Box1") {
            Debug.Log(other.GetComponent<Message>().dialogue.name);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

            Inventory.instance.AddItem(1001);
            Destroy(other);
        }

        else if (other.tag == "Box2") {
            Debug.Log("Box2");
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            //DialogueTrigger.instance.TriggerDialogue();

            Inventory.instance.AddItem(1002);
            Destroy(other);
        }
    }
}
