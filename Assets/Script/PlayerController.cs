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
            FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
            //FindObjectOfType<SoundManager>().PlaySingle(other.GetComponent<AudioClip>());
            
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (GameManager.stageLevel == 1)
        {
            if (other.tag == "Finish" && inputKey == true)
            {
                Debug.Log("Finish");
                if (Inventory.instance.inventoryContains(2009))
                    GameManager.EndGame();
                else
                {
                    //DialogueTrigger_Warning.instance.TriggerDialogue();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
            }

            else if (other.tag == "Box1" && inputKey == true)
            {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                Inventory.instance.AddItem(1001);
                Destroy(other);
            }

            else if (other.tag == "FirePlace" && inputKey == true)
            {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                if (Inventory.instance.inventoryContains(2003) && !Inventory.instance.inventoryContains(2007))
                    Inventory.instance.AddItem(2007);

                solveProblem = true;
            }

            else if (other.tag == "Window_1" && inputKey == true && solveProblem == true && Inventory.instance.inventoryContains(2007))
            {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                solveProblem = false;
            }

            else if (other.tag == "Window_2" && inputKey == true && solveProblem == true && Inventory.instance.inventoryContains(2007))
            {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                if (!Inventory.instance.inventoryContains(2009))
                    Inventory.instance.AddItem(2009);

            }

            else if (other.tag == "Box2" && inputKey == true)
            {
                Debug.Log("Box2");
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                //DialogueTrigger.instance.TriggerDialogue();

                Inventory.instance.AddItem(1002);
                Destroy(other);
            }
        }
        else if(GameManager.stageLevel == 2)
        {
            if (other.tag == "Finish" && inputKey == true)
            {
                Debug.Log("Finish");
                if (Inventory.instance.inventoryContains(3007))
                    GameManager.EndGame();
                else
                {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
            }

            else if (other.tag == "Cook" && inputKey == true)
            {
                if (Inventory.instance.inventoryContains(3001) && Inventory.instance.inventoryContains(3002) && Inventory.instance.inventoryContains(3003))
                {
                    other.GetComponent<Message>().dialogue.sentences[0] = "요리가 완성되었다!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.RemoveItem(3001);
                    Inventory.instance.RemoveItem(3002);
                    Inventory.instance.RemoveItem(3003);
                    Inventory.instance.AddItem(3005);
                    Destroy(other);
                }
                else
                {
                    Debug.Log(other.GetComponent<Message>().dialogue.name);
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
            }

            else if (other.tag == "Material" && inputKey == true)
            {
                if (Inventory.instance.inventoryContains(3001) == false && Inventory.instance.inventoryContains(3005) == false && Inventory.instance.inventoryContains(3006) == false && Inventory.instance.inventoryContains(3007) == false)
                {
                    Debug.Log("Get Item");
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3001);
                    Destroy(other);
                }
                else if(Inventory.instance.inventoryContains(3001) && (Inventory.instance.inventoryContains(3002) == false))
                {
                    Debug.Log("Get Item");
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3002);
                    Destroy(other);
                }
                else if(Inventory.instance.inventoryContains(3001) && Inventory.instance.inventoryContains(3002) && Inventory.instance.inventoryContains(3003) == false)
                {
                    Debug.Log("Get Item");
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3003);
                    Destroy(other);
                }
            }

            else if (other.tag == "NPC" && inputKey == true)
            {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                if (Inventory.instance.inventoryContains(3005))
                    other.GetComponent<Message>().dialogue.sentences[0] = "이딴 음식을 먹으라고 가져오다니!";
                else if (Inventory.instance.inventoryContains(6009))
                {
                    other.GetComponent<Message>().dialogue.sentences[0] = "커억!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                    Inventory.instance.RemoveItem(6009);
                    Inventory.instance.AddItem(3007);
                    Destroy(other);
                }
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                if (Inventory.instance.inventoryContains(3005) == false && Inventory.instance.inventoryContains(3006) == false)
                {
                    other.GetComponent<Message>().dialogue.sentences[0] = "어서 먹을 것을 가져오라고!";
                }
            }

            else if(other.tag == "ShelfPoison" && inputKey == true)
            {
                Debug.Log("Get Item");

                if(Inventory.instance.inventoryContains(3004) == false && Inventory.instance.inventoryContains(3005) == true)
                {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3004);
                    Destroy(other);
                }
            }
        }
    }
}

    


