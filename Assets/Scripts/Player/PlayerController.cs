using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private LayerMask groundLayer; // 바닥을 감지할 레이어 마스크

    private Rigidbody rb;
    private Vector3 moveDirection; // 플레이어 이동 방향
    private bool isGrounded; // 플레이어가 바닥에 붙어있는지 확인

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 방향키
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D 또는 좌우 화살표
        float verticalInput = Input.GetAxis("Vertical");   // W/S 또는 상하 화살표

        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize(); // 대각선 이동 시 속도 일정하게 유지

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded) // 스페이스바 입력 감지
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.9f, 0.2f, groundLayer);
    }
}
