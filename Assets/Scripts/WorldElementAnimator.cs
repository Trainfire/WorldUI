using UnityEngine;
using System;
using System.Collections.Generic;
using Framework;
using Framework.Animation;

[RequireComponent(typeof(Toggleable))]
class WorldElementAnimator : MonoBehaviour
{
    public event Action<WorldElementAnimator> ShowComplete;
    public event Action<WorldElementAnimator> HideComplete;

    [SerializeField]
    private List<AnimationBase> _showAnimations;

    [SerializeField]
    private List<AnimationBase> _hideAnimations;

    private Toggleable _worldElement;

    void Start()
    {
        _worldElement = GetComponent<Toggleable>();
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
