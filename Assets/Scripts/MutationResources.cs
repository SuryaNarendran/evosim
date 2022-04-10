using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MutationResources : Singleton<MutationResources>
{
    static string path = "MutationSettings/";

    Mutation[] basicMutationPrototypes;
    Mutation[] advancedMutationPrototypes;

    private void Awake()
    {
        basicMutationPrototypes = Resources.LoadAll<Mutation>(path + "Basic");
        advancedMutationPrototypes = Resources.LoadAll<Mutation>(path + "Advanced");
    }

    public static Mutation Get(string typeName)
    {
        Mutation soInstance;
        soInstance = Array.Find(Instance.basicMutationPrototypes, x => x.GetType().Name == typeName);

        if(soInstance == null)
            soInstance = Array.Find(Instance.advancedMutationPrototypes, x => x.GetType().Name == typeName);

        if (soInstance == null) return null;

        Mutation copy = Instantiate(soInstance);
        return copy;
    }

    public static Mutation GetRandom()
    {
        int index = UnityEngine.Random.Range(0, Instance.advancedMutationPrototypes.Length);
        Mutation soInstance = Instance.advancedMutationPrototypes[index];
        Mutation copy = Instantiate(soInstance);
        return copy;
    }
}
