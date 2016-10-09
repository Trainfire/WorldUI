using UnityEngine;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour
{
    public GameObject WorldRoot;
    public GameObject WorldPlayer;
    public float MapScale;

    private GameObject _mapRoot;

    void OnEnable()
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

public static class GameObjectEx
{
    public static void SetLayers(this GameObject go, int layer)
    {
        foreach (Transform t in go.transform)
        {
            t.gameObject.layer = layer;
        }
    }
}
