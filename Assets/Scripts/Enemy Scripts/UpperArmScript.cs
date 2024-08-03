using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperRightArmScript : MonoBehaviour
{
    EnemyScript enemyScript;
    public GameObject blood_effect;
    public float pushForce = 10f;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<EnemyScript>();
    }
    [ContextMenu("splatter")]
    public void splatter()
    {
        GameObject blood_position = transform.Find("ElbowSplatter").gameObject;
        GameObject blood_instance = Instantiate(blood_effect, blood_position.transform.position, Quaternion.Euler(180, 0, 0), blood_position.transform);
        ParticleSystem ps = blood_instance.GetComponent<ParticleSystem>();
        enemyScript.PlaySound(1);
        ps.Play();
        Destroy(blood_instance, ps.main.duration);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") && enemyScript.last_collided_with_arrow != collision.gameObject)
        {
            enemyScript.last_collided_with_arrow = collision.gameObject;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            ArrowScript arrow_script = collision.gameObject.GetComponent<ArrowScript>();
            if (arrow_script.is_active)
            {
                if (!enemyScript.is_dead)
                {
                    enemyScript.Mark_Dead();
                    enemyScript.PlaySound(0);
                    enemyScript.MakeJointsWeak();
                }
                Vector2 pushDirection = collision.contacts[0].point - (Vector2)transform.position;
                pushDirection = -pushDirection.normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                enemyScript.splatter(1);
                HingeJoint2D joint = GetComponent<HingeJoint2D>();
                joint.enabled = false;
                EnemyScript parent_script = gameObject.GetComponentInParent<EnemyScript>();
                if (gameObject.name == "RightUpperArm")
                    parent_script.Make_Bleed("r");
                else
                    parent_script.Make_Bleed("l");
            }
        }
    }
}
