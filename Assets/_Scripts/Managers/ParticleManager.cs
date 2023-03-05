using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable 0414

[System.Serializable]
public class ParticlePool
{
    [Tooltip("If true Pool will be hidden in Hierarchy.")]
    public bool HideInHierarchy = true;

    [Tooltip("From this name the Particle will be called.")]
    public string ParticleName = "Name";

    [Tooltip("Size of Pool to make.")]
    public int        PoolSize       = 10;
   
    [Tooltip("Actual Prefab that will be Instantiated to make a pool.")]
    public GameObject ParticlePrefab = null;

    [Tooltip("If true the actual Parent and Queue will be avialable to see in Inspector.")]
    [SerializeField] bool Debug      = false;
    
    [Tooltip("Parent of Pool will be made at runtime.")]
    public GameObject PoolParent = null;

    [Tooltip("Queue will be instantiated at runtime.")]
    public Queue<GameObject> ParticleQueue = new Queue<GameObject>();
}
public class ParticleManager : DontDestroySingleton<ParticleManager>
{
    [SerializeField] List<ParticlePool> Pools = new List<ParticlePool>();

    public override void Init()
    {
        base.Init();
        for(int i=0 ; i<Pools.Count ; i++)
            MakePool(Pools[i].ParticleName, Pools[i]);
    }//Start() end

    private void MakePool(string ParentName, ParticlePool Pool)
    {
        if(!Pool.ParticlePrefab)
            return;
        Pool.PoolParent = new GameObject(Pool.ParticleName);
        Pool.PoolParent.transform.SetParent(transform);
        if(Pool.HideInHierarchy)
            Pool.PoolParent.gameObject.hideFlags = HideFlags.HideInHierarchy;
        for(int i=0 ; i<Pool.PoolSize ; i++)
        {
            GameObject Temp = Instantiate(Pool.ParticlePrefab, Pool.PoolParent.transform);
            Temp.SetActive(false);
            Pool.ParticleQueue.Enqueue(Temp);
        }
    }

    public void PlayParticle(string ParticleName, Vector3 Pos)
    {
        Play(Pools.Find(pool => pool.ParticleName.Equals(ParticleName)), Pos);
    }
    private void Play(ParticlePool Pool, Vector3 Pos)
    {
        if(Pool.ParticleQueue.Count > 0)
        {
            ParticleSystem PS = Pool.ParticleQueue.Dequeue().GetComponent<ParticleSystem>();
            PS.transform.position = Pos;
            PS.gameObject.SetActive(true);
            StartCoroutine(PutBackInQueue(Pool, PS.gameObject, PS.main.duration));
            PS.Play();
        }
    }

    public void PlayParticle(string ParticleName, Transform Parent)
    {
        Play(Pools.Find(pool => pool.ParticleName.Equals(ParticleName)), Parent);
    }
    private void Play(ParticlePool Pool, Transform Parent)
    {
        if(Pool.ParticleQueue.Count > 0)
        {
            ParticleSystem PS = Pool.ParticleQueue.Dequeue().GetComponent<ParticleSystem>();
            PS.transform.SetParent(Parent);
            PS.transform.ResetLocalPos();
            PS.gameObject.SetActive(true);
            StartCoroutine(PutBackInQueue(Pool, PS.gameObject, PS.main.duration));
            PS.Play();
        }
    }

    private IEnumerator PutBackInQueue(ParticlePool Pool, GameObject Particle, float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Particle.SetActive(false);
        Pool.ParticleQueue.Enqueue(Particle);
    }

}