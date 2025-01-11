using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject boxObject;
    private Box box;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxObject = Instantiate(boxObject, transform);
        box = boxObject.GetComponent<Box>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (box && boxObject.activeSelf && box.IsHovering())
            {
                string output = box.TakeOneItem();
                Debug.Log(output);
                
                // Should play the click sound here 
                
                
                if (!box.NextExists())
                {
                    // Hide the box
                    boxObject.SetActive(false);
                    box.Reset();
                }
            }
            else
            {
                boxObject.SetActive(true);
            }
        }
    }
}
