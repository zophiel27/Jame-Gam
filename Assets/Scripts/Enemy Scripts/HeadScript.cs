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
        if (collision.gameObject.CompareTag("Arrow"))
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr)
            {
                ArrowScript arrow_script = collision.gameObject.GetComponent<ArrowScript>();
                if (arrow_script.is_active)
                {
                    if (!enemyScript.is_dead)
                        enemyScript.Mark_Dead();
                    HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
                    if (joint != null && joint.enabled)
                    {
                        joint.enabled = false;
                        EnemyScript parent_script = gameObject.GetComponentInParent<EnemyScript>();
                        parent_script.Decapitate();
                    }
                }
            }
        }
    }
}
