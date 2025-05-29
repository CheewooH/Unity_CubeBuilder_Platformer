using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCube : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // ���� Y �ӵ� �ʱ�ȭ
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
