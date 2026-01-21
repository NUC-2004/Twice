using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 玩家对象
    public float smoothSpeed = 0.125f;  // 跟随的平滑速度
    public Vector3 offset;  // 摄像头相对于玩家的位置偏移

    void Start()
    {
        // 设置相机的初始偏移
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = player.position + offset;
        
        // 平滑地移动相机
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // 设置相机的新位置
        transform.position = smoothedPosition;
    }
}

