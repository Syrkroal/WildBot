using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum type {
        Ammo, Health
    };
    public type itemType;
    public float rate;
}
