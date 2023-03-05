using UnityEngine;
using System.Collections.Generic;

public class CubePool : Singleton<CubePool>
{
    [SerializeField] int _poolSize;
    [SerializeField] Cube cubePrefab = null;

    private Queue<Cube> cubesQueue = new Queue<Cube>();
    private List<Cube> occupiedCubes = new List<Cube>();

    public override void Init()
    {
        base.Init();
        MakePool();
    }

    private void MakePool()
    {
        for(int i=0 ; i<_poolSize ; i++)
        {
            Cube cube = Instantiate(cubePrefab.gameObject, transform).GetComponent<Cube>();
            cube.gameObject.SetActive(false);
            cubesQueue.Enqueue(cube);
        }
    }

    public Cube GetCube()
    {
        if(cubesQueue.Count == 0)
            MakePool();
        occupiedCubes.Add(cubesQueue.Dequeue());
        return occupiedCubes[occupiedCubes.Count - 1];
    }

    public void PutBackInQueue(Cube cube)
    {
        cube.ResetCube();
        cubesQueue.Enqueue(cube);
    }

    public void Restart()
    {
        for(int i=0 ; i<occupiedCubes.Count ; i++)
            PutBackInQueue(occupiedCubes[i]);
        occupiedCubes.Clear();
    }

}