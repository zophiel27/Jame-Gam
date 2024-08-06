using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    HingeJoint hinge;
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            EnemyScript enemyScript = collider.gameObject.GetComponent<EnemyScript>();
            if (enemyScript != null && !enemyScript.is_dead)
            {
                enemyScript.Mark_Dead();
                enemyScript.PlaySound(0);
                enemyScript.MakeJointsWeak();
                Destroy(hinge);
            }
        }
    }
}
