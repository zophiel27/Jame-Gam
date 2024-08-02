using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoScript : MonoBehaviour
{

    EnemyScript enemyScript;
    private void Start()
    {
        enemyScript = transform.parent.GetComponent<EnemyScript>();
    }

    public GameObject blood_effect;
    [ContextMenu("splatter")]
    public void splatter(int key) {
        // 0 - h - head
        // 1 - rs - right shoulder
        // 2 - ls - left shoulder
        // 3 - lh - left hip
        // 4 - rh - right hip
        string[] game_object_name = { "HeadBloodSplatter", "RightShoulderSplatter", "LeftShoulderSplatter", "LeftHipSplatter", "RightHipSplatter" };
        int[] direction = { 270, 180, 180, 90, 90 };
        GameObject blood_position = transform.Find(game_object_name[key]).gameObject;
        GameObject blood_instance = Instantiate(blood_effect, blood_position.transform.position, Quaternion.Euler(direction[key], 0, 0), blood_position.transform);
        ParticleSystem ps = blood_instance.GetComponent<ParticleSystem>();
        enemyScript.PlaySound(1);
        ps.Play();
        Destroy(blood_instance, ps.main.duration);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") && enemyScript.last_collided_with_arrow != collision.gameObject) {
            enemyScript.last_collided_with_arrow = collision.gameObject;
            ArrowScript arrow_script = collision.gameObject.GetComponent<ArrowScript>();
            if (arrow_script.is_active)
            {
                if (!enemyScript.is_dead)
                {
                    enemyScript.Mark_Dead();
                    enemyScript.PlaySound(0);
                }
                if (collision.contacts.Length > 0)
                {
                    ContactPoint2D contact = collision.contacts[0];
                    Vector2 collisionPosition = contact.point;
                    Vector3 spawnPosition = new Vector3(collisionPosition.x, collisionPosition.y, 0f);
                    GameObject blood_instance = Instantiate(blood_effect, spawnPosition, Quaternion.Euler(180, 0, 0));
                    ParticleSystem ps = blood_instance.GetComponent<ParticleSystem>();
                    enemyScript.PlaySound(1);
                    ps.Play();
                    Destroy(blood_instance, ps.main.duration);
                }
            }
        }
    }
}
