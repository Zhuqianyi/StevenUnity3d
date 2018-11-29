using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void Awake()
    {
        InitWidget();
        Init();
    }

    private void Update()
    {
        //判断球是否被击落.
        if (transform.position.y < 0)
        {
            //本回合结束.
            Debug.Log("被击落了");
            GameManager.Instance.TurnFinish();
            Destroy(gameObject);
        }
    }

    private Rigidbody _rigidbody;

    //球碰击的音效.
    private AudioClip _ballHitAudio;

    /// <summary>
    /// 获取组件函数.
    /// </summary>
    private void InitWidget()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ballHitAudio = Resources.Load<AudioClip>("Hit");
    }

    [SerializeField]
    private float _ballSpeedY = 5.0f;

    /// <summary>
    /// 初始化函数.
    /// </summary>
    private void Init()
    {
        //TODO:Test
        _rigidbody.velocity = new Vector3(0, 0, -_ballSpeedY);
    }

    /// <summary>
    /// 碰撞检测.
    /// </summary>
    /// <param name="other">碰撞到的物体</param>
    private void OnTriggerEnter(Collider other)
    {
        //检测碰到桌子.
        if (other.CompareTag("Table"))
        {
            Debug.Log("碰到了桌子");
            Debug.Log(_rigidbody.velocity);
            //修改速度,弹起.
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -_rigidbody.velocity.y + 0.2f, _rigidbody.velocity.z);
            if (_rigidbody.velocity.y < 5.0f)
            {
                _rigidbody.velocity += new Vector3(0, 0.3f, 0);
            }
            GameManager.Instance.HitTable();
        }

        if (other.CompareTag("Racket"))
        {
            Debug.Log("碰到了球拍");
            GameManager.Instance.SetTurnRacket(other.GetComponent<Racket>());
            //反弹.
            if (other.GetComponent<Racket>().RacketType == Racket.Type.Player)
            {
                //根据球拍速度改变.
                _rigidbody.velocity = new Vector3(PlayerController.Instance.MouseVelocity / 10, -_rigidbody.velocity.y,
                -_rigidbody.velocity.z);
            }
            if (other.GetComponent<Racket>().RacketType == Racket.Type.AI)
            {
                //AI:朝着中心方向击球.
                float speedX = AIController.Instance.GetBallSpeedX(_rigidbody.velocity.z);
                _rigidbody.velocity = new Vector3(speedX, -_rigidbody.velocity.y, -_rigidbody.velocity.z);
            }
            Debug.Log(_rigidbody.velocity);
        }
        AudioSource.PlayClipAtPoint(_ballHitAudio, transform.position, 100);
    }
}
