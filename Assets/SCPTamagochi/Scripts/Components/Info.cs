using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Anomaly/Info")]
public class Info : MonoBehaviour, ITag
{
    public enum TYPE
    {
        Flesh,
        Eldrich,
        Mechan
    }

    [SerializeField] private TYPE AnomalyType;
    public string Name { get { return AnomalyType.ToString(); } }

    private void Start()
    {
        Debug.Log(Name);
    }
}
