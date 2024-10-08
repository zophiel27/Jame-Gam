using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class BombScript : MonoBehaviour
{
    public float aoe = 2;
    public float power = 200;
    //public LayerMask affected_layer; // if we want only specific items to be affected by the bomb change this else khair hai 
    public GameObject explosion_effect;
    CameraShake shake_script;

    void Start()
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
            shake_script = virtualCamera.GetComponent<CameraShake>();
    }
    public void Explode() {
        Collider2D[] objects_hit = Physics2D.OverlapCircleAll(transform.position, aoe);
        foreach (Collider2D obj in objects_hit) {
            Vector2 direction_of_force = obj.transform.position - transform.position; // get the direction in which force should be exerted
            Rigidbody2D obj_rigid_body = obj.GetComponent<Rigidbody2D>();
            if (obj_rigid_body != null)
                obj_rigid_body.AddForce(direction_of_force * power);
            if (obj.isTrigger)
            {
                if (obj.CompareTag("Enemy"))
                {
                    EnemyScript es = obj.GetComponent<EnemyScript>();
                    if (!es.is_dead)
                    {
                        es.MakeJointsWeak();
                        es.PlaySound(0);
                        es.Mark_Dead();
                        es.Make_Bleed("r");
                        es.Make_Bleed("l");
                        es.Decapitate();
                    }
                    es.splatter(0);
                    es.PlaySound(1);
                }
            }
        }
        shake_script.ShakeCamera();
        GameObject explosion_effect_instance = Instantiate(explosion_effect, transform.position, Quaternion.identity);
        Destroy(explosion_effect_instance, 3);
        Destroy(gameObject);
    }
    //ON collision with arrow 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") || collision.gameObject.CompareTag("Boulder"))
            Explode();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoe);
    }
}
