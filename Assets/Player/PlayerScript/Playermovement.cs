using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 水平移动速度
    public float jumpForce = 10f; // 跳跃的力
    public Transform groundCheck; // 用于检测玩家是否在地面
    public LayerMask groundMask;  // 地面检测层

    private Rigidbody2D rb;
    private bool isGrounded;  // 是否在地面上
    private float moveInput;  // 玩家水平输入

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 获取刚体组件
    }

    void Update()
    {
        // 检查是否在地面
        CheckGround();

        // 获取玩家输入
        moveInput = Input.GetAxisRaw("Horizontal"); // 水平输入

        // 移动和跳跃
        Move();
        Jump();
    }

    void Move()
    {
        // 水平移动
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        // 如果在地面并按下跳跃键，跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 设置垂直速度来实现跳跃
        }
    }

    void CheckGround()
    {
        // 检测玩家是否站在地面上
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
    }
}