using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatBasic")]
public class EatBasic : Mutation
{
    [SerializeField] float absorptionRate;
    [SerializeField] float valueEfficiency;

    Timer eatTimer;

    protected override void Setup()
    {
        myOrganism.eatDefault = Eat;
        myOrganism.nutrientAbsorptionRate = absorptionRate;
        myOrganism.nutrientEfficiency = valueEfficiency;
    }

    private void Eat(Nutrient nutrient)
    {
        if (myOrganism.canEat == false) return;

        myOrganism.AddSize(nutrient.value * myOrganism.nutrientEfficiency);
        nutrient.OnEaten();
        myOrganism.AddEatRestriction(this);
        Timer.Register(1f/myOrganism.nutrientAbsorptionRate, () => myOrganism.RemoveEatRestriction(this));
    }
}
