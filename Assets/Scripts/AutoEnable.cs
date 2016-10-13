using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoEnable : MonoBehaviour
{
    public float Delay;
    public List<GameObject> Targets;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);
        Targets.ForEach(x => x.SetActive(true));
    }
}
