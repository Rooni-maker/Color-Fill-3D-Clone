//Shady
//FadeSystem v1.1_2
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[HideMonoScript]
public class FadeSystem : DontDestroySingleton<FadeSystem>
{
    [Title("FADE SYSTEM", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] Image Panel = null;
    [Range(0.2f, 1.0f)]
    [SerializeField] float FadeSpeed = 1.0f;

    public override void Init()
    {
        base.Init();
        Panel = GetComponentInChildren<Image>();
    }//Awake() end

    public void Fade(GameObject TurnOff = null, GameObject TurnOn = null)
    {
        Panel.raycastTarget = true;
        Panel.DOFade(1.0f, FadeSpeed).OnComplete(()=>
        {
            if(TurnOff)
                TurnOff.SetActive(false);
            if(TurnOn)
                TurnOn.SetActive(true);
            Panel.DOFade(0.0f, FadeSpeed).OnComplete(()=>Panel.raycastTarget = false);
        });//Tween end
    }//Fade() end

    public void Fade(TweenCallback Action)
    {
        Panel.raycastTarget = true;
        Panel.DOFade(1.0f, FadeSpeed).OnComplete(()=>
        {
            if(Action != null)
                Action();
            Panel.DOFade(0.0f, FadeSpeed).OnComplete(()=>Panel.raycastTarget = false);
        });//Tween end
    }//Fade() end

}//class end