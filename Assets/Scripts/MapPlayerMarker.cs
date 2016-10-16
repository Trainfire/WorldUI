using UnityEngine;
using UnityEngine.UI;
using Framework.UI;

public class MapPlayerMarker : UIDataView<Player>, IMapElement
{
    [SerializeField]
    private Text _playerName;

    public override void OnSetData(Player player)
    {
        base.OnSetData(player);
        _playerName.text = player.Data.Name;
    }

    void IMapElement.Update(Map map)
    {
        transform.position = map.GetPosition(Data.transform.position);
    }
}
