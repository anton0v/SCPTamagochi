using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Anomaly anomaly;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anomaly.Food = (Anomaly.FOOD)((int)(anomaly.Food + 1) % Anomaly.FOOD.GetNames(typeof(Anomaly.FOOD)).Length);
            anomaly.InfoUpdate();
        }
            
        if (Input.GetKeyDown(KeyCode.Space))
            anomaly.CalculateContainment();
    }
}
