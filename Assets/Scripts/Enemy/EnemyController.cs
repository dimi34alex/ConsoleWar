using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public float chasingRange;
    public float shootingDelay;
    public Transform shootingPoint;
    public AudioClip shootSound;
    public Animator animator;
    public GameObject laserPrefab;
    public float timerBreaking;


    private float timer;
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
            animator.SetBool("IsBreaking", true);
            if (timer <= timerBreaking)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
            animator.SetBool("IsBreaking", false);
            Patrol();
        }
    }
    private void Chase()
    {

    }
    private void Shoot()
    {

    }
    private void Patrol()
    {
        transform.position = Vector3.Lerp(transform.position, currentTarget, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % points.Length;
            currentTarget = points[currentTargetIndex].position;
        }
    }
}