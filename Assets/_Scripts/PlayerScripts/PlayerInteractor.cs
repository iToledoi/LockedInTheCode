using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scene Refs")]
    public GameObject firstPersonCamGO;      // CameraManager/FirstPersonCam
    public Transform holdPoint;              // FirstPersonCam/HoldPoint
    public Transform focusTransform;         // Player/Focus (or Orientation)
    public LayerMask interactMask;           // ONLY Interactable

    [Header("Settings")]
    public float fpDistance = 3f;
    public float tpDistance = 2.5f;
    public float tpAimRadius = 0.28f;
    public KeyCode tpInteractKey = KeyCode.F;
    public float followMoveSpeed = 22f, followRotateSpeed = 22f;
    public bool debugRay = false;

    private Camera fpCam;
    private PickupItem carrying;
    private PlayerCamera.CameraStyle CurrentStyle {
        get {
            // Read the active PlayerCamera (your scripts are on each cam). We’ll just check which GO is active.
            if (firstPersonCamGO && firstPersonCamGO.activeInHierarchy) return PlayerCamera.CameraStyle.FirstPerson;
            // If you want, also check ThirdPerson/Shifted objects here and return those enums—but not required for logic below.
            return PlayerCamera.CameraStyle.ThirdPerson; // treat “not FP” as third-person family
        }
    }
    
    // OnEnable is called when the object becomes enabled and active
    void OnEnable(){
        if (firstPersonCamGO) fpCam = firstPersonCamGO.GetComponentInChildren<Camera>(true);
        if (!fpCam) fpCam = Camera.main;
    }

    void Update()
    {   
        // Determine if in FP or TP mode, then check for input accordingly
        bool inFP = CurrentStyle == PlayerCamera.CameraStyle.FirstPerson;

        // Separate mouse click and interact-key so we can use the key for button/lever interactions in FP
        bool mousePressed = inFP ? Input.GetMouseButtonDown(0) : false;
        bool keyPressed = Input.GetKeyDown(tpInteractKey);

        // No input, no action
        if (!mousePressed && !keyPressed) return;

        // If carrying an item, drop it on any input
        if (carrying != null){ carrying.Drop(); carrying = null; return; }

        if (inFP)
        {
            // In first-person: mouse click tries to pick up, interact key tries to trigger button/lever
            if (mousePressed)
            {
                PickupItem target = FP_FindTarget();
                if (target != null){
                    target.PickUp(holdPoint, followMoveSpeed, followRotateSpeed);
                    carrying = target;
                }
            }
            else if (keyPressed)
            {
                FP_InteractTarget();
            }
        }
        else
        {
            // Third-person: behave as before (use key to pick up)
            if (keyPressed)
            {
                PickupItem target = TP_FindTarget();
                if (target != null){
                    target.PickUp(holdPoint, followMoveSpeed, followRotateSpeed);
                    carrying = target;
                }
            }
        }
    }

    // FIRST-PERSON: Raycast from center and trigger button press on hit object (if any)
    private void FP_InteractTarget()
    {
        if (!fpCam) return;
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (debugRay) Debug.DrawRay(ray.origin, ray.direction * fpDistance, Color.green, 0.05f);
        if (Physics.Raycast(ray, out var hit, fpDistance, interactMask, QueryTriggerInteraction.Ignore))
        {
            // If it's a pickup item, ignore (F is used for button/lever interactions)
            var pickup = hit.collider.GetComponentInParent<PickupItem>();
            if (pickup != null) return;

            // Try to call PressButton on Button component in the hit object or its parents
            var button = hit.collider.GetComponentInParent<Button>();
            if (button != null)
                button.PressButton();
                
            // Try to call ToggleLever on Lever component in the hit object or its parents
            var lever = hit.collider.GetComponentInParent<Lever>();
            if (lever != null)
                lever.ToggleLever();
        }
    }

    // FIRST-PERSON: Raycast from center of screen
    private PickupItem FP_FindTarget(){
        if (!fpCam) return null;
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (debugRay) Debug.DrawRay(ray.origin, ray.direction * fpDistance, Color.cyan, 0.05f);
        if (Physics.Raycast(ray, out var hit, fpDistance, interactMask, QueryTriggerInteraction.Ignore))
            return hit.collider.GetComponentInParent<PickupItem>();
        return null;
    }

    // THIRD-PERSON: SphereCast from focus point forward
    private PickupItem TP_FindTarget(){
        Transform aim = focusTransform ? focusTransform : transform;
        Vector3 origin = aim.position + aim.forward * 0.2f;
        Vector3 dir = aim.forward;
        if (debugRay) Debug.DrawRay(origin, dir * tpDistance, Color.yellow, 0.05f);
        if (Physics.SphereCast(origin, tpAimRadius, dir, out var hit, tpDistance, interactMask, QueryTriggerInteraction.Ignore))
            return hit.collider.GetComponentInParent<PickupItem>();
        return null;
    }
}
