using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float aoe = 2;
    public float power = 100;
    //public LayerMask affected_layer; // if we want only specific items to be affected by the bomb change this else khair hai 
    public GameObject explosion_effect;
    

    [ContextMenu("Explode Bomb")] // will be invoked later by another event but for now run manually in editor
    public void Explode(){
        Collider2D[] objects_hit = Physics2D.OverlapCircleAll(transform.position, aoe);

        foreach (Collider2D obj in objects_hit) {
            Vector2 direction_of_force = obj.transform.position - transform.position; // get the direction in which force should be exerted
            Rigidbody2D obj_rigid_body = obj.GetComponent<Rigidbody2D>();
            if (obj_rigid_body != null)
                obj_rigid_body.AddForce(direction_of_force * power);
        }
        GameObject explosion_effect_instance = Instantiate(explosion_effect, transform.position, Quaternion.identity);
        Destroy(explosion_effect_instance,7);
        Destroy(gameObject);
    }
    //ON COLIDE WITH ARROW
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoe);
    }
}
