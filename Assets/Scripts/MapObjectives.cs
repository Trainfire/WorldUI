using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Map))]
public class MapObjectives : MonoBehaviour
{
    [SerializeField] private ToggleableEventType _showTrigger;
    [SerializeField] private ToggleableEventType _hideTrigger;
    [SerializeField] private MapObjectiveMarker _prototype;

    private Map _miniMap;
    private List<MapObjectiveMarker> _markers;

    void Awake()
    {
        _markers = new List<MapObjectiveMarker>();
    }

    void Start()
    {
        _miniMap = GetComponent<Map>();
        _miniMap.Shown += OnMiniMapShow;
        _miniMap.Toggleable.Triggered += OnMiniMapTriggered;
    }

    void OnMiniMapTriggered(ToggleableEventType obj)
    {
        if (obj == _hideTrigger)
        {
            Debug.Log("Hide started...");
            _markers.ForEach(x => x.GetComponent<Toggleable>().Hide());
        }
        else if (obj == _showTrigger)
        {
            _markers.ForEach(x => x.GetComponent<Toggleable>().Show());
        }
    }

    void OnMiniMapShow(Map miniMap)
    {
        _markers.ForEach(x => Destroy(x.gameObject));
        _markers.Clear();

        // Find objective markers.
        var objectives = FindObjectsOfType<ObjectiveMarker>();

        foreach (var objective in objectives)
        {
            var view = Instantiate(_prototype);
            view.transform.localScale = Vector3.one;
            view.transform.position = miniMap.Origin + objective.transform.position * miniMap.MapScale;

            view.SetData(objective);

            _markers.Add(view);
        }
    }

    void Update()
    {
        _markers
            .Cast<IMapElement>()
            .ToList()
            .ForEach(x => x.Update(_miniMap));
    }
}
