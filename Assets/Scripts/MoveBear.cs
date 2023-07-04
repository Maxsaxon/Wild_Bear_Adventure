using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBear : MonoBehaviour
{
  // normal move
    private Rigidbody rb;
    public float moveSpeed = 8f;
    public float rotation_speed = 0.15f;
    private float rotateDegreesPerSecond = 180f;
    private PlayerAnimations playerAnim;
    public ParticleSystem ripple;

    private bool isGrounded;

    [SerializeField] private float VelocityXZ, VelocityY;

    private Vector3 PlayerPos;
    

  void Awake()
  {
    // Awake is called when the script instance is being loaded.
    rb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<PlayerAnimations>();
  }

  void Update()
  {
    // Update is called every frame, if the MonoBehaviour is enabled.
     float horizontalInput = Input.GetAxis("Horizontal");
     float verticalInput = Input.GetAxis("Vertical");

     Rotate();
     AnimateWalk();

     Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
     movementDirection.Normalize();

     rb.transform.Translate(movementDirection * moveSpeed * Time.deltaTime); // move bear

     VelocityXZ = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(PlayerPos.x, 0, PlayerPos.z));
     VelocityY = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, PlayerPos.y, 0));
     PlayerPos = transform.position;
  }

  

  void Rotate()
  {
    Vector3 rotation_Direction = Vector3.zero;

    if(Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0) // move forward
    {
        rotation_Direction = transform.TransformDirection(Vector3.left);
    }
    
    if(Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0) // move backward
    {
        rotation_Direction = transform.TransformDirection(Vector3.right);
    }

    if(rotation_Direction != Vector3.zero) // we reset it every time
    {
      
      transform.rotation = Quaternion.RotateTowards( // rotate from transorm.rotation to Quaternion.LookRotation(rotation_Direction) in these degrees
        transform.rotation, Quaternion.LookRotation(rotation_Direction),
        rotateDegreesPerSecond * Time.deltaTime);//
    }
  }

  void AnimateWalk()
  {
    if(rb.velocity.sqrMagnitude != 0f) // sqrMagnitude - retuens the squared length of this vector
    {
      playerAnim.WalkForward(true);
    }
    else
    {
      playerAnim.WalkForward(false);
    }
  }

  void CreateRipple(int Start, int End, int Delta, float Speed, float Size, float Lifetime)
  {
    Vector3 forward = ripple.transform.eulerAngles;
    forward.y = Start;
    ripple.transform.eulerAngles = forward;
    for(int i = Start; i < End; i+=Delta)
    {
      ripple.Emit(transform.position + ripple.transform.forward * 0.5f, ripple.transform.forward * Speed, Size, Lifetime, Color.white);
      ripple.transform.eulerAngles += Vector3.up * 3;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.layer == 4 && VelocityY > 0.03f)
    {
      CreateRipple(-180, 180, 3, 2, 2, 2);
    }
  }

   private void OnTriggerStay(Collider other)
  {
    if(other.gameObject.layer == 4 && VelocityXZ > 0.02f && Time.renderedFrameCount % 5 == 0)
    {
      int y = (int)transform.eulerAngles.y;
      CreateRipple(y-90, y+90, 3, 5, 2, 1);
    }
  }

   private void OnTriggerExit(Collider other)
  {
    if(other.gameObject.layer == 4)
    {
      CreateRipple(-180, 180, 3, 2, 2, 2);
    }
  }
}
