using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float pushForce = 5f;  // 推动箱子的力
    public LayerMask groundLayer; // 用来检测地面的层
    public float slideForce = 2f; // 箱子在斜坡上的滑动力
    public float maxSlopeAngle = 45f;  // 最大斜坡角度，超过该角度箱子会滑下
    public float edgeRayLength = 0.2f; // 检测平台边缘的射线长度
    public float gravityScale = 2f;  // 重力比例，控制箱子下落的速度

    private Rigidbody2D rb;
    private bool isPlayerNearby;  // 玩家是否在附近推动箱子
    private bool isOnSlope;       // 箱子是否在斜坡上
    private float slopeAngle;     // 当前斜坡的角度

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 获取刚体组件
        rb.gravityScale = gravityScale;  // 设置箱子的重力
        rb.freezeRotation = false;  // 允许箱子旋转
    }

    void Update()
    {
        // 检测箱子是否在斜坡上
        CheckSlope();

        // 如果玩家接近并且按下了水平移动键，推动箱子
        if (isPlayerNearby && Input.GetAxisRaw("Horizontal") != 0)
        {
            PushBox();
        }
        else
        {
            // 如果箱子在斜坡上且没有被推动，应用滑动力
            if (isOnSlope && slopeAngle > maxSlopeAngle)
            {
                SlideDownSlope();
            }
        }
    }

    // 检测箱子是否处于斜坡上
    void CheckSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        if (hit.collider != null)
        {
            // 获取地面的法线与竖直方向的夹角
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // 判断斜坡的角度是否大于最大允许角度
            isOnSlope = slopeAngle <= maxSlopeAngle;
        }
        else
        {
            isOnSlope = false;
        }
    }

    // 玩家推动箱子的逻辑
    void PushBox()
    {
        // 获取玩家的水平输入（A/D 或 左右箭头键）
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 使用 transform.right 获取箱子当前的右侧方向，确保根据箱子的旋转调整推动方向
        Vector2 pushDirection = transform.right;

        // 根据箱子的旋转方向来应用推动力
        rb.velocity = new Vector2(moveInput * pushForce * pushDirection.x, rb.velocity.y);  // 水平推动
    }

    // 箱子在斜坡上滑动的逻辑
    void SlideDownSlope()
    {
        // 计算沿着斜坡的滑动力
        Vector2 slideDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * slopeAngle), -Mathf.Cos(Mathf.Deg2Rad * slopeAngle));
        rb.AddForce(slideDirection * slideForce, ForceMode2D.Force);
    }

    // 检测玩家是否靠近箱子
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerNearby = true;  // 玩家在附近时，可以推动箱子
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerNearby = false;  // 玩家离开时，不能推动箱子
        }
    }
}
