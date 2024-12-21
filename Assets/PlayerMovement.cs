using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 移動速度與跳躍力量
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Rigidbody2D 組件
    private Rigidbody2D rb;

    // 按鍵綁定
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    // 是否在地面上
    private bool isGrounded;

    // 地面檢測相關
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        // 獲取 Rigidbody2D 組件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 水平移動邏輯
        float moveInput = 0f;

        if (Input.GetKey(leftKey))
        {
            moveInput = -1f;
        }
        if (Input.GetKey(rightKey))
        {
            moveInput = 1f;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 翻轉角色朝向
        if (moveInput > 0)
            transform.localScale = new Vector3(5, 5, 5);  // 朝右
        else if (moveInput < 0)
            transform.localScale = new Vector3(-5, 5, 5); // 朝左

        // 跳躍邏輯
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // 地面檢測
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}
