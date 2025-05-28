using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private LayerMask groundLayer; // 땅 레이어

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 컴포넌트가 필요합니다.");
        }
    }
    void Update()
    {
        // 1. 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal"); // 좌우
        float verticalInput = Input.GetAxis("Vertical");   // 상하

        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize(); // 대각선 이동 시 속도 일정하게 유지

        // 2. 점프 입력
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
