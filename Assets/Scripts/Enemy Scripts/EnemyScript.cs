using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Sprite bloody_torso;
    public Sprite xx_head_sprite;
    public bool is_dead = false;
    public GameObject last_collided_with_arrow = null;
    private AudioSource audio_source;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void Decapitate() 
    {
        GameObject torso = transform.Find("Torso").gameObject;
        SpriteRenderer torso_sprite = torso.GetComponent<SpriteRenderer>();
        torso_sprite.sprite = bloody_torso;
    }

    public void splatter(int key)
    {
        GameObject torso = transform.Find("Torso").gameObject;
        TorsoScript torso_script = torso.GetComponent<TorsoScript>();
        torso_script.splatter(key);
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
        FindObjectOfType<GameScript>().EnemyDied();
        GameObject head = transform.Find("Head").gameObject;
        SpriteRenderer head_sprite = head.GetComponent<SpriteRenderer>();
        head_sprite.sprite = xx_head_sprite;
        is_dead = true;
    }

    public void PlaySound() 
    {
        audio_source.Play();
    }
}
