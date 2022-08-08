//Shady
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class EnemyCube : MonoBehaviour
{
    private EnemyCubeGroup enemyCubeGroup;

    private void Start() => enemyCubeGroup = GetComponentInParent<EnemyCubeGroup>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
            {
                ParticleManager.Instance?.PlayParticle("Destroy", transform.position);
                enemyCubeGroup.CubeDestroyed();
                gameObject.SetActive(false);
            }//if end
            else
                GameManager.Instance.LevelLose();
        }//else if end
    }//OntriggerEnter() end

}//class end