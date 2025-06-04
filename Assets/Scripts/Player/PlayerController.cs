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

    private bool isPlayerMove = false;
    private Vector3 initialPosition; // 플레이어 초기 위치
    private Quaternion initialRotation; // 플레이어 초기 회전

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // 마우스 입력 받아 플레이어 좌우 회전 + 카메라 상하 회전
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            transform.Rotate(0, mouseX, 0); // 좌우는 캐릭터 회전

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -45f, 70f);
            if (cameraPivot != null)
            {
                cameraPivot.localEulerAngles = new Vector3(pitch, 0, 0);
            }

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
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!isPlayerMove && (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f))
            {
                isPlayerMove = true;
                GameManager.Instance.PlayerHasMoved();
            }

            if (cameraTransform == null) return;

            // 입력 방향을 카메라 기준으로 변환
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirRaw = forward * v + right * h;
            Vector3 moveDir = moveDirRaw.normalized;
            if (moveDir.magnitude > 0.01f)
            {
                rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
            }
            else // 미끄럼 방지
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }

        else if (GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Playing)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    
    public void ResetPlayer()
    {
        isPlayerMove = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        pitch = 0f;
        if (animator)
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsJump", false);
        }
    }
}
