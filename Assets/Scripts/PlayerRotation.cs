using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerRotation : MonoBehaviour {

    public float moveSpeed = 5f;
    public float jumpForce = 500f;

    private Rigidbody rb;
    private bool grounded = true;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
       float h = Input.GetAxis("Horizontal");
       float v = Input.GetAxis("Vertical");

       if (Input.GetKey(KeyCode.A)) h = -1f;
       if (Input.GetKey(KeyCode.D)) h = 1f;
       if (Input.GetKey(KeyCode.S)) v = -1f;
       if (Input.GetKey(KeyCode.W)) v = 1f;

    Vector3 moveDirection = new Vector3(h, 0, v);

    if (moveDirection != Vector3.zero) {
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    rb.velocity = moveDirection.normalized * moveSpeed + new Vector3(0, rb.velocity.y, 0);

    if (Input.GetKeyDown(KeyCode.Space) && grounded) {
        rb.AddForce(Vector3.up * jumpForce);
        grounded = false;
    }

    if (!grounded && rb.velocity.y < 0.1f) {
        grounded = true;
     }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
    }
}
