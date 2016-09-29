using UnityEngine;

/// <summary>
/// Helper to retrieve information about a hit surface on a BoxCollider.
/// </summary>
public class SurfaceHelper
{
    private RaycastHit _hit;
    private BoxCollider _collider;

    public SurfaceHelper(RaycastHit hit)
    {
        _hit = hit;
        _collider = _hit.transform.GetComponent<BoxCollider>();
    }

    public Vector3 Top { get { return Center + _hit.transform.up * (Height / 2f); } }
    public Vector3 Right { get { return Center + Cross * (Width / 2f); } }
    public Vector3 Bottom { get { return Center + _hit.transform.up * (-Height / 2f); } }
    public Vector3 Left { get { return Center + Cross * -(Width / 2f); } }
    public Vector3 Cross { get { return _hit.collider == null ? Vector3.zero : Vector3.Cross(_hit.normal, _hit.transform.up); } }
    public Vector3 Normal { get { return _hit.collider != null ? _hit.normal : Vector3.zero; } }
    public Vector3 Center { get { return _hit.collider.bounds.center + (_hit.normal * Depth) / 2f; } }

    /// <summary>
    /// Returns the Depth of object relative to the normal of the surface.
    /// </summary>
    public float Depth
    {
        get
        {
            if (_hit.collider != null)
            {
                if (AxisAligned(_hit.transform.up))
                    return _collider.size.y * _collider.transform.lossyScale.y;

                if (AxisAligned(_hit.transform.right))
                    return _collider.size.x * _collider.transform.lossyScale.x;

                if (AxisAligned(_hit.transform.forward))
                    return _collider.size.z * _collider.transform.lossyScale.z;
            }

            return 0f;
        }
    }

    public float Width
    {
        get
        {
            if (_hit.collider != null)
            {
                if (AxisAligned(_hit.transform.up) || AxisAligned(_hit.transform.forward))
                    return _collider.size.x * _collider.transform.lossyScale.x;

                if (AxisAligned(_hit.transform.right))
                    return _collider.size.z * _collider.transform.lossyScale.z;
            }

            return 0f;
        }
    }

    public float Height
    {
        get
        {
            if (_hit.collider != null)
            {
                if (AxisAligned(_hit.transform.up))
                    return _collider.size.z * _collider.transform.lossyScale.z;

                if (AxisAligned(_hit.transform.right) || AxisAligned(_hit.transform.forward))
                    return _collider.size.y * _collider.transform.lossyScale.y;
            }

            return 0f;
        }
    }

    bool AxisAligned(Vector3 localAxis)
    {
        var dot = Vector3.Dot(_hit.normal, localAxis);
        return Mathf.Abs(dot) > 0.5f;
    }
}
