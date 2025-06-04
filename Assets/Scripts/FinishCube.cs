using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCube : MonoBehaviour
{
    public string playerTag = "Player"; // 플레이어 태그

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("도착!!!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameFinished();
            }
        }
    }
}
