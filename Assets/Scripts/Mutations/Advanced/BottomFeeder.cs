using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BottomFeeder")]
public class BottomFeeder : Mutation
{
    [SerializeField] float movementSpeedFactor;
    [SerializeField] float nutrientEfficiencyFactor;
    [SerializeField] Color color;

    protected override void Setup()
    {
        myOrganism.movementSpeed *= movementSpeedFactor;
        myOrganism.nutrientEfficiency *= nutrientEfficiencyFactor;
        myOrganism.color += color;
    }
}
