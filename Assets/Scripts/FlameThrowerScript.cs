using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerScript : MonoBehaviour
{
    public GameObject Fiyyah;
    public bool is_active = false;
    public void Activate()
    {
        if (!is_active) {
            is_active = true;
            GameObject fiyah = Instantiate(Fiyyah, new Vector3(transform.localPosition.x + 2.3f, transform.localPosition.y - 1.7f, transform.localPosition.z), Quaternion.Euler(0, 0, -90));
            Destroy(fiyah, 2f);
            StartCoroutine(SetIsActiveFalseAfterDelay(2f));
        }
    }
    private IEnumerator SetIsActiveFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        is_active = false;
    }
}
