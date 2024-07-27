using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoScript : MonoBehaviour
{
    public GameObject blood_effect;
    [ContextMenu("splatter")]
    public void splatter() {
        GameObject blood_position = transform.Find("HeadBloodSplatter").gameObject;
        GameObject blood_instance = Instantiate(blood_effect,blood_position.transform.position, Quaternion.identity);
        ParticleSystem ps = blood_instance.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(blood_instance, ps.main.duration);
    }   
}
