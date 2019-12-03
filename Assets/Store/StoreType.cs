using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreType : MonoBehaviour
{
    public enum type
    {
        Damage, Life, FireRate, LoaderSize
    }
    public type storeType;
    public int cost;
    public int upgradePercentage;
}
