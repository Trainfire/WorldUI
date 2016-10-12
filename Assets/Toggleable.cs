using UnityEngine;
using System;
using Framework;
using Framework.Animation;

/// <summary>
/// Allows a GameObject to be toggled on and off with callbacks and optional animations.
/// </summary>
public class Toggleable : MonoBehaviour
{
    public event Action Showed;
    public event Action Hidden;

    [SerializeField] private bool _autoDisable;
    [SerializeField] private AnimationGroup _showAnimations;
    [SerializeField] private AnimationGroup _hideAnimations;

    private bool _isShowing;

    void Awake()
    {
        if (_hideAnimations != null)
            _hideAnimations.Completed += OnHideAnimationsComplete;
    }

    void Start()
    {
        Set(gameObject.activeSelf);
    }

    public virtual void Show()
    {
        _isShowing = true;

        if (_autoDisable)
            gameObject.SetActive(true);

        if (_hideAnimations != null)
            _hideAnimations.Stop();

        if (_showAnimations != null)
            _showAnimations.Play();

        Showed.InvokeSafe();
    }

    public virtual void Hide()
    {
        _isShowing = false;

        if (_showAnimations != null)
            _showAnimations.Stop();

        if (_hideAnimations != null)
        {
            _hideAnimations.Play();
        }
        else
        {
            FinishHide();
        }
    }

    public void Set(bool show)
    {
        _isShowing = show;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Toggle()
    {
        if (_isShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void OnHideAnimationsComplete(AnimationGroup obj)
    {
        FinishHide();
    }

    void FinishHide()
    {
        if (_autoDisable)
            gameObject.SetActive(false);

        Hidden.InvokeSafe();
    }
}
