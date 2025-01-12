using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    public void Update()
    {
        transform.position = Input.mousePosition + new Vector3(80,20,0);    
    }
    
}
