using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anomaly : MonoBehaviour
{
    public enum FOOD {MEAT, FRUIT};

    [SerializeField] private Text Info;
    private SpriteRenderer sr;
    private FOOD _currentFood;
    private FOOD _preferFood;

    public FOOD Food { get { return _currentFood; } set { _currentFood = value; } }

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        _preferFood = (FOOD)Random.Range(0, 2);
        InfoUpdate();
    }

    public void CalculateContainment()
    {
        InfoUpdate();
        if (_currentFood != _preferFood)
            GetAngry();
        else
            CalmDown();
    }    

    private void GetAngry()
    {
        sr.color = Color.red;
    }

    private void CalmDown()
    {
        sr.color = Color.white;
    }

    public void InfoUpdate()
    {
        Info.text = "Рацион: " + _currentFood.ToString();
    }

    
}
