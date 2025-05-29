using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCube : MonoBehaviour
{
    [SerializeField] private float sinkForce = 5f; // บüม๖ดย ศ๛
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.down * sinkForce, ForceMode.Acceleration);
            }
        }
    }
}
