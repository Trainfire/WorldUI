using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Map))]
public class MapPlayerMarker : MonoBehaviour
{
    [SerializeField] private Transform _playerMarker;

    private Map _miniMap;

    void Awake()
    {
        _miniMap = GetComponent<Map>();
    }

    void Update()
    {
        if (_playerMarker != null)
            _playerMarker.transform.position = _miniMap.PlayerPosition;
    }
}
