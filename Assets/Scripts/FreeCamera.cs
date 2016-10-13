using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;

    [SerializeField]
    private float _moveSpeed;

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var hAxis = Input.GetAxis("Mouse X");
            transform.Rotate(transform.up, hAxis * _rotateSpeed);
        }

        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * _moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * _moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * _moveSpeed * Time.deltaTime;
    }
}
