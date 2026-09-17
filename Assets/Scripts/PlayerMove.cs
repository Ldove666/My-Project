using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float fallDeathY = -8f;         //低于这个y值就掉落死亡
    public Vector3 respawnPoint;           //重生的位置

    bool isGrounded;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position; //开局保存出生坐标
    }

    void Update()
    {
        //读取键盘输入：左右移动（输入必须放在Update）
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        //空格跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        //物理帧循环检测掉落出地图死亡（没有碰撞物体，只能持续判断坐标）
        if (transform.position.y < fallDeathY)
        {
            Respawn();
        }
    }

    //物理引擎回调：发生碰撞瞬间调用一次，用于地面、地刺碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到地面
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        //撞到地刺立刻重生
        if (collision.collider.CompareTag("Spike"))
        {
            Respawn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //离开地面
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// 重生函数：回到出生点并且清空刚体速度，防止复活后带着惯性飞出去
    /// </summary>
    void Respawn()
    {
        transform.position = respawnPoint;
        rb.velocity = Vector2.zero;
    }
}
