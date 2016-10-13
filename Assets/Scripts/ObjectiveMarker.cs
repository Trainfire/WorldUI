using UnityEngine;
using System.Collections;

public class ObjectiveMarker : MonoBehaviour
{
    [SerializeField] private string _objectiveName;
    public string ObjectiveName { get { return _objectiveName; } }

    [SerializeField] private int _id;
    public int ID { get { return _id; } }
}
