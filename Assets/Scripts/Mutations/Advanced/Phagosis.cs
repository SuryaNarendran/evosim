using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Phagosis")]
public class Phagosis : Mutation
{
    [SerializeField] float efficiency;
    [SerializeField] float minimumSizeFactor;
    [SerializeField] float nutrientEfficiencyFactor;
    [SerializeField] float nutrientAbsorptionRateFactor;
    [SerializeField] Color color;

    const float phagosisDelay = 2f;

    protected override void Setup()
    {
        myOrganism.nutrientAbsorptionRate *= nutrientAbsorptionRateFactor;
        myOrganism.nutrientEfficiency = nutrientEfficiencyFactor;
        myOrganism.phagosisEfficiency = efficiency;
        myOrganism.onOrganismContact += Devour;
        myOrganism.phagosisSizeFactor = minimumSizeFactor;
        myOrganism.color += color;
    }

    private void Devour(Organism other)
    {
        if(myOrganism.size * myOrganism.phagosisSizeFactor >= other.size && myOrganism.canEat)
        {
            myOrganism.AddSize(other.size * myOrganism.phagosisEfficiency);
            other.Kill();
            myOrganism.AddEatRestriction(this);
            Timer.Register(phagosisDelay, () => myOrganism.RemoveEatRestriction(this));
        }
    }
}
