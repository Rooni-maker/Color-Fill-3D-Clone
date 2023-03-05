using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class Player : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] bool spawnCubes = false;
    [SerializeField] List<Cube> spawnedCubes = new List<Cube>();
    public bool SpawnCubes{get => spawnCubes; set => spawnCubes = value;}

    public void Init()
    {
        _playerController = GetComponent<PlayerController>();
        spawnedCubes = new List<Cube>();
        MakeCube();
    }

    private Vector3 RoundPos() => new Vector3((float)Mathf.Round(transform.position.x), transform.position.y, (float)Mathf.Round(transform.position.z));

    public void SpawnCube()
    {
        if(!spawnCubes)
            return;
        MakeCube();
    }

    private void MakeCube()
    {
        spawnedCubes.Add(CubePool.Instance.GetCube());
        spawnedCubes[spawnedCubes.Count - 1].Init(new Vector3((float)Mathf.Round(transform.position.x), transform.position.y - 0.7f, (float)Mathf.Round(transform.position.z)));
    }

    private void FillCubes()
    {
        for(int i=0 ; i<spawnedCubes.Count ; i++)
            spawnedCubes[i].FillCube();
        spawnedCubes.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boundary"))
        {
            _playerController.IsMoving = false;
            transform.DOMove(RoundPos(), 0.1f);
            if(spawnCubes)
            {
                SpawnCube();
                spawnCubes = false;
                FillCubes();
                GridManager.Instance.ApplyFloodFill();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
            {
                FillCubes();
                if(spawnCubes)
                    GridManager.Instance.ApplyFloodFill();
            }
            else
            {
                if(cube.CanHarm)
                {
                    Haptics.Generate(HapticTypes.HeavyImpact);
                    GameManager.Instance.LevelLose();
                }
            }
        }
        else if(other.TryGetComponent<Enemy>(out Enemy enemyCube))
        {
            _playerController.IsMoving = false;
            transform.position = RoundPos();
            GameManager.Instance.LevelLose();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
                spawnCubes = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if(cube.IsFilled)
                spawnCubes = true;
            else
                cube.CanHarm = true;
        }
    }

}