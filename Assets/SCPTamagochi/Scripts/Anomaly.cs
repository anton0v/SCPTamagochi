using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anomaly : MonoBehaviour
{
    Anomaly.FoodTag fruitTag = new Anomaly.FoodTag("Фрукты", Anomaly.FOOD.FRUIT);
    Anomaly.FoodTag meatTag = new Anomaly.FoodTag("Мясо", Anomaly.FOOD.MEAT);

    public enum FOOD {MEAT, FRUIT};

    [SerializeField] private Text Info;
    private SpriteRenderer sr;
    private FOOD _currentFood;
    private FOOD _preferFood;
    private List<Tag> tags;

    public FOOD Food { get { return _currentFood; } set { _currentFood = value; } }

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        //_preferFood = (FOOD)Random.Range(0, 2);
        tags = new List<Tag>(1);
        tags.Add(meatTag);
        tags[0].SettingTag(this);
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
        Info.text += "\nТеги: ";
        for(int i = 0; i < tags.Count; i++)
        {
            Info.text += tags[i].Name;
        }
    }

    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
        }
        public string Name { get; protected set; }
        public delegate void TagDelegate(Anomaly anomaly);
        public TagDelegate SettingTag;
    }

    public class FoodTag : Anomaly.Tag
    {
        public FoodTag(string name, Anomaly.FOOD preferFood) : base(name)
        {
            Name = name;
            _preferFood = preferFood;
            SettingTag = SetFood;
        }

        private Anomaly.FOOD _preferFood;

        public void SetFood(Anomaly anomaly)
        {
            anomaly._preferFood = _preferFood;
        }
    }
}
