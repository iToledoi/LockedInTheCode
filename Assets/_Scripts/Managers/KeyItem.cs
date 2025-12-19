using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class KeyItem : MonoBehaviour
{   
    // Unique identifier for this key item
    [Tooltip("Identifier for this key. Must match the LockedDoor.requiredKeyId to open.")]
    public string keyId = "Key";

    // Optional display name for the key (To be implemented in UI)
    [Tooltip("Optional: a friendly name displayed in the inspector.")]
    public string displayName = "Key";
}
