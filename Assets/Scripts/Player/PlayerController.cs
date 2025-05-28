using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵� ����")]
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float jumpForce = 5f; // ���� ��
    [SerializeField] private LayerMask groundLayer; // �ٴ��� ������ ���̾� ����ũ

    private Rigidbody rb;
    private Vector3 moveDirection; // �÷��̾� �̵� ����
    private bool isGrounded; // �÷��̾ �ٴڿ� �پ��ִ��� Ȯ��

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ����Ű
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D �Ǵ� �¿� ȭ��ǥ
        float verticalInput = Input.GetAxis("Vertical");   // W/S �Ǵ� ���� ȭ��ǥ

        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize(); // �밢�� �̵� �� �ӵ� �����ϰ� ����

        // ����
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
