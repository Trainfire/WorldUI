using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Map Map;
    public MapObjectives MapObjectives;
    public MapPlayers MapPlayers;
    public UISliderField SliderField;
    public Toggle ToggleObjectives;
    public Toggle TogglePlayers;

    void Start()
    {
        SliderField.Set(Map.MapScale);
        SliderField.Slider.onValueChanged.AddListener(OnScaleSliderValueChanged);

        ToggleObjectives.isOn = MapObjectives.enabled;
        ToggleObjectives.onValueChanged.AddListener(OnToggleObjectives);

        TogglePlayers.isOn = MapPlayers.enabled;
        TogglePlayers.onValueChanged.AddListener(OnTogglePlayers);
    }
    
    void OnScaleSliderValueChanged(float arg)
    {
        Map.MapScale = arg;
        SliderField.Set(arg);
    }

    void OnToggleObjectives(bool arg)
    {
        MapObjectives.enabled = arg;
    }

    void OnTogglePlayers(bool arg)
    {
        MapPlayers.enabled = arg;
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            Map.GetComponent<Toggleable>().Toggle();

        if (Input.GetKeyUp(KeyCode.Tab))
            Time.timeScale = Time.timeScale > 0.1f ? 0.1f : 1f;
    }
}
