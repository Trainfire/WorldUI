using UnityEngine;

public class LookAt : MonoBehaviour
{
    public bool LookAtCamera;

    public Transform Target;

    void Update()
    {
        if (LookAtCamera)
            Target = Camera.main.transform;

        if (Target != null)
        {
            transform.LookAt(Target, Vector3.up);
            transform.eulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        }
    }
}
