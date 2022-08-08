//Shady
using UnityEngine;
using Sirenix.OdinInspector;

public enum SFX
{
    Click,
    GameWin,
    GameLose,
    Collect,
    Impact
}//enum end

[HideMonoScript]
public class AudioManager : DontDestroySingleton<AudioManager>
{
    [Title("AUDIO MANAGER", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] AudioSource BGSource   = null;
    [SerializeField] AudioSource SFXSource  = null;

    [FoldoutGroup("Background Music")]
    [SerializeField] AudioClip BGMusic = null;

    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip ClickClip    = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip GameWinClip  = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip GameLoseClip = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip CollectClip  = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip ImpactClip   = null;

    public override void Init()
    {
        base.Init();
        SetBGSetting(SaveData.Instance.Music);
        SetSFXSetting(SaveData.Instance.SFX);
    }//Start() end

    public void StartGame()
    {
        if(BGSource.isPlaying)
            return;
        BGSource.clip = BGMusic;
        BGSource.loop = true;
        BGSource.Play();
    }//StartGame() end

    public void SetBGSetting(bool Toggle) => BGSource.mute  = !Toggle;

    public void SetSFXSetting(bool Toggle) => SFXSource.mute = !Toggle;

    public void GameEnd() => BGSource.Stop();

    public void PlaySFX(SFX sfx, float volume = 1f)
    {
        switch(sfx)
        {
            case SFX.Click:
                SFXSource.PlayOneShot(ClickClip, volume);
            break;
            case SFX.GameWin:
                SFXSource.PlayOneShot(GameWinClip, volume);
            break;
            case SFX.GameLose:
                SFXSource.PlayOneShot(GameLoseClip, volume);
            break;
            case SFX.Collect:
                SFXSource.PlayOneShot(CollectClip, volume);
            break;
            case SFX.Impact:
                SFXSource.PlayOneShot(ImpactClip, volume);
            break;
        }//switch end
    }//PlaySFX() end

}//class end