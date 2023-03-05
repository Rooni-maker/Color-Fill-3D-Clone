using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        FadeSystem.Instance.Fade(()=>SceneManager.LoadScene(1));
    }
}