using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutateBasic")]
public class MutateBasic : Mutation
{
    [SerializeField] float mutationRate;

    protected override void Setup()
    {
        myOrganism.mutationRate = mutationRate;
        myOrganism.mutateDefault = Mutate;
    }

    private void Mutate()
    {
        if(myOrganism.mutationRate > Random.Range(0f, 1f))
        {
            myOrganism.AddMutation();
        }

    }
}
