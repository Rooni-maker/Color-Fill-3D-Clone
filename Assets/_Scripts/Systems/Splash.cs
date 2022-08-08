//Shady
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

[HideMonoScript]
public class Splash : MonoBehaviour
{
    [Title("SPLASH", null, titleAlignment: TitleAlignments.Centered)]
    [SerializeField] float delay = 1f;

    private async void Start()
    {
        await Tasks.Delay(delay);
        FadeSystem.Instance.Fade(()=>SceneManager.LoadScene(1));
    }//Start() end

}//class end