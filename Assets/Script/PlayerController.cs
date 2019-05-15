using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;

    private Rigidbody2D rb2D;
    bool coll;

    Vector2 MoveVelocity;
    public float Speed = 5f;
    void Start() {
        coll = false;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        MoveVelocity = moveInput.normalized * Speed;
    }

    void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + MoveVelocity * Time.fixedDeltaTime);
    }
}
