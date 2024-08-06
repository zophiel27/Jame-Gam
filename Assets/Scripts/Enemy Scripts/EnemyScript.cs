using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;

public class EnemyScript : MonoBehaviour
{
    public Sprite bloody_torso;
    public Sprite xx_head_sprite;
    public bool is_dead = false;
    public GameObject last_collided_with_arrow = null;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    public AudioClip[] audio_clips;
    bool crushed = false;

    private void Start()
    {
        AudioSource[] audio_source_list = GetComponents<AudioSource>();
        if (audio_source_list.Length >= 2)
        {
            audioSource1 = audio_source_list[0];
            audioSource2 = audio_source_list[1];
            audioSource1.clip = audio_clips[0];
            audioSource2.clip = audio_clips[1];

        }
    }
    public void Decapitate()
    {
        GameObject torso = transform.Find("Torso").gameObject;
        SpriteRenderer torso_sprite = torso.GetComponent<SpriteRenderer>();
        torso_sprite.sprite = bloody_torso;
    }

    public void splatter(int key)
    {
        string[] game_object_name = { "RightUpperArm", "LeftUpperArm" };
        if (key < 5)
        {
            GameObject torso = transform.Find("Torso").gameObject;
            TorsoScript torso_script = torso.GetComponent<TorsoScript>();
            torso_script.splatter(key);
        }
        else
        {
            GameObject upper_arm = transform.Find(game_object_name[key - 5]).gameObject;
            UpperRightArmScript upper_arm_script = upper_arm.GetComponent<UpperRightArmScript>();
            upper_arm_script.splatter();
        }
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

    public void Mark_Dead()
    {
        FindObjectOfType<GameScript>().EnemyDied();
        GameObject head = transform.Find("Head").gameObject;
        SpriteRenderer head_sprite = head.GetComponent<SpriteRenderer>();
        head_sprite.sprite = xx_head_sprite;
        is_dead = true;
    }

    public void PlaySound(int indx)
    {
        if (indx == 1)
            audioSource2.Play();
        else
            audioSource1.Play();
    }

    public void MakeJointsWeak()
    {
        string[] limbs = { "LeftUpperLeg", "RightUpperLeg", "LeftLowerLeg", "LeftLowerLeg" };
        foreach (string limb in limbs)
        {
            GameObject obj = transform.Find(limb).gameObject;
            HingeJoint2D joint = obj.GetComponent<HingeJoint2D>();
            JointAngleLimits2D limits = joint.limits;
            limits.min = 0;
            limits.max = 359;
            joint.limits = limits;
            joint.useLimits = true;

        }

    }

    public void Crush() {
        if (!crushed)
        {
            crushed = true;
            MakeJointsWeak();
            PlaySound(0);
            Mark_Dead();
            Make_Bleed("r");
            Make_Bleed("l");
            Decapitate();
            splatter(0);
            PlaySound(1);
        }
    }
}
