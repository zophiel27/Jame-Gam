using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLeftArmScript : MonoBehaviour
{
    public Sprite bloody_left_arm;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            HingeJoint2D joint = GetComponent<HingeJoint2D>();
            joint.enabled = false;
            Bleed();
        }
    }
    public void Bleed() { 
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = bloody_left_arm;
    }
}
