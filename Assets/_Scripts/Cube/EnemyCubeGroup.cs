using UnityEngine;

public class EnemyCubeGroup : MonoBehaviour
{
    [SerializeField] Enemy[] _cubes;
    [SerializeField] int _collided = 0;

    private void Start()
    {
         _cubes = GetComponentsInChildren<Enemy>();
    }

    public void Restart()
    {
        _collided = 0;
        gameObject.SetActive(true);
        for(int i=0 ; i<_cubes.Length ; i++)
            _cubes[i].gameObject.SetActive(true);
    }

    public void CubesDestroyed()
    {
        _collided++;
        if(_collided == _cubes.Length)
        {
            AudioManager.Instance.Play("Impact");
            gameObject.SetActive(false);
        }
    }

}