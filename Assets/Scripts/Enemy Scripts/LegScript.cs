using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpperLeftLegScript : MonoBehaviour
{
    EnemyScript enemyScript;
    string[] game_object_name = {"LeftUpperLeg", "RightUpperLeg"};
    public GameObject blood_effect;
    public float pushForce = 10f;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<EnemyScript>();
    }
    [ContextMenu("splatter")]
    public void splatter()
    {
        GameObject blood_position = transform.Find("KneenemyScriptplatter").gameObject;
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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            enemyScript.last_collided_with_arrow = collision.gameObject;
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
                int indx = Array.IndexOf(game_object_name, gameObject.name);
                HingeJoint2D joint = GetComponent<HingeJoint2D>();
                if (joint.enabled)
                {
                    joint.enabled = false;
                    if (indx != -1)
                        enemyScript.splatter(indx + 3);
                    else
                    {
                        UpperLeftLegScript upper_leg_script = joint.connectedBody.gameObject.GetComponent<UpperLeftLegScript>();
                        upper_leg_script.splatter();
                    }

                }
            }
            else
            {
                Debug.Log("Arrow inactive");
            }
        }
        else if (collision.gameObject.CompareTag("Boulder"))
        {
            enemyScript.Crush();
        }
    }
}
