using UnityEngine;
using UnityEngine.InputSystem;

// I kinda want to try peak commands!! E to grab, hold to throw!! time will fill out gouge
public class PlayerInteractions : MonoBehaviour
{
    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public Transform holdPoint;
    private GameObject heldObject;
    private Rigidbody heldObjectRb;

    // now to throw object
    private bool isChargingThrow = false; // if player is holding E
    private float throwChargeTime = 0f; // the PEAK mechanic

    public float maxThrowForce = 20f; // limit of the throw distance
    public float chargeSpeed = 1f; // how fast meter fills

    // sound when throwing
    public AudioSource audioSource;
    public AudioClip throwSound;

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

                }

               

                return;
            }
        }
        crosshairUIScript.SetInteract(false);

        if (heldObject != null)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                /*heldObject.transform.SetParent(null);

                // Re-enable physics
                heldObjectRb.isKinematic = false;
                heldObjectRb.useGravity = true;

                heldObject = null;
                heldObjectRb = null;*/
                isChargingThrow = true;
                throwChargeTime = 0f;
            }
            if (Keyboard.current.qKey.isPressed)
            {
                isChargingThrow = true;
                throwChargeTime += Time.deltaTime * chargeSpeed;

                // Clamp to max 1.0 (normalized)
                throwChargeTime = Mathf.Clamp01(throwChargeTime);
            }
            if (Keyboard.current.qKey.wasReleasedThisFrame)
            {
                ThrowHeldObject();
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
    void ThrowHeldObject()
    {
        // Unparent object
        heldObject.transform.SetParent(null);

        // Re-enable physics
        heldObjectRb.isKinematic = false;
        heldObjectRb.useGravity = true;

        // Calculate force
        float finalForce = throwChargeTime * maxThrowForce;

        // Apply force in camera forward direction
        heldObjectRb.AddForce(playerCamera.transform.forward * finalForce, ForceMode.Impulse);

        if (audioSource != null && throwSound != null)
        {
            audioSource.PlayOneShot(throwSound);
        }

        // Reset states
        heldObject = null;
        heldObjectRb = null;
        isChargingThrow = false;
        throwChargeTime = 0f;
    }
}
