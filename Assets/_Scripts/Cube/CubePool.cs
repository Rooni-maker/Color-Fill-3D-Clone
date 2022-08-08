//Shady
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[HideMonoScript]
public class CubePool : Singleton<CubePool>
{
    [Title("CUBE POOL", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] int _poolSize = 100;
    [SerializeField] Cube _cubePrefab = null;

    private Queue<Cube> _cubeQueue = new Queue<Cube>();
    private List<Cube> TakenCubes = new List<Cube>();

    public override void Init()
    {
        base.Init();
        MakePool();
    }//Init() end

    private void MakePool()
    {
        for(int i=0 ; i<_poolSize ; i++)
        {
            Cube cube = Instantiate(_cubePrefab.gameObject, transform).GetComponent<Cube>();
            cube.gameObject.SetActive(false);
            _cubeQueue.Enqueue(cube);
        }//loop end
    }//MakePool() end

    public Cube GetCube()
    {
        if(_cubeQueue.Count == 0)
            MakePool();
        TakenCubes.Add(_cubeQueue.Dequeue());
        return TakenCubes[TakenCubes.Count - 1];
    }//GetCube() end

    public void PutBackInQueue(Cube cube)
    {
        cube.ResetCube();
        _cubeQueue.Enqueue(cube);
    }//PutBackInQueue() end

    public void Restart()
    {
        for(int i=0 ; i<TakenCubes.Count ; i++)
            PutBackInQueue(TakenCubes[i]);
        TakenCubes.Clear();
    }//Restart() end

}//class end