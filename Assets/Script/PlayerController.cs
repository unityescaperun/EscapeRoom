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

    Vector2 MoveVelocity;
    public float Speed = 5f;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        inven = GetComponent<Inventory>();

        player = GameObject.FindGameObjectWithTag("Player");
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
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
            player.transform.position = new Vector3(18, 4, 0);
        }

        if (other.tag == "PortalDown") {
            Debug.Log("DOWN");
            player.transform.position = new Vector3(1, 0, 0);
        }

        if (other.tag == "Item1") {
            Debug.Log("Get Item1");
            other.gameObject.SetActive(false);
            Inventory.instance.AddItem(1001);
        }

        if (other.tag == "Item2") {
            Debug.Log("Get Item2");
            other.gameObject.SetActive(false);
            Inventory.instance.AddItem(1002);
        }

        if (other.tag == "Finish") {
            Debug.Log("Finish");
            if (Inventory.instance.inventoryContains(2003))
                GameManager.EndGame();
        }
    }
}
