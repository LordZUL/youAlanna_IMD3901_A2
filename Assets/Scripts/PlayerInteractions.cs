using UnityEngine;
using UnityEngine.InputSystem;

// I kinda want to try peak commands!! E to grab, hold to throw!! time will fill out gouge
public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public Transform holdPoint;
    //public KeyCode pickupKey = KeyCode.E;
    private GameObject heldObject;
    private Rigidbody heldObjectRb;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //bool isLookingAtInteractable = false;

        // if object in 5f, detect tag
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                crosshairUIScript.SetInteract(true);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (heldObject == null)
                    {
                        PickUpObject(hit.collider.gameObject);
                    }
                    else
                    {
                        // Unparent
                        heldObject.transform.SetParent(null);

                        // Re-enable physics
                        heldObjectRb.isKinematic = false;
                        heldObjectRb.useGravity = true;

                        heldObject = null;
                        heldObjectRb = null;
                    }

                }

               

                return;
            }
        }
        crosshairUIScript.SetInteract(false);
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject != null)
            {
                heldObject.transform.SetParent(null);

                // Re-enable physics
                heldObjectRb.isKinematic = false;
                heldObjectRb.useGravity = true;

                heldObject = null;
                heldObjectRb = null;
            }
        }





    }
    void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObjectRb = obj.GetComponent<Rigidbody>();

        // Disable physics
        heldObjectRb.isKinematic = true;
        heldObjectRb.useGravity = false;

        // Parent to hold point
        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }

}
