using UnityEngine;
using System.Collections;

public class ObjectiveMarker : MonoBehaviour
{
    [SerializeField] private string _objectiveName;
    public string ObjectiveName { get { return _objectiveName; } }
}
