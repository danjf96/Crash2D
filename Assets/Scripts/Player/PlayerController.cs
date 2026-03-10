using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool jump = false;
    private Animator anim;
    private bool onTheFloor = false;
    private Transform groundCheck;

    private PlayerInputActions inputController;
    private Vector2 moveInput;

    private GameObject ugaBugaInstance;

    void Awake()
    {
        inputController = new PlayerInputActions();
        
        inputController.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveInput = moveInput.normalized;

        inputController.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputController.Player.Jump.performed += ctx => OnJump();
    }
    
    public void OnJump()
    {
        PlayerAttack attackScript = GetComponent<PlayerAttack>();
        if(attackScript.IsAttacking()) return;
        
        anim.SetTrigger("Pulou");
        jump = true;
    }

    void OnEnable() => inputController.Enable();
    void OnDisable() => inputController.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        onTheFloor = Physics2D.Linecast(
            transform.position,
            groundCheck.position,
            1 << LayerMask.NameToLayer("Ground")
        );
    }

    void FixedUpdate()
    {
        anim.SetFloat("Velocidade", Mathf.Abs(moveInput.x));

        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();

        if (jump && onTheFloor)
        {  
            rb.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool IsFloating()
    {
        return !onTheFloor;
    }

    public void OnUgaBugaCollected(GameObject ugaBuga, Vector3 position)
    {
        if (ugaBugaInstance == null)
        {   
            GameObject ugaBugaObj = Instantiate(ugaBuga, position, Quaternion.identity);
            ugaBugaInstance = ugaBuga;
        }

        if(ugaBugaInstance != null)
        {
            
            if (ugaBugaInstance.TryGetComponent<UgaBuga>(out var script))
            {
                script.UpdatedCollectedCount();
            }
        }

    }

    public GameObject GetUgaBugaInstance()
    {
        return ugaBugaInstance;
    }

    public void Bounce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, jumpForce));
    }
}