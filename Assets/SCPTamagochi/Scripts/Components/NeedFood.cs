using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Anomaly/NeedFood")]
public class NeedFood : MonoBehaviour, IContainable
{
    public enum TYPE
    {
        Fruit,
        Meat
    }

    [SerializeField] private TYPE FoodType;
    private TYPE _currentFood;

    public string Name { get { return FoodType.ToString(); } }
    public TYPE Food { get { return _currentFood; } }

    public bool CheckContainment()
    {
        return FoodType == _currentFood;
    }
}
