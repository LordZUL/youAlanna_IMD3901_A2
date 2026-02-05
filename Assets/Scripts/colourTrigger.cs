using UnityEngine;

public class colourTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is interactable
        if (other.CompareTag("Interactable"))
        {
            objectRenderer.material.color = Color.green;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            objectRenderer.material.color = Color.red;
        }
    }
}
