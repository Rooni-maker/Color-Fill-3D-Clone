//Shady
using UnityEngine;

public interface IInit
{
    public void Init();
}//interface end

public class Singleton<T> : MonoBehaviour, IInit where T : Component
{
    public static T Instance {get; private set;}

    public virtual void Init()
    {
        if(Instance == null)
            Instance = this as T;
        else
            Destroy(gameObject);
    }//Init() end

}//class end

public class DontDestroySingleton<T> : MonoBehaviour, IInit where T : Component
{
    public static T Instance {get; private set;}

    public virtual void Init()
    {
        if(Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }//if end
        else
            DestroyImmediate(gameObject);
    }//Init() end
    
}//class end