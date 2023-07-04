using System.Collections;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce = 8f;

    private bool isGrounded;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        // GET INPUTS
        float inputHorizontal = Input.GetAxis(Axis.HORIZONTAL_AXIS);
        float inputVertical = Input.GetAxis(Axis.VERTICAL_AXIS);

        Vector3 movementDirection = new Vector3(inputHorizontal, 0, inputVertical);// move player
        
        float magnitude = movementDirection.magnitude; // magnitude of movement
        magnitude = Mathf.Clamp(magnitude, 0, 1); // clamping magnitude
        movementDirection.Normalize(); // stopping movement when unpressed keys

        rb.transform.Translate(movementDirection * magnitude * speed * Time.deltaTime, Space.World); // moving gameobject

        if (movementDirection != Vector3.zero) // rotate player
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
