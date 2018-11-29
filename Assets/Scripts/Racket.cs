using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public enum Type
    {
        Player,
        AI
    }

    [SerializeField]
    private Type _type;

    public Type RacketType
    {
        get { return _type; }
    }

    private void Awake()
    {
        InitWidget();
    }

    private readonly float _rotateEdge = 3;

    private void Update()
    {
        //AI球拍跟随自身位置旋转.
        if (_type == Type.AI)
        {
            Rotate(GetRoateAngle(transform.position.x));
        }
    }

    //沿x轴旋转的角度.
    private float _rotateX;

    private float _posZ;

    /// <summary>
    /// z轴深度.
    /// </summary>
    public float PosZ
    {
        get { return _posZ; }
    }

    /// <summary>
    /// 初始化组件函数.
    /// </summary>
    private void InitWidget()
    {
        _rotateX = transform.rotation.eulerAngles.x;
        _posZ = transform.position.z;
    }

    /// <summary>
    /// 球拍沿着z轴旋转的角度.
    /// </summary>
    /// <param name="angle">角度</param>
    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(_rotateX, 0, angle));
    }

    /// <summary>
    /// 球拍移动速度.
    /// </summary>
    [SerializeField]
    private float _moveSpeed = 1.0f;

    /// <summary>
    /// 球拍移动.
    /// </summary>
    /// <param name="dst">目标位置</param>
    public void Move(Vector3 dst)
    {
        transform.position = dst;
    }

    /// <summary>
    /// 根据坐标获取旋转角度.
    /// </summary>
    /// <param name="x">x位置</param>
    private float GetRoateAngle(float x)
    {
        if (x > _rotateEdge)
        {
            return -90;
        }
        else if (x < -_rotateEdge)
        {
            return 90;
        }
        else
        {
            return -30 * x;
        }
    }
}
