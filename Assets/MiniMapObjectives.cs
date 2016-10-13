using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MiniMap))]
public class MiniMapObjectives : MonoBehaviour
{
    [SerializeField] private MapObjectiveMarker _prototype;

    private MiniMap _miniMap;
    private List<MapObjectiveMarker> _markers;

    void Awake()
    {
        _miniMap = GetComponent<MiniMap>();
        _miniMap.Shown += OnMiniMapShow;
        _miniMap.Hidden += OnMiniMapHide;

        _markers = new List<MapObjectiveMarker>();
    }

    void OnMiniMapHide(MiniMap obj)
    {
        _markers.ForEach(x => Destroy(x.gameObject));
        _markers.Clear();
    }

    void OnMiniMapShow(MiniMap miniMap)
    {
        Debug.Log("Mini map shown...");

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
