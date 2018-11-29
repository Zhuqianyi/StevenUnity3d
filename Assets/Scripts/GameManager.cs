using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    private static GameManager _instance;
    public static GameManager Instance
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

    private Racket _playerRacket;
    private Racket _aiRacket;

    /// <summary>
    /// 初始化组件函数.
    /// </summary>
    private void InitWidget()
    {
        _instance = this;
        _playerRacket = GameObject.Find("PlayerRacket").GetComponent<Racket>();
        _aiRacket = GameObject.Find("AIRacket").GetComponent<Racket>();
        _ballPrefab = Resources.Load<GameObject>("Ball");
    }

    /// <summary>
    /// 击球者(符合击球条件).
    /// </summary>
    private Racket _hitter;

    /// <summary>
    /// 击中球的球拍(还未符合胜利条件).
    /// </summary>
    private Racket _turnRacket;

    /// <summary>
    /// 初始化函数.
    /// </summary>
    private void Init()
    {
        _hitter = _aiRacket;
        _turnRacket = _aiRacket;
        UpdateScoreText();
        _hitTableCount = 0;
    }

    //玩家的分数存储.
    private int _playerScore;
    private int _aiScore;

    /// <summary>
    /// 一回合结束.
    /// </summary>
    public void TurnFinish()
    {
        Debug.Log(_hitter.gameObject.name + "取得了胜利");
        switch (_hitter.RacketType)
        {
            case Racket.Type.Player:
                _playerScore++;
                break;
            case Racket.Type.AI:
                _aiScore++;
                break;
            default:
                break;
        }
        UpdateScoreText();
        _isGaming = false;
        UIManager.Instance.ShowServeButton();
    }

    /// <summary>
    /// 设置击中球的一方.
    /// </summary>
    /// <param name="racket">球拍</param>
    public void SetTurnRacket(Racket racket)
    {
        _turnRacket = racket;
    }

    private int _hitTableCount = 0;

    /// <summary>
    /// 球碰到桌面,计数增加.
    /// </summary>
    public void HitTable()
    {
        _hitTableCount++;
        //判断是否满足击中两次桌面的要求.
        if (_hitTableCount == 2)
        {
            _hitter = _turnRacket;
            _hitTableCount = 0;
            Debug.Log("当前击球成功的一方为:" + _hitter.gameObject.name);
        }
    }
    
    /// <summary>
    /// 更新分数信息.
    /// </summary>
    private void UpdateScoreText()
    {
        UIManager.Instance.ShowScoreText(_playerScore, _aiScore);
    }

    private bool _isGaming;
    /// <summary>
    /// 是否正在游戏.
    /// </summary>
    public bool IsGaming
    {
        get { return _isGaming; }
    }

    private GameObject _ballPrefab;

    /// <summary>
    /// 开始游戏.
    /// </summary>
    public void StartGame()
    {
        Init();
        _isGaming = true;
        GameObject clone = Instantiate(_ballPrefab);
        AIController.Instance.SetBall(clone);
    }
}
