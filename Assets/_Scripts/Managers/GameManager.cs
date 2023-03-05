using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool _gameRunning = false;
    public bool GameRunning => _gameRunning;

    
    [SerializeField] Transform Camera = null;
    [SerializeField] PlayerController Player = null;

    [Header("Level Data")]
    [SerializeField] bool Debug       = false;

    [SerializeField] int StartLevel   = 0;
    [SerializeField] int CurrentLevel = 1;
    [SerializeField] LevelData[] Levels = null;

    public int GetCurrentLevel => CurrentLevel;

    
    private bool _gameStarted = false;
    private int   LevelToUse  = 1;

    private void SetLevels()
    {
        for(int i=0 ; i<Levels.Length ; i++)
            Levels[i].SetLevel($"Level {i+1}");
    }

    private void Start()
    {
        if(!Application.isEditor)
            Debug = false;
        foreach(LevelData Level in Levels)
            Level.LevelObject.SetActive(false);

        CurrentLevel = DataBase.Instance.Level;
        LevelToUse   = Debug ? StartLevel : DataBase.Instance.LevelToUse;

        if(LevelToUse > Levels.Length)
            LevelToUse = 1;
        
        DataBase.Instance.LevelToUse = LevelToUse;
        SaveSystem.SaveProgress();  

        GridManager.Instance.InitGrid(Levels[LevelToUse -1].Columns, Levels[LevelToUse -1].Rows);
        Levels[LevelToUse -1].ResetGroups();
        Levels[LevelToUse -1].LevelObject.SetActive(true);
        if(Levels[LevelToUse - 1].PlayerPos)
            Player.transform.position = Levels[LevelToUse - 1].PlayerPos.position;

        Haptics.Generate(HapticTypes.LightImpact);

        Camera.transform.position = new Vector3(-0.5f, 25f, 15f);
        Camera.transform.DOMoveZ(-5f, 0.5f);

        Player.Init();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !_gameStarted)   
            StartGame();
    }

    private void StartGame()
    {
        _gameStarted = true;
        AudioManager.Instance.Play("Click");
        UI_Manager.Instance.StartGame();
        _gameRunning = true;
    }

    public void LevelComplete()
    {
        Player.enabled = _gameRunning = false;
        CurrentLevel++;
        LevelToUse++;
        DataBase.Instance.Level = CurrentLevel;
        DataBase.Instance.LevelToUse = LevelToUse;
        UI_Manager.Instance.LevelWin();
        SaveSystem.SaveProgress();
        AudioManager.Instance.Play("GameWin");
    }

    public void LevelLose()
    {
        Player.enabled = _gameRunning = false;
        UI_Manager.Instance.LevelFail();
        AudioManager.Instance.Play("GameLose");
    }

    public void Replay()
    {
        AudioManager.Instance.Play("Click");
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
        }
    }
}

[System.Serializable]
public class LevelData
{
    public GameObject LevelObject = null;
    
    public Transform PlayerPos    = null;
   
    public int Columns = 10;
    public int Rows    = 10;

    public EnemyCubeGroup[] Groups = null;

    private string LevelName = null;

    public void SetLevel(string levelName)
    {                                                                                                                                                                                                                                                                                                                                                                                                                                             
        LevelName = levelName;
    }

    public void ResetGroups()
    {
        for(int i=0 ; i<Groups.Length ; i++)
            Groups[i].Restart();
    }
}