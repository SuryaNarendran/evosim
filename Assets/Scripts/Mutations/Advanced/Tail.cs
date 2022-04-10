using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tail")]
public class Tail : Mutation
{
    [SerializeField] float movementSpeedFactor;
    [SerializeField] float decayRateFactor;
    [SerializeField] Color color;

    protected override void Setup()
    {
        myOrganism.movementSpeed *= movementSpeedFactor;
        myOrganism.baseMovementCost *= decayRateFactor;
        myOrganism.color += color;
    }
}
