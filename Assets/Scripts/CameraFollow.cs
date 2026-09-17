using UnityEngine;
/// <summary>
/// 使用独立相机速度来跟随玩家，尝试消除刚体移动横向抖动
/// 挂载到MainCamera
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标，拖入Player")]
    public Transform target;
    [Header("跟随力度 (2‑6，越大跟随越快)")]
    public float followStrength = 4f;
    [Header("相机偏移")]
    public Vector3 offset;

    [Header("地图边界")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector3 cameraVelocity; //✅相机自己的速度（你要的camera速度）

    void LateUpdate()
    {
        if (target == null) return;

        //目标理想坐标（读取刚体物理位置，避免transform滞后）
        Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
        Vector3 targetPos;
        if(playerRb != null)
        {
            targetPos = (Vector3)playerRb.position + offset;
        }
        else
        {
            targetPos = target.position + offset;
        }

        // 计算相机虚拟速度：朝着目标位置加速靠拢
        Vector3 delta = targetPos - transform.position;
        cameraVelocity = delta * followStrength;

        // 使用虚拟速度移动相机
        Vector3 newCamPos = transform.position + cameraVelocity * Time.deltaTime;

        //边界限制
        newCamPos.x = Mathf.Clamp(newCamPos.x, minX, maxX);
        newCamPos.y = Mathf.Clamp(newCamPos.y, minY, maxY);

        transform.position = newCamPos;
    }
}
