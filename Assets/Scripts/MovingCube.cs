using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.right; // 이동 방향
    [SerializeField] private float moveDistance = 3f; // 이동 거리
    [SerializeField] private float moveSpeed = 2f; // 이동 속도

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingToTarget = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        Vector3 destination = movingToTarget ? targetPos : startPos; // 목적지면 시작위치로 시작위치면 목적지로 설정
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // 목적지 도달 시 방향 전환
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingToTarget = !movingToTarget;
        }
    }
}
