using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{
    public Map Map;

	void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            Map.GetComponent<Toggleable>().Toggle();

        if (Input.GetKeyUp(KeyCode.Tab))
            Time.timeScale = Time.timeScale > 0.1f ? 0.1f : 1f;
    }
}
