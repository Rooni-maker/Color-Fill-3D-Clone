using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeSystem : DontDestroySingleton<FadeSystem>
{
    [SerializeField] Image Panel = null;
    [Range(0.2f, 1.0f)]
    [SerializeField] float FadeSpeed = 1.0f;

    public override void Init()
    {
        base.Init();
        Panel = GetComponentInChildren<Image>();
    }

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