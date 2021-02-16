using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private ArcherController archer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        archer = GameObject.FindObjectOfType<ArcherController>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isShooting", archer.isShooting);
        animator.SetBool("ReleaseArrow", archer.arrowReleased);
    }
}
