using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
    public float value = 10f;

    public void OnEaten()
    {
        PoolManager.ReleaseObject(gameObject);
    }
}
