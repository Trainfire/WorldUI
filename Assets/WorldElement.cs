using UnityEngine;
using System;
using Framework;

class WorldElement : MonoBehaviour
{
    public event Action Showed;
    public event Action Hidden;

    public virtual void Show()
    {
        Showed.InvokeSafe();
    }

    public virtual void Hide()
    {
        Hidden.InvokeSafe();
    }
}
