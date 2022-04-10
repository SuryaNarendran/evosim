using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutation : ScriptableObject
{
    public string displayName;
    public int priority;

    protected Organism myOrganism;

    public void AddToOrganism(Organism organism)
    {
        myOrganism = organism;
        Setup();
    }

    protected virtual void Setup()
    {

    }
}
