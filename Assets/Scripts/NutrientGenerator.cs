using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientGenerator : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] int count;
    [SerializeField] GameObject nutrientPrefab;
    [SerializeField] float initialNumber;

    private void Start()
    {
        PoolManager.WarmPool(nutrientPrefab, 100);
        InitialSeed();
        StartCoroutine(GenerateNutrient());
    }

    private IEnumerator GenerateNutrient()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < count; i++)
            {
                GameObject nut = PoolManager.SpawnObject(nutrientPrefab, Boundaries.GetPointInBounds(), Quaternion.identity);
                nut.transform.parent = transform;
            }
            
        }
    }

    private void InitialSeed()
    {
        for(int i = 0; i < initialNumber; i++)
        {
            PoolManager.SpawnObject(nutrientPrefab, Boundaries.GetPointInBounds(), Quaternion.identity);
        }
    }
}
