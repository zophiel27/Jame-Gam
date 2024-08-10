using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallScript : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }
    public void PlayAnimation()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        // Trigger the animation
        animator.Play("IceWallMelt");
    }
}
