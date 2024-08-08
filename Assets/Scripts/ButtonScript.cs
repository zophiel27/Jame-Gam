using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Sprite button_not_activated_sprite;
    public Sprite button_activated_sprite;
    bool is_activated = false;
    SpriteRenderer button_sprite;
    float offset = 0.04f;
    public char axis = 'x';
    public GameObject[] mechanisms;
    float deactivation_time = 11f;
    int multiplier = 1;

    private void Start()
    {
        button_sprite = GetComponent<SpriteRenderer>();
        button_sprite.sprite = button_not_activated_sprite;
        if ((button_sprite.flipX && !button_sprite.flipY) || (button_sprite.flipX && !button_sprite.flipY))
            multiplier = -1;
    }
    public void Update()
    {
        if (is_activated) { 
            deactivation_time -= Time.deltaTime;
            if (deactivation_time <= 0)
            {
                ChangeSpirte();
                deactivation_time = 11f;
            }
        }
    }
    void ChangeSpirte() {
        Vector3 vec = new Vector3(button_sprite.transform.localPosition.x, button_sprite.transform.localPosition.y, button_sprite.transform.localPosition.z);
        if (is_activated)
        {
            is_activated = false;
            button_sprite.sprite = button_not_activated_sprite;
            button_sprite.transform.localScale = new Vector3(-button_sprite.transform.localScale.x, button_sprite.transform.localScale.y, button_sprite.transform.localScale.z);
            if (axis == 'x')
                vec.x -= offset * multiplier;
            else
                vec.y -= offset * multiplier;
            button_sprite.transform.localPosition = vec;
        }
        else
        {
            if (axis == 'x')
                vec.x += offset * multiplier;
            else
                vec.y += offset * multiplier;
            is_activated = true;
            button_sprite.sprite = button_activated_sprite;
            button_sprite.transform.localScale = new Vector3(-button_sprite.transform.localScale.x, button_sprite.transform.localScale.y, button_sprite.transform.localScale.z);
            button_sprite.transform.localPosition = vec;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_activated)
        {
            ChangeSpirte();
            foreach (GameObject mechanism in mechanisms)
            {
                MonoBehaviour[] scripts = mechanism.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    System.Type scriptType = script.GetType();
                    Debug.Log("Script type is" + scriptType);
                    MethodInfo activateMethod = scriptType.GetMethod("Activate", BindingFlags.Instance | BindingFlags.Public);
                    activateMethod.Invoke(script, null);
                }
            } 
        }
    }
}
