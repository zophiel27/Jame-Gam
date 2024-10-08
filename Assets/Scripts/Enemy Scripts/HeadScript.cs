using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
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
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr)
            {
                ArrowScript arrow_script = collision.gameObject.GetComponent<ArrowScript>();
                if (arrow_script.is_active)
                {
                    if (!enemyScript.is_dead)
                    {
                        enemyScript.Mark_Dead();
                        enemyScript.PlaySound(0);
                        enemyScript.MakeJointsWeak();
                    }
                    HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
                    if (joint != null && joint.enabled)
                    {
                        joint.enabled = false;
                        EnemyScript parent_script = gameObject.GetComponentInParent<EnemyScript>();
                        parent_script.Decapitate();
                        parent_script.splatter(0);
                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag("Boulder"))
        {
            enemyScript.Crush();
        }
    }
}
