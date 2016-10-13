using UnityEngine;
using Framework;

public class MiniMap : MonoBehaviour
{
    public GameObject WorldRoot;
    public GameObject WorldPlayer;
    public float MapScale;

    [SerializeField] private Transform _root;

    private GameObject _geometry;
    private Toggleable _toggleable;

    void Awake()
    {
        _toggleable = gameObject.GetOrAddComponent<Toggleable>();
        _toggleable.Showed += OnShow;
    }

    void OnShow()
    {
        Generate();
    }

    void Generate()
    {
        if (_geometry != null)
            Destroy(_geometry);

        var layer = LayerMask.NameToLayer("MiniMap");

        _geometry = Instantiate(WorldRoot);
        _geometry.layer = layer;
        _geometry.transform.SetParent(_root, false);

        foreach (Transform t in _root.transform)
        {
            t.gameObject.SetLayers(layer);
        }
    }

    void Update()
    {
        transform.position = WorldPlayer.transform.position;

        if (_geometry != null)
        {
            _geometry.transform.localScale = Vector3.one * MapScale;
            _geometry.transform.position = transform.position - WorldPlayer.transform.position * _geometry.transform.localScale.x;
        }
    }
}
