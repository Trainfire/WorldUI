using UnityEngine;
using System.Collections;

public class SurfaceAttacher : MonoBehaviour
{
    [SerializeField] private WorldElement _affector;
    [SerializeField] private float _surfaceWidth;

    private bool _positionValid;

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
            _affector.Show();

        if (Input.GetMouseButtonUp(1))
            _affector.Hide();

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (hit.collider != null)
            {
                // Get top point of collider.
                var top = hit.collider.bounds.center + Vector3.up * hit.collider.bounds.extents.y;

                // Get top edge of surface.
                var topEdge = new Vector3(hit.point.x, top.y, hit.point.z);

                // Get a right-facing direction parallel to the surface.
                var perpDirection = Vector3.Cross(hit.normal, hit.transform.up);

                // Check if the left and right points are within the width of the surface.
                var left = hit.point - perpDirection * _surfaceWidth / 2f;
                var right = hit.point + perpDirection * _surfaceWidth / 2f;

                // Don't update if the hit normal is up or down.
                bool isUp = Vector3.Dot(hit.normal, Vector3.up) > 0f;

                _positionValid = hit.collider.bounds.Contains(left) && hit.collider.bounds.Contains(right) && !isUp;

                Debug.DrawLine(left, right, Color.gray);
                Debug.DrawLine(hit.point, hit.point + hit.normal * 1f, Color.black);
                Debug.DrawLine(topEdge, topEdge + hit.normal * 1f, Color.green);

                if (_positionValid)
                {
                    _affector.transform.position = topEdge;
                    _affector.transform.rotation = Quaternion.LookRotation(-hit.normal);
                }
            }
        }
    }
}
