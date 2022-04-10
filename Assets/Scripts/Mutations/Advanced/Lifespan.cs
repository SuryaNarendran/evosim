using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lifespan")]
public class Lifespan : Mutation
{
    [SerializeField] float lifespanExtensionFactor;
    [SerializeField] Color color;
    [SerializeField] Color decreaseColor;

    protected override void Setup()
    {
        bool decreaseLifespan = Random.value > 0.5f;
        if (decreaseLifespan)
        {
            lifespanExtensionFactor = 1f / lifespanExtensionFactor;
            color = decreaseColor;
        }

        myOrganism.maxSize *= lifespanExtensionFactor;
        myOrganism.color += color;
    }
}