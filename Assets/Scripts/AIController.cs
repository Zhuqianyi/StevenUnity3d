using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI球拍的控制器.
/// </summary>
public class AIController : MonoBehaviour 
{
    private static AIController _instance;
    public static AIController Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        InitWidget();
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// AI的球拍.
    /// </summary>
    private Racket _aiRacket;

    /// <summary>
    /// 场上球的物体.
    /// </summary>
    private GameObject _ball;

    /// <summary>
    /// 获取组件函数.
    /// </summary>
    private void InitWidget()
    {
        _instance = this;
        _aiRacket = GameObject.Find("AIRacket").GetComponent<Racket>();

    }

    /// <summary>
    /// 设置对应的乒乓球.
    /// </summary>
    /// <param name="ball">物体</param>
    public void SetBall(GameObject ball)
    {
        _ball = ball;
    }

    /// <summary>
    /// 初始化函数.
    /// </summary>
    private void Init()
    {
        _dstPosition = _aiRacket.transform.position;
        _dstY = _dstPosition.y;
    }

    /// <summary>
    /// 每隔多久思考一次.
    /// </summary>
    [SerializeField]
    private float _thinkTimeOfIdle = 2.0f;

    //思考时间计时器.
    private float _thinkTimeCounter = 0;

    /// <summary>
    /// 下一步需要移动到的位置.
    /// </summary>
    private Vector3 _dstPosition;

    private float _dstY;

    /// <summary>
    /// 移动速度.
    /// </summary>
    [SerializeField]
    private float _aiMoveSpeed = 1.0f;

    private void Update()
    {
        if (!GameManager.Instance.IsGaming)
        {
            return;
        }
        //_dstPosition = GetDestination();
        _thinkTimeCounter += Time.deltaTime;
        if (_thinkTimeCounter > _thinkTimeOfIdle)
        {
            //获取一次目标位置y值.
            _dstPosition = GetDestination();
            Debug.Log("下一步AI需要移动到的位置：" + _dstPosition);
            //清空计时器.
            _thinkTimeCounter = 0;
        }
        
        //插值移动到目标位置.
        _aiRacket.transform.localPosition = Vector3.Lerp(_aiRacket.transform.position, _dstPosition, _aiMoveSpeed * Time.deltaTime);
        //_aiRacket.transform.localPosition = _dstPosition;
        //TODO:AI可以攻击.

    }

    /// <summary>
    /// 获取AI球拍需要移动到的位置.
    /// </summary>
    /// <returns>目标位置</returns>
    private Vector3 GetDestination()
    {
        //球的位置.
        Vector3 ballPos = _ball.transform.position;
        //移动方向.
        Vector3 moveDir = _ball.GetComponent<Rigidbody>().velocity;
        //目标位置.
        Vector3 dstPos = new Vector3 { y = _aiRacket.transform.position.y, z = _aiRacket.transform.position.z };
        dstPos.x = (moveDir.x / moveDir.z) * (dstPos.z - ballPos.z) + ballPos.x;
        return dstPos;
        //return new Vector3(_ball.transform.position.x, _dstPosition.y, _aiRacket.transform.position.z);
    }

    /// <summary>
    /// 获取小球X方向的速度.
    /// </summary>
    /// <param name="ballSpeedZ">球Z轴方向的速度</param>
    /// <returns></returns>
    public float GetBallSpeedX(float ballSpeedZ)
    {
        //随机落点.
        float dstZ = Random.Range(-3.7f, -1.7f);
        //球空中运动的时间.
        float time = (dstZ - _aiRacket.transform.position.z) / ballSpeedZ;
        time = Mathf.Abs(time);
        return (-_aiRacket.transform.position.x) / time;
    }
}
