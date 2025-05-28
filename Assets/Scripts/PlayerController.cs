using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵� ����")]
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float jumpForce = 5f; // ���� ��
    [SerializeField] private LayerMask groundLayer; // �� ���̾�

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D ������Ʈ�� �ʿ��մϴ�.");
        }
    }
    void Update()
    {
        // 1. �Է� ó��
        float horizontalInput = Input.GetAxis("Horizontal"); // �¿�
        float verticalInput = Input.GetAxis("Vertical");   // ����

        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize(); // �밢�� �̵� �� �ӵ� �����ϰ� ����

        // 2. ���� �Է�
        if (Input.GetButtonDown("Jump") && isGrounded) // �����̽��� �Է� ����
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
