using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Tooltip("The ID of the key that opens this door. Must match a KeyItem.keyId.")]
    public string requiredKeyId;

    [Tooltip("If true, the key must be held (picked up) when touching the door to open it. If false, touching the door with the key (even dropped) will open it.")]
    public bool requireHeld = true;

    [Tooltip("If true the door GameObject will be destroyed when opened. If false, visuals and colliders are disabled instead.")]
    public bool destroyOnOpen = false;

    [Tooltip("Optional delay (seconds) before destroying the door when opened.")]
    public float destroyDelay = 0f;

    [Tooltip("Optional visual root to disable instead of the whole GameObject. If null, the whole GameObject is used.")]
    public GameObject visualRoot;

    bool opened = false;

    void Reset()
    {
        // ensure the door collider is a trigger so keys can be detected easily
        var col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    void Awake()
    {
        // ensure we have a trigger collider so OnTrigger events fire
        var col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning($"LockedDoor '{name}' has no Collider — add a Collider and set isTrigger=true to detect keys.");
        }
        else if (!col.isTrigger)
        {
            // prefer trigger mode for the detection region
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        // Try to find a KeyItem on the colliding object or its parents
        KeyItem key = other.GetComponentInParent<KeyItem>();
        if (key == null) return;

        // Key ID must match
        if (!string.Equals(key.keyId, requiredKeyId))
            return;

        // If required, ensure it's being held (PickupItem tracks IsHeld)
        if (requireHeld)
        {
            var pickup = other.GetComponentInParent<PickupItem>();
            if (pickup == null || !pickup.IsHeld)
                return;
        }

        OpenDoor();
    }

    void OpenDoor()
    {
        if (opened) return;
        opened = true;

        // Optionally disable visuals and colliders instead of destroying the whole object
        if (!destroyOnOpen)
        {
            GameObject root = visualRoot != null ? visualRoot : gameObject;

            // disable renderers
            foreach (var r in root.GetComponentsInChildren<Renderer>())
                r.enabled = false;

            // disable colliders
            foreach (var c in root.GetComponentsInChildren<Collider>())
                c.enabled = false;
        }
        else
        {
            if (destroyDelay <= 0f)
                Destroy(gameObject);
            else
                Destroy(gameObject, destroyDelay);
        }
    }
}
