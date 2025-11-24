using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class KeyItem : MonoBehaviour
{
    [Tooltip("Identifier for this key. Must match the LockedDoor.requiredKeyId to open.")]
    public string keyId = "Key";

    [Tooltip("Optional: a friendly name displayed in the inspector.")]
    public string displayName = "Key";
}
