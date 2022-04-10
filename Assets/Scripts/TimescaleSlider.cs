using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescaleSlider : MonoBehaviour
{

    private void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSlide);
    }

    public void OnSlide(float value)
    {
        Time.timeScale = value;
    }
}
