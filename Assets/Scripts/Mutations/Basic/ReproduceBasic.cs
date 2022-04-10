using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "ReproduceBasic")]
public class ReproduceBasic : Mutation
{
    [SerializeField] float reproductionSize;

    const float eatDelay = 2f;

    protected override void Setup()
    {
        myOrganism.reproductionDefault = Reproduce;
        myOrganism.reproductionNumber = 2;
        myOrganism.maxSize = reproductionSize;
    }

    private void Reproduce()
    { 
        myOrganism.SetSize(myOrganism.size / myOrganism.reproductionNumber);
        for(int i = 0; i < myOrganism.reproductionNumber - 1; i++)
        {
            Organism sister = OrganismPool.GetCopy(myOrganism);
            sister.SetSize(myOrganism.size);
            Vector3 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            //myOrganism.StartCoroutine(SeparationForce(myOrganism, 0.4f, myOrganism.size/Organism.baseScale, randomDirection));
            //sister.StartCoroutine(SeparationForce(sister, 0.4f, sister.size/Organism.baseScale, -randomDirection));

            myOrganism.AddEatRestriction(this);
            Timer.Register(eatDelay, () => myOrganism.RemoveEatRestriction(this));


        }
    }

    private IEnumerator SeparationForce(Organism o, float duration, float distance, Vector2 direction)
    {
        float time = 0;
        while(time < duration)
        {
            o.transform.Translate(direction * distance * Time.fixedDeltaTime / duration);
            time += Time.fixedDeltaTime;
            yield return null;
        }
    }
}
