using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float rotationSpeed = 200f; // 회전 속도
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    [SerializeField] private float rayLength = 0.2f; // 바닥 감지 레이 길이

    private Animator animator;
    private Rigidbody rb;
    private float verticalInput; // 수직 입력 값을 저장할 변수
    private bool isGrounded; // 플레이어가 바닥에 붙어있는지 확인

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 방향키
        float horizontalInput = Input.GetAxis("Horizontal"); // 회전
        verticalInput = Input.GetAxis("Vertical");   // 이동

        // 캐릭터 회전
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // 점프
        if (isGrounded)
        {
            animator.SetBool("IsJump", false); // 땅에 있으면 점프 상태 해제

            bool isMoving = Mathf.Abs(verticalInput) > 0.1f; // 앞뒤 이동 감지
            animator.SetBool("IsWalk", isMoving);
            animator.SetBool("IsIdle", !isMoving);

            // 점프 입력 감지
            if (Input.GetButtonDown("Jump")) // 스페이스바
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("IsJump", true);  // 점프 애니메이션 시작
                animator.SetBool("IsWalk", false); // 점프 중에는 걷기/멈춤 애니메이션 해제
            }
        }
    }

    void FixedUpdate()
    {
        // 바닥 감지
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        isGrounded = Physics.Raycast(ray, rayLength, groundLayer);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        // 캐릭터 이동
        Vector3 move = transform.forward * verticalInput * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
