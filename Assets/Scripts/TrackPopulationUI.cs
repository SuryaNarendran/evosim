using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackPopulationUI : MonoBehaviour
{
    [SerializeField] TMP_Text value;

    void Awake()
    {
        OrganismPool.onPopulationChanged += UpdateValue;
    }

    private void UpdateValue(int population)
    {
        value.text = population.ToString();
    }
}
