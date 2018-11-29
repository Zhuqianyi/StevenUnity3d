using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{

    private static UIManager _instance;
    public static UIManager Instance
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

    private Text _playerScore;
    private Text _aiScore;

    private Button _serveButton;

    /// <summary>
    /// 获取组件函数.
    /// </summary>
    private void InitWidget()
    {
        _instance = this;
        _playerScore = transform.Find("PlayerScore/Count").GetComponent<Text>();
        _aiScore = transform.Find("AIScore/Count").GetComponent<Text>();
        _serveButton = transform.Find("ServeButton").GetComponent<Button>();
        _serveButton.onClick.AddListener(OnServeButtonClick);
    }

    /// <summary>
    /// 初始化函数.
    /// </summary>
    private void Init()
    {
        
    }

    /// <summary>
    /// 显示分数信息.
    /// </summary>
    /// <param name="player">玩家分数</param>
    /// <param name="ai">电脑分数</param>
    public void ShowScoreText(int player, int ai)
    {
        _playerScore.text = player.ToString();
        _aiScore.text = ai.ToString();
    }

    /// <summary>
    /// 显示准备按钮.
    /// </summary>
    public void ShowServeButton()
    {
        _serveButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 按下准备按钮的事件.
    /// </summary>
    private void OnServeButtonClick()
    {
        GameManager.Instance.StartGame();
        _serveButton.gameObject.SetActive(false);
    }
}
