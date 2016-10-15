using UnityEngine;
using UnityEngine.UI;
using Framework.UI;
using System;

public class MapObjectiveMarker : UIDataView<ObjectiveMarker>, IMapElement
{
    [SerializeField] private Text _objectiveName;
    [SerializeField] private Text _objectiveId;

    public override void OnSetData(ObjectiveMarker data)
    {
        base.OnSetData(data);
        _objectiveName.text = data.ObjectiveName;
        _objectiveId.text = data.ID.ToString("D2");
    }

    void IMapElement.Update(Map map)
    {
        transform.position = map.GetPosition(Data.transform.position);
    }
}
