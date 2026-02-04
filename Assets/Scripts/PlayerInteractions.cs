using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                crosshairUIScript.SetInteract(true);
                /*

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    Button button = hit.collider.GetComponent<Button>();
                    button.Press();

                }
                */
                return;
            }
        }

        crosshairUIScript.SetInteract(false);
    }
}
