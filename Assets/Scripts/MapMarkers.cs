using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Framework.UI;

/// <summary>
/// Binds data to a marker on a map.
/// </summary>
/// <typeparam name="TData">The type of object to search for.</typeparam>
/// <typeparam name="TView">The type of view to represent the object.</typeparam>
[RequireComponent(typeof(Map))]
public abstract class MapMarkers<TData, TView> : MonoBehaviour 
    where TData : MonoBehaviour 
    where TView : UIDataView<TData>
{
    [SerializeField] private ToggleableEventType _showTrigger;
    [SerializeField] private ToggleableEventType _hideTrigger;
    [SerializeField] private TView _prototype;

    private Map _miniMap;
    private List<TView> _markers;

    void Awake()
    {
        _markers = new List<TView>();
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

        var data = FindObjectsOfType<TData>();

        foreach (var item in data)
        {
            var view = Instantiate(_prototype);
            view.transform.localScale = Vector3.one;
            view.transform.position = miniMap.Origin + item.transform.position * miniMap.MapScale;

            view.SetData(item);

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

    void OnEnable()
    {
        _markers.ForEach(x => x.GetComponent<Toggleable>().Show());
    }

    void OnDisable()
    {
        _markers.ForEach(x => x.GetComponent<Toggleable>().Hide());
    }
}
