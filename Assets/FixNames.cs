using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixNames : MonoBehaviour
{
    [ContextMenu("Fix Names")]
    void FixTriggerNames()
    {
        int iterator = 1;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != gameObject)
            {
                t.gameObject.name = iterator.ToString();
                iterator++;
            }
        }
    }

    public void ReverseOrder()
    {
        int iterator = int.Parse(GetComponentsInChildren<Transform>()[GetComponentsInChildren<Transform>().Length - 1].name);
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != gameObject)
            {
                t.gameObject.name = iterator.ToString();
                iterator--;
            }
        }
    }
}
