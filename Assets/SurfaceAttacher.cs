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
                var surface = new SurfaceHelper(hit);

                // Check if the left and right points are within the width of the surface.
                var left = hit.point - surface.Cross * _surfaceWidth / 2f;
                var right = hit.point + surface.Cross * _surfaceWidth / 2f;

                // Don't update if the hit normal is up or down.
                bool isUp = Vector3.Dot(hit.normal, Vector3.up) > 0f;

                _positionValid = hit.collider.bounds.Contains(left) && hit.collider.bounds.Contains(right) && !isUp;  

                Debug.DrawLine(left, right, Color.gray);
                Debug.DrawLine(hit.point, hit.point + hit.normal * 1f, Color.black);
                Debug.DrawLine(surface.Top, surface.Top + hit.normal * 1f, Color.green);

                Debug.DrawLine(surface.Top, surface.Top + hit.normal * 1f, Color.red);
                Debug.DrawLine(surface.Left, surface.Left + hit.normal * 1f, Color.red);
                Debug.DrawLine(surface.Right, surface.Right + hit.normal * 1f, Color.red);
                Debug.DrawLine(surface.Bottom, surface.Bottom + hit.normal * 1f, Color.red);
                Debug.DrawLine(surface.Center, surface.Center + hit.normal * 0.5f, Color.red);

                Debug.Log("Height: " + surface.Height);

                if (_positionValid)
                {
                    //return new Vector3(_hit.point.x, top.y, _hit.point.z);
                    _affector.transform.position = new Vector3(hit.point.x, surface.Top.y, hit.point.z);
                    _affector.transform.rotation = Quaternion.LookRotation(-hit.normal);
                }
            }
        }
    }
}
