using UnityEngine;
using UnityEngine.UI;

public class UISliderField : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Slider Slider { get { return _slider; } }

    [SerializeField]
    private Text _value;

    public void Set(float value)
    {
        _slider.value = value;
        _value.text = value.ToString("F2");
    }
}
