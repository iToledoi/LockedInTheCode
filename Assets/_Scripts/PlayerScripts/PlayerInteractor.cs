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

    void OnEnable(){
        if (firstPersonCamGO) fpCam = firstPersonCamGO.GetComponentInChildren<Camera>(true);
        if (!fpCam) fpCam = Camera.main;
    }

    void Update()
    {
        bool inFP = CurrentStyle == PlayerCamera.CameraStyle.FirstPerson;
        bool pressed = inFP ? Input.GetMouseButtonDown(0) : Input.GetKeyDown(tpInteractKey);

        if (!pressed) return;

        if (carrying != null){ carrying.Drop(); carrying = null; return; }

        PickupItem target = inFP ? FP_FindTarget() : TP_FindTarget();
        if (target != null){
            target.PickUp(holdPoint, followMoveSpeed, followRotateSpeed);
            carrying = target;
        }
    }

    private PickupItem FP_FindTarget(){
        if (!fpCam) return null;
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (debugRay) Debug.DrawRay(ray.origin, ray.direction * fpDistance, Color.cyan, 0.05f);
        if (Physics.Raycast(ray, out var hit, fpDistance, interactMask, QueryTriggerInteraction.Ignore))
            return hit.collider.GetComponentInParent<PickupItem>();
        return null;
    }

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
