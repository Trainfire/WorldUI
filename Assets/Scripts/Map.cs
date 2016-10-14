using UnityEngine;
using Framework;
using System;

public interface IMapElement
{
    void Update(Map map);
}

public class Map : MonoBehaviour
{
    public event Action<Map> Shown;
    public event Action<Map> Hidden;

    public GameObject WorldRoot;
    public GameObject WorldPlayer;
    public float MapScale;
    public Material MapMaterial;

    [SerializeField] private Transform _root;

    private GameObject _geometry;
    private Toggleable _toggleable;
    private MapObjectives _objectives;

    public Toggleable Toggleable { get { return _toggleable; } }

    void Awake()
    {
        _toggleable = gameObject.GetOrAddComponent<Toggleable>();
        _toggleable.ShowStarted += OnShow;
        _toggleable.HideFinished += OnHide;
    }

    void OnHide()
    {
        if (_geometry != null)
            Destroy(_geometry);

        Hidden.InvokeSafe(this);
    }

    void OnShow()
    {
        Generate();

        Shown.InvokeSafe(this);
    }

    void Generate()
    {
        var layer = LayerMask.NameToLayer("MiniMap");

        _geometry = Instantiate(WorldRoot);
        _geometry.layer = layer;
        _geometry.transform.SetParent(_root, false);

        foreach (Transform t in _root.transform)
        {
            t.gameObject.SetLayers(layer);
        }

        foreach (var t in _root.GetComponentsInChildren<Renderer>())
        {
            t.material = MapMaterial;
        }
    }

    void Update()
    {
        transform.position = WorldPlayer.transform.position;

        if (_geometry != null)
        {
            _geometry.transform.localScale = Vector3.one * MapScale;
            _geometry.transform.position = Origin;
        }
    }

    /// <summary>
    /// The local position of the world origin.
    /// </summary>
    public Vector3 Origin
    {
        get { return transform.position - WorldPlayer.transform.position * MapScale; }
    }

    public Vector3 PlayerPosition
    {
        get { return GetPosition(WorldPlayer.transform.position); }
    }

    /// <summary>
    /// Translates a world position to a map position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Vector3 GetPosition(Vector3 worldPosition)
    {
        return Origin + worldPosition * MapScale;
    }
}
