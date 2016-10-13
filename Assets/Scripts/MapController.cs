using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{
    public Map Map;

	void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            Map.GetComponent<Toggleable>().Toggle();
    }
}
