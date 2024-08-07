using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveableDoorScript : MonoBehaviour
{
    float move_offset = 0;
    bool is_active = false;
    GameObject door;
    private void Start()
    {
        door = transform.Find("Door").gameObject;
    }
    void Update()
    {
        if (is_active) {
            float movement = move_offset * Time.deltaTime;
            door.transform.localPosition = new Vector3(door.transform.localPosition.x, door.transform.localPosition.y + movement, door.transform.localPosition.z);
            if (door.transform.localPosition.y >= 1 || door.transform.localPosition.y <= -1) {
                is_active = false;
            } 
        }
    }
    [ContextMenu("Activate Door")]
    public void Activate() {
        if (!is_active)
        {
            is_active = true;
            move_offset = door.transform.localPosition.y >= 1 ? -0.4f : 0.4f;
        } 
    }
}
