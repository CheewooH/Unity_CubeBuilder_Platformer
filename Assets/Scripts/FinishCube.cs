using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCube : MonoBehaviour
{
    public string playerTag = "Player"; // �÷��̾� �±�

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("����!!!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameFinished();
            }
        }
    }
}
