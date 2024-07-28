using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class EnemyScript : MonoBehaviour
{
    public Sprite bloody_torso;
    public Sprite xx_head_sprite;
    public bool is_dead = false;

    public void Decapitate() {
        GameObject torso = transform.Find("Torso").gameObject;
        SpriteRenderer torso_sprite = torso.GetComponent<SpriteRenderer>();
        TorsoScript s = torso.GetComponent<TorsoScript>();
        torso_sprite.sprite = bloody_torso;
        s.splatter();
    }

    public void Make_Bleed(string side)
    {
        if (side == "l")
        {
            GameObject lower_arm = transform.Find("LeftLowerArm").gameObject;
            LowerLeftArmScript s = lower_arm.GetComponent<LowerLeftArmScript>();
            s.Bleed();
        }
        else if (side == "r")
        {
            GameObject lower_arm = transform.Find("RightLowerArm").gameObject;
            LowerRightArmScript s = lower_arm.GetComponent<LowerRightArmScript>();
            s.Bleed();
        }
    }

    public void Mark_Dead() {
        GameObject head = transform.Find("Head").gameObject;
        SpriteRenderer head_sprite = head.GetComponent<SpriteRenderer>();
        head_sprite.sprite = xx_head_sprite;
        is_dead = true;
    }
}
