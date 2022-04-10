using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrganismPool : Singleton<OrganismPool>
{
    [SerializeField] GameObject organismPrefab;
    [SerializeField] int initialSpawn;
    public static System.Action<int> onPopulationChanged;

    private int _population = 0;
    public int population
    {
        get => _population;
        private set
        {
            _population = value;
            onPopulationChanged?.Invoke(_population);
        }
    }

    private void Start()
    {
        //PoolManager.WarmPool(organismPrefab, 1);
        InitialSpawn();
    }

    private void InitialSpawn()
    {
        for(int i=0;i <initialSpawn; i++)
        {
            //PoolManager.SpawnObject(Instance.organismPrefab, Boundaries.GetPointInBounds(), Quaternion.identity);
            Instantiate(Instance.organismPrefab, Boundaries.GetPointInBounds(), Quaternion.identity);
        }

        Instance.population += initialSpawn;
    }

    public static Organism GetCopy(Organism o)
    {
        //GameObject copyGO = PoolManager.SpawnObject(Instance.organismPrefab, o.transform.position, o.transform.rotation);
        GameObject copyGO = Instantiate(Instance.organismPrefab, o.transform.position, o.transform.rotation);
        copyGO.transform.parent = Instance.transform;
        Organism copy = copyGO.GetComponent<Organism>();

        foreach(Mutation m in o.mutations)
        {
            copy.AddMutation(m.GetType().Name);
        }

        Instance.population++;

        return copy;
    }

    public static void KillOrganism(Organism o)
    {
        Instance.population--;
        //PoolManager.ReleaseObject(o.gameObject);
        Destroy(o.gameObject);
    }
}
