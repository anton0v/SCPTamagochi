using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private AnomalyTest anomaly;
    Test contactTest;

    private void Awake()
    {
        contactTest = new Test("Контакт", new List<int>(new int[] { 1, 2, 3 }));
    }

    private void Start()
    {
        anomaly.InfoUpdate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anomaly.CurrentFood = (AnomalyFood.FOOD)((int)(anomaly.CurrentFood + 1) % AnomalyFood.FOOD.GetNames(typeof(AnomalyFood.FOOD)).Length);
            anomaly.InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anomaly.Test(contactTest);
            anomaly.InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            anomaly.CalculateContainment();
    }
}
