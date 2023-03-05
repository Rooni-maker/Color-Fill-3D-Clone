using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField] private EnemyCubeGroup enemyCubeGroup;

    private void Start()
    {
        enemyCubeGroup = GetComponentInParent<EnemyCubeGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
            {
                ParticleManager.Instance?.PlayParticle("Destroy", transform.position);
                enemyCubeGroup.CubeDestroyed();
                gameObject.SetActive(false);
            }
            else
                GameManager.Instance.LevelLose();
        }
    }

}