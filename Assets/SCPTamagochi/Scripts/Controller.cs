using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private AnomalyTest anomaly;
    [SerializeField] protected Text Info;
    Test contactTest;

    private class KPoint
    {
        public AnomalyInfo.INFO InfoType { get; }
        public int Count { get; set; }
        public KPoint(AnomalyInfo.INFO info)
        {
            InfoType = info;
            Count = 0;
        }
    }

    private KPoint EldrichKnowledge;
    private KPoint FleshKnowledge;
    private KPoint MechKnowledge;
    private List<KPoint> KPList;

    private void Awake()
    {
        contactTest = new Test("Контакт", new List<int>(new int[] { 1, 2, 3 }));
        EldrichKnowledge = new KPoint(AnomalyInfo.INFO.ELDRICH);
        FleshKnowledge = new KPoint(AnomalyInfo.INFO.FLESH);
        MechKnowledge = new KPoint(AnomalyInfo.INFO.MECH);
        KPList = new List<KPoint>();
        KPList.Add(EldrichKnowledge);
        KPList.Add(FleshKnowledge);
        KPList.Add(MechKnowledge);
    }

    private void Start()
    {
        InfoUpdate();
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
            InfoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            anomaly.CalculateContainment();
    }

    public void InfoUpdate()
    {
        Info.text = "Информация: ";
        for (int i = 0; i < KPList.Count; i++)
        {
            Info.text += "\n" + KPList[i].InfoType.ToString() + ": " + KPList[i].Count;
        }
    }

    public void AddKPoint(AnomalyInfo.INFO info)
    {
        for (int i = 0; i < KPList.Count; i++)
        {
            if (info == KPList[i].InfoType)
                KPList[i].Count++;
        }
    }
}
