using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCube : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float buoyancy = 10f; // �η�
    [SerializeField] private float buoyancyDecrease = 1f;   // �η� ���� �ӵ�

    // �÷��̾ ������ �η��� �����ϴ� ��ųʸ�
    private Dictionary<Rigidbody, float> playerCurrentbuoyancy = new Dictionary<Rigidbody, float>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �÷��̾ ���� �����ų� �ٽ� ������ �η��� �ִ�� �ʱ�ȭ
                if (!playerCurrentbuoyancy.ContainsKey(rb))
                {
                    playerCurrentbuoyancy.Add(rb, buoyancy);
                }
                else
                {
                    playerCurrentbuoyancy[rb] = buoyancy;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && playerCurrentbuoyancy.ContainsKey(rb))
            {
                float currentForce = playerCurrentbuoyancy[rb];

                // ���� �η��� �÷��̾�� ����
                if (currentForce > 0)
                {
                    rb.AddForce(Vector3.up * currentForce, ForceMode.Acceleration);
                }

                // �ð��� ���� �η� ����
                playerCurrentbuoyancy[rb] = Mathf.Max(0, currentForce - (buoyancyDecrease * Time.deltaTime));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && playerCurrentbuoyancy.ContainsKey(rb))
            {
                // �÷��̾ ������ ����� �ش� �÷��̾� ���� ����
                playerCurrentbuoyancy.Remove(rb);
            }
        }
    }
}
