using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] Collider2D collider;

    [SerializeField] float speed;
    private Vector3 _input;
    public bool isMoving;
    [SerializeField] float jumpForce;
    [SerializeField] Vector3 checkGroundOffset;
    private bool isGrounded;
    public LayerMask groundMask;

    //private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer hero_sprite;

    [SerializeField] Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        CheckGroud();
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && Input.anyKey && isGrounded)
        {
            Jump();
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
    }

    private void CheckGroud()
    {
        float rayLen = 1f;
        Vector3 rayStartPosition = transform.position + checkGroundOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLen, groundMask);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            animator.SetBool("IsFalling", false);
            isGrounded = true;
        }
        else
        {
            animator.SetBool("IsFalling", true);
            isGrounded = false;
        }
    }
    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += _input * speed * Time.deltaTime;
        isMoving = _input.x != 0 ? true : false;
        if (isMoving)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        if (_input.x != 0)
        {
            hero_sprite.flipX = _input.x <= 0;
        }
    }
    private void Jump()
    {
        animator.SetBool("IsJumping", true);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("ÏÐÛÃ");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "killObject")
        {
            animator.SetBool("IsDead", true);
        }
        
    }
}
