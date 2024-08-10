using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("gameobject name(Fire): " + collision.gameObject.name);
        if (collision.gameObject.name == "IceWall")
        {
            IceWallScript wallScript = collision.gameObject.GetComponent<IceWallScript>();
            wallScript.PlayAnimation();
        }
    }
}
