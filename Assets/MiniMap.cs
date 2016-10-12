using UnityEngine;
using Framework;

public class MiniMap : MonoBehaviour
{
    public GameObject WorldRoot;
    public GameObject WorldPlayer;
    public float MapScale;

    private GameObject _mapRoot;
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
        if (_mapRoot != null)
            Destroy(_mapRoot);

        var layer = LayerMask.NameToLayer("MiniMap");

        _mapRoot = Instantiate(WorldRoot);
        _mapRoot.layer = layer;
        _mapRoot.transform.SetParent(transform, false);

        foreach (Transform t in _mapRoot.transform)
        {
            t.gameObject.SetLayers(layer);
        }
    }

    void Update()
    {
        transform.position = WorldPlayer.transform.position;

        if (_mapRoot != null)
        {
            _mapRoot.transform.localScale = Vector3.one * MapScale;
            _mapRoot.transform.position = transform.position - WorldPlayer.transform.position * _mapRoot.transform.localScale.x;
        }
    }
}
