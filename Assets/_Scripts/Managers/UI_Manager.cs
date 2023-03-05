using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_Manager : Singleton<UI_Manager>
{
    [Space(20)]
    [Header("Game Win n Fail UI Data")]

    [SerializeField] CanvasGroup _startScreen      = null;
    [SerializeField] GameObject  _levelWinPanel    = null;
    [SerializeField] GameObject  _levelLosePanel   = null;

    [SerializeField] Button      _nextLevelBtn     = null;
    [SerializeField] Button      _retryLevelBtn    = null;

    [Space(20)]
    [Header("Text for level Data")]

    [SerializeField] TextMeshProUGUI _currentLevelText = null;
    [SerializeField] TextMeshProUGUI _nextLevelText    = null;
    [SerializeField] TextMeshProUGUI _levelWinText     = null;
    [SerializeField] TextMeshProUGUI _levelFailText    = null;

    [SerializeField] Image       fillBar          = null;
    [SerializeField] float       fillTime      = 0.3f;


    public void Start()
    {
        _levelWinPanel.SetActive(false);
        _levelLosePanel.SetActive(false);
        fillBar.fillAmount = 0f;
        _nextLevelBtn.onClick.RemoveAllListeners();
        _nextLevelBtn.onClick.AddListener(GameManager.Instance.Replay);
        _retryLevelBtn.onClick.RemoveAllListeners();
        _retryLevelBtn.onClick.AddListener(GameManager.Instance.Replay);
        _nextLevelText.text    = (GameManager.Instance.GetCurrentLevel + 1).ToString();
        _currentLevelText.text = GameManager.Instance.GetCurrentLevel.ToString();
    }

    public void FillAmount(float value)
    {
        fillBar.DOFillAmount(value, fillTime);
    }
    public void StartGame()
    {
        _startScreen.DOFade(0.0f, 0.5f).OnComplete(() =>
        {
            _startScreen.gameObject.SetActive(false);
        });
    }
    #region Level Win and Fail Scenario
    public void LevelWin()
    {
        _levelWinText.text = "LEVEL\n<size=190>COMPLETED!";
        _levelWinPanel.SetActive(true);
        _levelWinPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }

    public void LevelFail()
    {
        _levelFailText.text = "LEVEL\n<size=200>FAILED";
        _levelLosePanel.SetActive(true);
        _levelLosePanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }
    #endregion
}