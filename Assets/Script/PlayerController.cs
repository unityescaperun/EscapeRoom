using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    GameObject player;
    GameObject Key;

    List<Inventroy> itemList = new List<Inventroy>();

    private Rigidbody2D rb2D;

    Vector2 MoveVelocity;
    public float Speed = 5f;
    bool OpenDoor;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        OpenDoor = false;

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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PortalUp") {
            player.transform.position = new Vector3(18, 4, 0);
        }

        if (other.tag == "PortalDown") {
            player.transform.position = new Vector3(1, 0, 0);
        }

        if(other.tag == "Key") {
            other.gameObject.SetActive(false);
            OpenDoor = true;
        }

        if(other.tag == "Finish") {
            if (OpenDoor == true)
                GameManager.EndGame();
        }
    }
}
