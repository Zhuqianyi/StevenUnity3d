using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        InitWidget();
    }

    private float _lastX;

    private float _mouseVelocity;

    /// <summary>
    /// 鼠标移动速度.
    /// </summary>
    public float MouseVelocity
    {
        get { return _mouseVelocity; }
    }

    private void Update()
    {
        UpdateRacketState();
        //获取鼠标的移动速度.
        _mouseVelocity = Input.mousePosition.x - _lastX;
        _lastX = Input.mousePosition.x;
        //Debug.Log("MouseVelocity:"+_mouseVelocity);
    }

    /// <summary>
    /// 玩家的球拍物体.
    /// </summary>
    private Racket _racket;

    /// <summary>
    /// 获取组件函数.
    /// </summary>
    private void InitWidget()
    {
        _instance = this;
        _racket = GameObject.Find("PlayerRacket").GetComponent<Racket>();
    }

    /// <summary>
    /// 更新球拍的状态.
    /// </summary>
    private void UpdateRacketState()
    {
        //更改位置.
        _racket.Move(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10 + _racket.PosZ)));
        //更改旋转角度.
        _racket.Rotate(GetRotateAngle(Input.mousePosition.x));
    }

    /// <summary>
    /// 获取旋转角度的函数.
    /// </summary>
    /// <param name="x">屏幕上x的坐标位置</param>
    /// <returns>旋转角度</returns>
    private static float GetRotateAngle(float x)
    {
        //斜率.
        float k = -(float)180 / Screen.width;
        return k * x + 90;
    }
}
