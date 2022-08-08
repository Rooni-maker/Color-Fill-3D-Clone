//Shady
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Initializer : MonoBehaviour
{
    [Title("SEQUENTIAL INITIALIZER", null, titleAlignment: TitleAlignments.Centered)]
    [SerializeField] MonoBehaviour[] Scripts = null;

    private void Awake()
    {
        DOTween.Init();
        for(int i=0 ; i<Scripts.Length ; i++)
        {
            if(Scripts[i].TryGetComponent<IInit>(out IInit init))
                init.Init();
        }//loop end
    }//Awake() end

}//class end