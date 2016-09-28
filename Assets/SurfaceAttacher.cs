using UnityEngine;
using System.Collections;

public class SurfaceAttacher : MonoBehaviour
{
    public GameObject Affector;
    public float SurfaceWidth;

    private bool _positionValid;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (hit.collider != null)
            {
                var top = hit.collider.bounds.center + Vector3.up * hit.collider.bounds.extents.y;
                var topEdge = new Vector3(hit.point.x, top.y, hit.point.z);

                var perpDirection = Vector3.Cross(hit.normal, hit.transform.up);

                var left = hit.point - perpDirection * SurfaceWidth / 2f;
                var right = hit.point + perpDirection * SurfaceWidth / 2f;

                // lmao
                if (Approximate(hit.normal, transform.right))
                {
                    var leftEdge = hit.collider.bounds.center - perpDirection * hit.collider.bounds.extents.z;
                    var rightEdge = hit.collider.bounds.center - -perpDirection * hit.collider.bounds.extents.z;

                    Debug.DrawLine(leftEdge, leftEdge + Vector3.up * 1f, Color.blue);
                    Debug.DrawLine(rightEdge, rightEdge + Vector3.up * 1f, Color.blue);
                }
                else if (Approximate(hit.normal, -transform.forward))
                {
                    var leftEdge = hit.collider.bounds.center - perpDirection * hit.collider.bounds.extents.x;
                    var rightEdge = hit.collider.bounds.center - -perpDirection * hit.collider.bounds.extents.x;

                    Debug.DrawLine(leftEdge, leftEdge + Vector3.up * 1f, Color.blue);
                    Debug.DrawLine(rightEdge, rightEdge + Vector3.up * 1f, Color.blue);
                }

                _positionValid = hit.collider.bounds.Contains(left) && hit.collider.bounds.Contains(right);

                Debug.DrawLine(left, right, Color.gray);
                Debug.DrawLine(hit.point, hit.point + hit.normal * 1f, Color.black);
                Debug.DrawLine(topEdge, topEdge + hit.normal * 1f, Color.green);

                if (_positionValid)
                {
                    Affector.transform.position = topEdge;
                    Affector.transform.rotation = Quaternion.LookRotation(-hit.normal);
                }
            }
        }
    }

    bool Approximate(Vector3 v1, Vector3 v2)
    {
        //return Vector3.Dot(v1, v2) < 0.1f;
        if ((v1 - v2).sqrMagnitude <= (v1 * 1f).sqrMagnitude)
        {
            return true;
        }
        return false;
    }
}
