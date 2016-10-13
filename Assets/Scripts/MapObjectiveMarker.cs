using UnityEngine;
using UnityEngine.UI;
using Framework.UI;
using System;

public class MapObjectiveMarker : UIDataView<ObjectiveMarker>, IMapElement
{
    [SerializeField] private Text _objectiveName;

    void Awake()
    {
        _objectiveName.text = "N/A";
    }

    public override void OnSetData(ObjectiveMarker data)
    {
        base.OnSetData(data);
        _objectiveName.text = data.ObjectiveName;
    }

    void IMapElement.Update(MiniMap map)
    {
        transform.position = map.GetPosition(Data.transform.position);
    }
}
