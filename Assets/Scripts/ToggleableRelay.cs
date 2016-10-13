using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(ToggleableRelay))]
public class ToggleableRelay : MonoBehaviour
{
    [SerializeField] private List<Toggleable> _targets;
    private Toggleable _relay;


    void Awake()
    {
        _relay = GetComponent<Toggleable>();
        _relay.Showed += OnShow;
        _relay.Hidden += OnHide;

        if (_targets == null)
            _targets = new List<Toggleable>();
    }

    void OnShow()
    {
        _targets.ForEach(x => x.Show());
    }

    void OnHide()
    {
        _targets.ForEach(x => x.Hide());
    }
}
