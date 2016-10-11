using UnityEngine;

public abstract class MiniMapAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _enabled = true;

    public void Show()
    {
        if (_enabled)
            OnShow();
    }

    public void Hide()
    {
        if (_enabled)
            OnHide();
    }

    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
}
