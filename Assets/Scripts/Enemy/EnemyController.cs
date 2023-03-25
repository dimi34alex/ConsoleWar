using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject[] points;
    [SerializeField] GameObject vision;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Patrol()
    {
        //Random = new Random(10);
    }
}
