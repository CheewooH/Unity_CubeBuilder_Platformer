using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCube : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float buoyancy = 10f; // 부력
    [SerializeField] private float buoyancyDecrease = 1f;   // 부력 감소 속도

    // 플레이어가 들어오면 부력을 저장하는 딕셔너리
    private Dictionary<Rigidbody, float> playerCurrentbuoyancy = new Dictionary<Rigidbody, float>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 플레이어가 새로 들어오거나 다시 들어오면 부력을 최대로 초기화
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

                // 현재 부력을 플레이어에게 적용
                if (currentForce > 0)
                {
                    rb.AddForce(Vector3.up * currentForce, ForceMode.Acceleration);
                }

                // 시간에 따라 부력 감소
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
                // 플레이어가 완전히 벗어나면 해당 플레이어 정보 제거
                playerCurrentbuoyancy.Remove(rb);
            }
        }
    }
}
