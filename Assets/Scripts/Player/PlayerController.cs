using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    [Header("마우스 회전 설정")]
    public float mouseSensitivity = 2f;
    public Transform cameraPivot; // 카메라 피벗 (상하 회전)
    public Transform cameraTransform; // 메인 카메라

    private Rigidbody rb;
    private Animator animator;
    private float pitch = 0f;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력 받아 플레이어 좌우 회전 + 카메라 상하 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0); // 좌우는 캐릭터 회전

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45f, 70f);
        cameraPivot.localEulerAngles = new Vector3(pitch, 0, 0);

        // 바닥 체크
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, groundLayer);

        // 점프 입력
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 애니메이션 처리 (선택 사항)
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = input.magnitude > 0.1f;
        if (animator)
        {
            animator.SetBool("IsWalk", isMoving);
            animator.SetBool("IsIdle", !isMoving);
            animator.SetBool("IsJump", !isGrounded);
        }
    }

    void FixedUpdate()
    {
        // 입력 방향을 캐릭터 기준으로 변환
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);

        Vector3 move = transform.TransformDirection(inputDir) * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
