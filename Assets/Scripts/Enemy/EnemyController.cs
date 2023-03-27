using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    public float chasingRange = 10f;
    public float chasingTime = 2f;
    public float shootingDelay = 2f;
    public Transform shootingPoint;
    public AudioClip shootSound;
    public Animator animator;
    public GameObject laserPrefab;
    public float timerBreaking = 2f;
    public GameObject player;
    public LayerMask mask;
    public Rigidbody2D vision;

    private float timer;
    private GameObject laser;
    private bool isPatroling = true;
    private bool breaking = false;
    private Vector3 currentTarget;
    private int currentTargetIndex = 0; // индекс текущей цели
    private void Start()
    {
        currentTarget = points[currentTargetIndex].position; // начинаем с первой точки патрулирования
    }
    private void FixedUpdate()
    {
        if (breaking)
        {
            //animator.SetBool("IsBreaking", true);
            if (timer <= timerBreaking)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
            //animator.SetBool("IsBreaking", false);
            if (isPatroling)
                Patrol();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPatroling = false;
        transform.position = transform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Ты вошел в меня");
        if (collision.tag == "Player")
        {
            Debug.Log("Проверяю игрок ли это");
            
            if (timer <= chasingTime)
                timer += Time.deltaTime;
            else
                Shoot();
        }
        else
            Debug.Log("Не нашел(");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Потерял");
        isPatroling = true;
        timer = 0;
    }

    private void Shoot()
    {
        timer = 0;
        Debug.Log("Стреляю");
        laser = Instantiate(laserPrefab, shootingPoint);
        Rigidbody2D rbLaser = laser.GetComponent<Rigidbody2D>();
        rbLaser.AddForce(new Vector2(10, 0));
    }
    private void OnBecameInvisible()
    {
        Destroy(laser);
    }
    private void Patrol()
    { 
        //Debug.Log("Вошел в патруль");
        transform.position = Vector3.Lerp(transform.position, currentTarget, speed * Time.deltaTime);
        //Debug.Log("Перемещаю");
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            if (currentTargetIndex + 1 == points.Length)
            {
                currentTargetIndex = 0;
            }
            else
                currentTargetIndex += 1;
            currentTarget = points[currentTargetIndex].position;
        }
    }
}