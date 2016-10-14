using UnityEngine;
using System.Collections.Generic;

public enum ToggleableRelayAction
{
    Show,
    Hide
}

[RequireComponent(typeof(Toggleable))]
public class ToggleableRelay : MonoBehaviour
{
    [SerializeField] private ToggleableEventType _trigger;
    [SerializeField] private ToggleableRelayAction _action;
    [SerializeField] private List<Toggleable> _targets;

    private Toggleable _relay;

    void Awake()
    {
        _relay = GetComponent<Toggleable>();
        _relay.Triggered += OnTrigger;

        if (_targets == null)
            _targets = new List<Toggleable>();
    }

    void OnTrigger(ToggleableEventType eventType)
    {
        Debug.Log("Triggered: " + eventType);

        if (eventType == _trigger)
        {
            Debug.Log("Triggering action...");

            if (_action == ToggleableRelayAction.Show)
            {
                _targets.ForEach(x => x.Show());
            }
            else
            {
                _targets.ForEach(x => x.Hide());
            }
        }
    }
}
