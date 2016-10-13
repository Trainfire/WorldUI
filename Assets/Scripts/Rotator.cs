using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Vector3 Axis;
    public float Speed;

    void Update()
    {
        transform.Rotate(Axis, Speed * 1f);
    }
}
