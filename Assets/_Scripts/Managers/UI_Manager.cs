//Shady
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[HideMonoScript]
public class UI_Manager : Singleton<UI_Manager>
{
    [Title("UI_MANAGER", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] CanvasGroup StartScreen      = null;
    [SerializeField] TMP_Text    CurrentLevelText = null;
    [SerializeField] TMP_Text    NextLevelText    = null;
    [SerializeField] GameObject  LevelWinPanel    = null;
    [SerializeField] Button      NextLevelBtn     = null;
    [SerializeField] TMP_Text    LevelWinText     = null;
    [SerializeField] GameObject  LevelLosePanel   = null;
    [SerializeField] Button      RetryLevelBtn    = null;
    [SerializeField] TMP_Text    LevelFailText    = null;
    [SerializeField] Image       Fill             = null;

    public void Start()
    {
        LevelWinPanel.SetActive(false);
        LevelLosePanel.SetActive(false);
        Fill.fillAmount = 0f;
        NextLevelBtn.onClick.RemoveAllListeners();
        NextLevelBtn.onClick.AddListener(GameManager.Instance.Replay);
        RetryLevelBtn.onClick.RemoveAllListeners();
        RetryLevelBtn.onClick.AddListener(GameManager.Instance.Replay);
        NextLevelText.text    = (GameManager.Instance.GetCurrentLevel + 1).ToString();
        CurrentLevelText.text = GameManager.Instance.GetCurrentLevel.ToString();
    }//Start() end

    public void FillAmount(float amount) => Fill.DOFillAmount(amount, 0.25f);

    public void StartGame() => StartScreen.DOFade(0.0f, 0.5f).OnComplete(()=>StartScreen.gameObject.SetActive(false));

    public void LevelComplete()
    {
        LevelWinText.text = "LEVEL\n<size=190>COMPLETED!";
        LevelWinPanel.SetActive(true);
        LevelWinPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }//LevelComplete() end

    public void LevelLose()
    {
        LevelFailText.text = "LEVEL\n<size=200>FAILED";
        LevelLosePanel.SetActive(true);
        LevelLosePanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
    }//LevelLose() end

}//class end