using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    GameObject player;
    GameObject Key;
    bool inputKey;
    bool solveProblem;

    // 스테이지2 퍼즐 체크
    bool npcTalk = false; // npc와 먼저 대화를 했는지
    bool needNew = false; // 음식을 거절당했는지

    // 스테이지3 퍼즐 체크
    bool PianoTrigger;
    bool electricityOn;
    bool PCTrigger;
    bool canDown;
    bool EndTrigger;

    private Inventory inven;
    private Rigidbody2D rb2D;
    public Dialogue dialogue;
    public SpriteRenderer render;
    public Sprite None;

    Vector2 MoveVelocity;
    public float Speed = 5f;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        inven = GetComponent<Inventory>();
        render = gameObject.GetComponentInChildren<SpriteRenderer>();

        inputKey = false;
        solveProblem = false;
        PianoTrigger = false;
        electricityOn = false;
        PCTrigger = false;
        EndTrigger = false;
        None = null;

        if (GameManager.stageLevel == 3)
            canDown = false;

        player = GameObject.FindGameObjectWithTag("Player");
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        // 캐릭터 좌우 반전
        if (Input.GetAxisRaw("Horizontal") < 0) {
            render.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) {
            render.flipX = false;
        }
        MoveVelocity = moveInput.normalized * Speed;

        // W키 입력
        if (Input.GetKeyDown(KeyCode.W)) {
            inputKey = true;
            Debug.Log("KeyDownK");
        }
        else if (Input.GetKeyUp(KeyCode.W)) {
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
            if (GameManager.stageLevel == 1 || GameManager.stageLevel == 2) {
                Debug.Log(other.name);
                FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
                //FindObjectOfType<SoundManager>().PlaySingle(other.GetComponent<AudioClip>());
                other.GetComponent<AudioSource>().Play();
            }
            if (GameManager.stageLevel == 3) {
                if (canDown) {
                    Debug.Log(other.name);
                    FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
                    //FindObjectOfType<SoundManager>().PlaySingle(other.GetComponent<AudioClip>());
                    other.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (GameManager.stageLevel == 1 && inputKey == true) {
            if (other.tag == "Finish") {
                Debug.Log("Finish");
                if (Inventory.instance.inventoryContains(2009)) {
                    GameManager.instance.EndGame();
                }
                else {
                    //DialogueTrigger_Warning.instance.TriggerDialogue();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
            }

            else if (other.tag == "Box1") {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                other.GetComponent<AudioSource>().Play();
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                Inventory.instance.AddItem(1001);
                Destroy(other);
            }

            else if (other.tag == "FirePlace") {
                Debug.Log(other.GetComponent<Message>().dialogue.name);

                if (!Inventory.instance.inventoryContains(2003)) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
                else if (Inventory.instance.inventoryContains(2003) && !Inventory.instance.inventoryContains(2007)) {
                    other.GetComponent<Message>().dialogue.sentences[0] = "열쇠와 화로에 적힌 글을 발견하였다.";
                    other.GetComponent<Message>().dialogue.sentences[1] = "INT_MAX에 없는 숫자는 무엇일까요?";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    other.GetComponent<AudioSource>().Play();
                    Inventory.instance.AddItem(2007);
                    solveProblem = true;
                }
                else if (Inventory.instance.inventoryContains(2003) && Inventory.instance.inventoryContains(2007) && solveProblem == false) {
                    other.GetComponent<Message>().dialogue.sentences[0] = "글을 다시 살펴보았다.";
                    other.GetComponent<Message>().dialogue.sentences[1] = "INT_MAX에 없는 숫자는 무엇일까요?";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    solveProblem = true;
                }
            }

            else if (other.tag == "Window_1" && solveProblem == true && Inventory.instance.inventoryContains(2007)) {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                solveProblem = false;
            }

            else if (other.tag == "Window_2" && solveProblem == true && Inventory.instance.inventoryContains(2007)) {
                Debug.Log(other.GetComponent<Message>().dialogue.name);
                other.GetComponent<AudioSource>().Play();
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                if (!Inventory.instance.inventoryContains(2009))
                    Inventory.instance.AddItem(2009);

            }

            else if (other.tag == "Box2") {
                Debug.Log("Box2");
                other.GetComponent<AudioSource>().Play();
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                Inventory.instance.AddItem(1002);
                Destroy(other);
            }
        }
        else if (GameManager.stageLevel == 2 && inputKey == true) {
            if (other.tag == "Finish") {
                Debug.Log("Finish");
                if (Inventory.instance.inventoryContains(3007)) {
                    //FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
                    GameManager.instance.EndGame();
                }
                else {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
            }

            else if (other.tag == "Cook") {
                if (npcTalk == true && Inventory.instance.inventoryContains(3001) && Inventory.instance.inventoryContains(3002) && Inventory.instance.inventoryContains(3003)) {
                    other.GetComponent<AudioSource>().Play();
                    other.GetComponent<Message>().dialogue.sentences[0] = "요리가 완성되었다!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.RemoveItem(3001);
                    Inventory.instance.RemoveItem(3002);
                    Inventory.instance.RemoveItem(3003);
                    Inventory.instance.AddItem(3005);
                    Destroy(other);
                }
                else {
                    if (npcTalk == true) {
                        Debug.Log(other.GetComponent<Message>().dialogue.name);
                        FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    }
                }
            }

            else if (other.tag == "Material") {
                if (npcTalk == true && Inventory.instance.inventoryContains(3001) == false && Inventory.instance.inventoryContains(3005) == false && Inventory.instance.inventoryContains(3006) == false && Inventory.instance.inventoryContains(3007) == false) {
                    Debug.Log("Get Item");
                    other.GetComponent<AudioSource>().Play();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3001);
                    Destroy(other);
                }
                else if (npcTalk == true && Inventory.instance.inventoryContains(3001) && (Inventory.instance.inventoryContains(3002) == false)) {
                    Debug.Log("Get Item");
                    other.GetComponent<AudioSource>().Play();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3002);
                    Destroy(other);
                }
                else if (npcTalk == true && Inventory.instance.inventoryContains(3001) && Inventory.instance.inventoryContains(3002) && Inventory.instance.inventoryContains(3003) == false) {
                    Debug.Log("Get Item");
                    other.GetComponent<AudioSource>().Play();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3003);
                    Destroy(other);
                }
            }

            else if (other.tag == "NPC") {
                Debug.Log(other.GetComponent<Message>().dialogue.name);

                if (npcTalk == false) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    npcTalk = true;
                }
                if (npcTalk == true && Inventory.instance.inventoryContains(3005) == false && Inventory.instance.inventoryContains(6009) == false) {
                    other.GetComponent<Message>().dialogue.sentences[0] = "어서 먹을 것을 가져오라고!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
                if (npcTalk == true && Inventory.instance.inventoryContains(3005)) {
                    other.GetComponent<Message>().dialogue.sentences[0] = "이딴 음식을 먹으라고 가져오다니!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    needNew = true;
                }
                else if (npcTalk == true && needNew == true && Inventory.instance.inventoryContains(6009)) {
                    other.GetComponent<AudioSource>().Play();
                    other.GetComponent<Message>().dialogue.sentences[0] = "커억!";
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                    Inventory.instance.RemoveItem(6009);
                    Inventory.instance.AddItem(3007);
                    Destroy(other);
                }
            }

            else if (other.tag == "ShelfPoison") {
                Debug.Log("Get Item");

                if (npcTalk == true && needNew == true && Inventory.instance.inventoryContains(3004) == false && Inventory.instance.inventoryContains(3005) == true) {
                    other.GetComponent<AudioSource>().Play();
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    Inventory.instance.AddItem(3004);
                    Destroy(other);
                }
            }
        }

        else if (GameManager.stageLevel == 3 && inputKey == true) {

            if (other.tag == "Bookshelf") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "Picture") {
                canDown = true;
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "Window") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "TV") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "Piano") {
                if (PianoTrigger == false) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    PCTrigger = true;
                }
                else if (PianoTrigger == true) {
                    FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
                }
            }

            else if (other.tag == "PC") {
                if (!electricityOn) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
                else if (electricityOn) {
                    if (Inventory.instance.inventoryContains(4003)) {
                        other.GetComponent<Message>().dialogue.sentences[0] = "컴퓨터가 켜진것 같다....";
                        other.GetComponent<Message>().dialogue.sentences[1] = "이것저것 찾아보자....";
                        other.GetComponent<Message>().dialogue.sentences[2] = "왜 이런 사진이...??";
                        other.GetComponent<Message>().dialogue.sentences[3] = "..............";
                        other.GetComponent<Message>().dialogue.sentences[4] = "나와 철창에 갖혀있는 사람들이 찍혀있다.....";
                        other.GetComponent<Message>().dialogue.sentences[5] = "문을 열 수 있는 보안프로그램을 해제하였다...";
                        FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                        PianoTrigger = true;
                    }
                }
            }

            else if (other.tag == "Clock") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "USB_Vase") {
                if (Inventory.instance.inventoryContains(4013)) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                    if (!Inventory.instance.inventoryContains(4003))
                        Inventory.instance.AddItem(4003);
                    other.GetComponent<SpriteRenderer>().sprite = None;
                }
            }

            else if (other.tag == "Shelf") {
                if (!Inventory.instance.inventoryContains(4001))
                    Inventory.instance.AddItem(4001);
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "Bookshelf2") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());

                if (!Inventory.instance.inventoryContains(4013))
                    Inventory.instance.AddItem(4013);
            }

            else if (other.tag == "Electricity" && PCTrigger == true) {
                electricityOn = true;
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "Jail") {
                FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
            }

            else if (other.tag == "JailDoor") {
                FindObjectOfType<PortalController>().Teleport(player, other.GetComponent<PortalPosition>());
                EndTrigger = true;
            }

            else if (other.tag == "Chain") {
                if (EndTrigger == false) {
                    FindObjectOfType<DialogueTrigger>().TriggerDialogue(other.GetComponent<Message>());
                }
                else {
                    if (!Inventory.instance.inventoryContains(5007))
                        Inventory.instance.AddItem(5007);
                }
            }
        }
    }
}



