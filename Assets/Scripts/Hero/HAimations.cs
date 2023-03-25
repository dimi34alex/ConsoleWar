using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAimations : MonoBehaviour
{
    private Animator animator;

    public bool IsMoving { private get; set; }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("IsMoving", IsMoving);
    }
}
