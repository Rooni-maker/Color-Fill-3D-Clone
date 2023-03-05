//Introduce your new variables under Game Variables and pass them accordingly
//in the Constructor => SaveData() and Method => CreateSaveObject()

public class DataBase
{
    private static DataBase _Instance = null;
    public static DataBase Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new DataBase();
                SaveSystem.LoadProgress();
            }//if end
            return _Instance;
        }//get end
    }//Property end

    public void Reset() => _Instance = new DataBase();

    //Setting Variable
    public bool Music  = true;
    public bool SFX    = true;
    public bool Haptic = true;

    //Game Variables
    public int Level = 1;
    public int LevelToUse = 1;

    public string HashOfSaveData;

    private DataBase(){}

    private DataBase(int level, int levelToUse)
    {
        Level      = level;
        LevelToUse = levelToUse;
    }//SaveData() end

    public DataBase CreateSaveObject() => new DataBase(Instance.Level, Instance.LevelToUse);

}//class end