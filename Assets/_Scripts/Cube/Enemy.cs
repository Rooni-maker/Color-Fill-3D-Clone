using UnityEngine;

public class Enemy : MonoBehaviour
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
                ActivateParticle();
                enemyCubeGroup.CubesDestroyed();
                transform.GetComponent<Collider>().enabled = false;
                GetDestroyed();
            }
            else
                GameManager.Instance.LevelLose();
        }
    }

    private void ActivateParticle()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void GetDestroyed()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

}