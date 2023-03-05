using UnityEngine;
using DG.Tweening;

public class Initializer : MonoBehaviour
{
    [Header("SEQUENTIAL INITIALIZER")]
    [SerializeField] MonoBehaviour[] Scripts = null;

    private void Awake()
    {
        DOTween.Init();
        for(int i=0 ; i<Scripts.Length ; i++)
        {
            if(Scripts[i].TryGetComponent<IInit>(out IInit init))
                init.Init();
        }
    }

}