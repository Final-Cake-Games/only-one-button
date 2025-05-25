using UnityEngine;
using UnityEngine.UI;

public class InformationComputerUI : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;

    public void SetSliderMaxValue(float value)
    { 
        _timerSlider.maxValue = value;
    }

    public void SetSliderValue(float value)
    {
        _timerSlider.value = value;
    }
}
