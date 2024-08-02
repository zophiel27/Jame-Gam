using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoScript : MonoBehaviour
{
    public GameObject blood_effect;
    [ContextMenu("splatter")]
    public void splatter(int key) {
        // 0 - h - head
        // 1 - rs - right shoulder
        // 2 - ls - left shoulder
        // 3 - lh - left hip
        // 4 - rh - right hip
        string[] game_object_name = { "HeadBloodSplatter", "RightShoulderSplatter", "LeftShoulderSplatter", "LeftHipSplatter", "RightHipSplatter" };
        int[] direction = { 270, 180, 90, 0, 0 };
        GameObject blood_position = transform.Find(game_object_name[key]).gameObject;
        GameObject blood_instance = Instantiate(blood_effect, blood_position.transform.position, Quaternion.Euler(270, 0, 0), blood_position.transform);
        ParticleSystem ps = blood_instance.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(blood_instance, ps.main.duration);
    }   
}
