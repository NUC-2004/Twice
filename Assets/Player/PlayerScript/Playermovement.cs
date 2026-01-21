using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 水平移动速度
    public float jumpForce = 7f; // 跳跃的力
    public LayerMask groundMask;  // 地面检测层，确保能够检测到箱子、平台、斜坡
    private Rigidbody2D rb;
    private bool isGrounded;  // 玩家是否在地面上
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 获取刚体组件
    }

    void Update()
    {
        // 检测地面
        CheckGround();
        moveInput = Input.GetAxisRaw("Horizontal"); // 获取水平输入（A/D 键 或 左右箭头键）

        // 水平移动
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); 

        // 跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 设置垂直速度来实现跳跃
        }

        
    }

    // 检测是否站在地面上，包括平台、箱子和斜坡
    void CheckGround()
    {
        // 使用 OverlapCircle 检测玩家脚下的地面
        isGrounded = Physics2D.OverlapCircle(transform.position, 1.1f, groundMask);
    }
}