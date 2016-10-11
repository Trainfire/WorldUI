using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Framework;

public class MiniMapAnimator : MonoBehaviour
{
    void OnEnable()
    {
        foreach (var anim in GetComponents<MiniMapAnimation>())
        {
            anim.Show();
        }
    }
}
