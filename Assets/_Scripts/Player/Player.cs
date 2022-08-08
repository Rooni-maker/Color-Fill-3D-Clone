//Shady
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[HideMonoScript]
public class Player : MonoBehaviour
{
    [Title("PLAYER", null, titleAlignment: TitleAlignments.Centered)]
    [DisplayAsString]
    [SerializeField] bool _spawnCubes = false;
    [SerializeField] List<Cube> spawnedCubes = new List<Cube>();

    private PlayerMovement _playerMovement = null;

    public bool SpawnCubes{get => _spawnCubes; set => _spawnCubes = value;}

    public void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        spawnedCubes = new List<Cube>();
        MakeCube();
    }//Start() end

    private Vector3 RoundPos() => new Vector3((float)Mathf.Round(transform.position.x), transform.position.y, (float)Mathf.Round(transform.position.z));

    public void SpawnCube()
    {
        if(!_spawnCubes)
            return;
        MakeCube();
    }//SpawnCube() end

    private void MakeCube()
    {
        spawnedCubes.Add(CubePool.Instance.GetCube());
        spawnedCubes[spawnedCubes.Count - 1].Init(new Vector3((float)Mathf.Round(transform.position.x), transform.position.y - 0.7f, (float)Mathf.Round(transform.position.z)));
    }//MakeCube() end

    private void FillCubes()
    {
        for(int i=0 ; i<spawnedCubes.Count ; i++)
            spawnedCubes[i].FillCube();
        spawnedCubes.Clear();
    }//FillCubes() end

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boundary"))
        {
            _playerMovement.IsMoving = false;
            transform.DOMove(RoundPos(), 0.1f);
            if(_spawnCubes)
            {
                SpawnCube();
                _spawnCubes = false;
                FillCubes();
                GridManager.Instance.PerformFloodFill();
            }//if end
        }//if end
    }//OnCollisionEnter() end

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
            {
                FillCubes();
                if(_spawnCubes)
                    GridManager.Instance.PerformFloodFill();
            }//if end
            else
            {
                if(cube.CanHarm)
                {
                    Haptics.Generate(HapticTypes.HeavyImpact);
                    GameManager.Instance.LevelLose();
                }//if end
            }//else end
        }//if end
        else if(other.TryGetComponent<EnemyCube>(out EnemyCube enemyCube))
        {
            _playerMovement.IsMoving = false;
            transform.position = RoundPos();
            GameManager.Instance.LevelLose();
        }//else if end
    }//OnTriggerEnter() end

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
                _spawnCubes = false;
        }//if end
    }//OnTriggerStay() end

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
                _spawnCubes = true;
            else
                cube.CanHarm = true;
        }//if end
    }//OnTriggerExit() end

}//class end