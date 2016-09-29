using UnityEngine;
using Framework.UI;
using System.Collections.Generic;

[RequireComponent(typeof(WorldElement))]
class WorldElementAnimator : MonoBehaviour
{
    [SerializeField]
    private List<UIAnimation> _showAnimations;

    [SerializeField]
    private List<UIAnimation> _hideAnimations;

    private WorldElement _worldElement;

    void Start()
    {
        _worldElement = GetComponent<WorldElement>();
        _worldElement.Showed += OnShow;
        _worldElement.Hidden += OnHide;
    }

    void OnShow()
    {
        _hideAnimations.ForEach(x => x.Stop());
        _showAnimations.ForEach(x => x.Play());
    }

    void OnHide()
    {
        _showAnimations.ForEach(x => x.Stop());
        _hideAnimations.ForEach(x => x.Play());
    }
}
