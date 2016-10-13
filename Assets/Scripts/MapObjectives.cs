using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Map))]
public class MapObjectives : MonoBehaviour
{
    [SerializeField] private MapObjectiveMarker _prototype;

    private Map _miniMap;
    private List<MapObjectiveMarker> _markers;

    void Awake()
    {
        _miniMap = GetComponent<Map>();
        _miniMap.Shown += OnMiniMapShow;
        _miniMap.Hidden += OnMiniMapHide;

        _markers = new List<MapObjectiveMarker>();
    }

    void OnMiniMapHide(Map obj)
    {
        foreach (var marker in _markers)
        {
            var toggleable = marker.GetComponent<Toggleable>();
            if (toggleable)
                toggleable.Hide();
        }
    }

    void OnMiniMapShow(Map miniMap)
    {
        Debug.Log("Mini map shown...");

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
