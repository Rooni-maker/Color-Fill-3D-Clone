//Shady
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[HideMonoScript]
public class GameManager : Singleton<GameManager>
{
    [Title("GAME MANAGER", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [DisplayAsString]
    [SerializeField] bool _gameRunning = false;
    public bool GameRunning => _gameRunning;

    [FoldoutGroup("Player References")]
    [SerializeField] Transform Camera = null;
    [FoldoutGroup("Player References")]
    [SerializeField] PlayerMovement Player = null;

    [FoldoutGroup("Level Data")]
    [SerializeField] bool Debug = false;
    [FoldoutGroup("Level Data")]
    [ShowIf("Debug", true)]
    [CustomValueDrawer("TotalLevels")]
    [SerializeField] int StartLevel = 0;
    [Space]
    [FoldoutGroup("Level Data")]
    [DisplayAsString]
    [SerializeField] int CurrentLevel = 1;
    [FoldoutGroup("Level Data")]
    [OnInspectorInit(nameof(SetLevels))]
    [OnValueChanged(nameof(SetLevels))]
    [SerializeField] LevelData[] Levels = null;

    public int GetCurrentLevel => CurrentLevel;

    // Private Fields
    private bool _gameStarted = false;
    private int   LevelToUse  = 1;

#if UNITY_EDITOR
    private int TotalLevels(int value, GUIContent label) => (int)UnityEditor.EditorGUILayout.Slider(label, value, 1, this.Levels.Length);
#endif
    private void SetLevels()
    {
        for(int i=0 ; i<Levels.Length ; i++)
            Levels[i].SetLevel($"Level {i+1}");
    }//SetLevels() end

    private void Start()
    {
        if(!Application.isEditor)
            Debug = false;
        foreach(LevelData Level in Levels)
            Level.LevelObject.SetActive(false);

        CurrentLevel = SaveData.Instance.Level;
        LevelToUse   = Debug ? StartLevel : SaveData.Instance.LevelToUse;

        if(LevelToUse > Levels.Length)
            LevelToUse = 1;
        
        SaveData.Instance.LevelToUse = LevelToUse;
        SaveSystem.SaveProgress();  

        GridManager.Instance.InitGrid(Levels[LevelToUse -1].Columns, Levels[LevelToUse -1].Rows);
        Levels[LevelToUse -1].ResetGroups();
        Levels[LevelToUse -1].LevelObject.SetActive(true);
        if(Levels[LevelToUse - 1].PlayerPos)
            Player.transform.position = Levels[LevelToUse - 1].PlayerPos.position;

        Haptics.Generate(HapticTypes.LightImpact);
        if(AudioManager.Instance)
            AudioManager.Instance?.StartGame();

        Camera.transform.position = new Vector3(-0.5f, 25f, 15f);
        Camera.transform.DOMoveZ(-5f, 0.5f);

        Player.Init();
    }//Start() end

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !_gameStarted)   
            StartGame();
    }//Update() end

    private void StartGame()
    {
        _gameStarted = true;
        AudioManager.Instance?.PlaySFX(SFX.Click);
        UI_Manager.Instance?.StartGame();
        _gameRunning = true;
    }//StartGame() end

    public void LevelComplete()
    {
        Player.enabled = _gameRunning = false;
        CurrentLevel++;
        LevelToUse++;
        SaveData.Instance.Level = CurrentLevel;
        SaveData.Instance.LevelToUse = LevelToUse;
        UI_Manager.Instance.LevelComplete();
        SaveSystem.SaveProgress();
        AudioManager.Instance?.PlaySFX(SFX.GameWin);
    }//LevelComplete() end

    public void LevelLose()
    {
        Player.enabled = _gameRunning = false;
        UI_Manager.Instance?.LevelLose();
        AudioManager.Instance?.GameEnd();
        AudioManager.Instance?.PlaySFX(SFX.GameLose);
    }//LevelFail() end

    public void Replay()
    {
        AudioManager.Instance?.PlaySFX(SFX.Click);
        CubePool.Instance.Restart();
        UI_Manager.Instance.Start();
        
        if(FadeSystem.Instance)
            FadeSystem.Instance.Fade(()=>
            {
                Start();
                Player.Restart();
            });
        else
        {
            Start();
            Player.Restart();
        }//else end
    }//Replay() end

}//class end

[System.Serializable]
public class LevelData
{
    [FoldoutGroup("$LevelName")]
    [Required]
    public GameObject LevelObject = null;
    [FoldoutGroup("$LevelName")]
    [Required]
    public Transform PlayerPos    = null;
    [FoldoutGroup("$LevelName")]
    public int Columns = 10;
    [FoldoutGroup("$LevelName")]
    public int Rows = 10;
    [FoldoutGroup("$LevelName")]
    public EnemyCubeGroup[] Groups = null;

    private string LevelName = null;

    public void SetLevel(string levelName) => LevelName = levelName;

    public void ResetGroups()
    {
        for(int i=0 ; i<Groups.Length ; i++)
            Groups[i].Restart();
    }//ResetGroups() end

}//class end