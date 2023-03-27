using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] Collider2D collider;
    public TextMeshProUGUI textComp;
    [SerializeField] float speed;
    private Vector3 _input;
    public bool isMoving;
    [SerializeField] float jumpForce;
    [SerializeField] Vector3 checkGroundOffset;
    private bool isGrounded;

    public bool canRead = false;
    public LayerMask groundMask;

    //private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer hero_sprite;

    public Console console;
    [SerializeField] Rigidbody2D rb;
    public Animator animator;

    private string name;
    public bool end = false;
    public bool visibleText = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        textComp.gameObject.SetActive(false);
    }


    void FixedUpdate()
    {
        CheckGroud();
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            animator.SetBool("IsFalling", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) && visibleText)
        {
            textComp.gameObject.SetActive(false);
            visibleText = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && canRead)
        {
            if (name == "spawnPlatform" || name == "spawnPlatform(1)")
            {
                textComp.gameObject.SetActive(true);
                textComp.text = "/spawn platform";
            }
            else if (name == "password")
            {
                textComp.gameObject.SetActive(true);
                textComp.text = "password: I love pizza";
            }
            else if (name == "login")
            {
                textComp.gameObject.SetActive(true);
                textComp.text = "login: Sushi";
            }
            else if (name == "commands")
            {
                textComp.gameObject.SetActive(true);
                textComp.text = "/reboot all\n/destroy all\n/stop all";
            }
            else if (name == "spawnLift")
            {
                textComp.gameObject.SetActive(true);
                textComp.text = "/spawn lift";
            }
            visibleText = true;
            if (name == "superComp")
            {
                end = true;
            }
        }
    }

    private void CheckGroud()
    {
        float rayLen = 2f;
        Vector3 rayStartPosition = transform.position + checkGroundOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLen, groundMask);
        if (hit.collider != null && (hit.collider.tag == "Ground" || hit.collider.tag == "Platform"))
        {
            animator.SetBool("IsJumping", false);
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Comp")
        {
            canRead = true;
            name = collision.name;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Comp")
        {
            canRead = false;
        }
    }
}
