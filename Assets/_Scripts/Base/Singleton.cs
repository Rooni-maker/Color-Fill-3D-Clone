using UnityEngine;

public interface IInit
{
    public void Init();
}

public class Singleton<T> : MonoBehaviour, IInit where T : Component
{
    public static T Instance {get; private set;}

    public virtual void Init()
    {
        if(Instance == null)
            Instance = this as T;
        else
            Destroy(gameObject);
    }

}

public class DontDestroySingleton<T> : MonoBehaviour, IInit where T : Component
{
    public static T Instance {get; private set;}

    public virtual void Init()
    {
        if(Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }
    
}