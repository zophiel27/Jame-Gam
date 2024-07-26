using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public Sprite xx_head_sprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr)
            {
                sr.sprite = xx_head_sprite;
                HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
                joint.enabled = false;
                EnemyScript parent_script = gameObject.GetComponentInParent<EnemyScript>();
                parent_script.Decapitate();
            }
        }
    }
}
