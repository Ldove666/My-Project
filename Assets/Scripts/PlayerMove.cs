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

    //物理引擎回调：实体碰撞，只用来判断地面
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到地面
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        //【删掉了这里原来的Spike判断！地刺现在是触发器，不走这个！】
    }

    //离开地面
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //碰到地刺触发器执行死亡重生（地刺勾选IsTrigger，就由这个函数接收）
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Spike"))
        {
            Respawn();
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
