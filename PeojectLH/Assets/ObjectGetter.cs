using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetter : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enermy")
        {
            if (!colliders.Contains(other)) {
                Debug.Log(other.name + " In Attack Area.");
                colliders.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
        Debug.Log(other.name + " out Attack Area.");
    }
}
