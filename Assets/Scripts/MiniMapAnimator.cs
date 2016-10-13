using UnityEngine;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public class MiniMapAnimator : MonoBehaviour
{
    public event Action<MiniMapAnimator> PlayOutComplete;

    private List<MiniMapAnimation> _playOutAnimations;

    void Awake()
    {
        _playOutAnimations = new List<MiniMapAnimation>();
    }

    public void PlayIn()
    {
        GetComponents<MiniMapAnimation>().ToList().ForEach(x => x.PlayIn());
    }

    public void PlayOut()
    {
        _playOutAnimations = GetComponents<MiniMapAnimation>().ToList();

        foreach (var anim in _playOutAnimations.ToList())
        {
            Debug.Log("Add " + anim.GetType());
            _playOutAnimations.Add(anim);

            anim.OnPlayOutComplete += OnAnimPlayOutCompleted;
            anim.PlayOut();
        }
    }

    private void OnAnimPlayOutCompleted(MiniMapAnimation obj)
    {
        Debug.Log("Anim " + obj.GetType() + " finished.");

        obj.OnPlayOutComplete -= OnAnimPlayOutCompleted;

        if (_playOutAnimations.Contains(obj))
            _playOutAnimations.Remove(obj);

        if (_playOutAnimations.Count == 0)
        {
            Debug.Log("Anim all animations completed");
            PlayOutComplete.InvokeSafe(this);
        }
    }
}
