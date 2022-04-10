using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieBasic")]
public class DieBasic : Mutation
{
    [SerializeField] float deathSize;

    protected override void Setup()
    {
        myOrganism.minSize = deathSize;
        myOrganism.deathDefault = Death;
    }

    private void Death()
    {
        OrganismPool.KillOrganism(myOrganism);
    }
}
