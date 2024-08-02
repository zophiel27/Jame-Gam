using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerRightArmScript : MonoBehaviour
{
    public Sprite bloody_right_arm;
    EnemyScript enemyScript;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<EnemyScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") && enemyScript.last_collided_with_arrow != collision.gameObject)
        {
            enemyScript.last_collided_with_arrow = collision.gameObject;
            ArrowScript arrow_script = collision.gameObject.GetComponent<ArrowScript>();
            if (arrow_script.is_active)
            {
                if (!enemyScript.is_dead)
                {
                    enemyScript.Mark_Dead();
                    enemyScript.PlaySound();
                }
                HingeJoint2D joint = GetComponent<HingeJoint2D>();
                if (joint.enabled)
                {
                    joint.enabled = false;
                    Bleed();
                    enemyScript.splatter(5);
                }
            }
        }
    }
    public void Bleed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = bloody_right_arm;
    }
}
