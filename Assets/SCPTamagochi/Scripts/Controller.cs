using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    
    [SerializeField] private Text Info;
    [SerializeField] private GameObject[] Rooms;
    [SerializeField] private GameObject CurrentRoom;

    public AnomalyTest Anomaly { get; private set; }
    public int Day { get; private set; } = 1;
    public int Actions { get; private set; } = 3;
    public int Capital { get; set; } = 100;
    public bool EndGame { get; set; } = false;
    public List<KPoint> KPList { get; private set; }
    public UnityAction OnGameEnd;
    public UnityAction OnFailure;
    public UnityAction OnEndDay;

    private List<AnomalyBase> _anomalies;
    private KeyCode[] _keypad;
    private KeyCode[] _keyForRooms;
    private KPoint EldrichKnowledge;
    private KPoint FleshKnowledge;
    private KPoint MechKnowledge;
    private Test contactTest;

    private void Awake()
    {
        contactTest = new Test("Контакт", new List<int>(new int[] { 1, 2, 3, 4 }));
        EldrichKnowledge = new KPoint("Древние", AnomalyInfo.INFO.ELDRICH);
        FleshKnowledge = new KPoint("Плоть", AnomalyInfo.INFO.FLESH);
        MechKnowledge = new KPoint("Механ", AnomalyInfo.INFO.MECH);

        KPList = new List<KPoint>();
        KPList.Add(EldrichKnowledge);
        KPList.Add(FleshKnowledge);
        KPList.Add(MechKnowledge);

        _keypad = new KeyCode[] { KeyCode.Keypad0,
                                  KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,
                                  KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6,
                                  KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9,
        };

        _keyForRooms = new KeyCode[] { 0, KeyCode.A , KeyCode.S , KeyCode.D };

        _anomalies = new List<AnomalyBase>();
    }

    public void Init()
    {
        Anomaly = (AnomalyTest)_anomalies[0];
        InfoUpdate();
        Anomaly.HideShowSprite();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeDiet();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContactTest();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            CheckContainment();

        CheckKeyPadPressed();
        CheckKeyForRooms();

    }

    private void CheckEndGame()
    {
        if (EndGame)
            return;
        if (Capital < 0)
        {
            OnFailure();
            EndGame = true;
        }
        if (Day > 10)
        {
            OnGameEnd();
            EndGame = true;
        }
    }

    private void CheckKeyPadPressed()
    {
        for(int i = 0; i < _keypad.Length; i++)
            if (Input.GetKeyDown(_keypad[i]))
                SwitchAnomaly(i);
    }
    private void CheckKeyForRooms()
    {
        for (int i = 0; i < _keyForRooms.Length; i++)
            if (Input.GetKeyDown(_keyForRooms[i])) 
                ChangeRoom((AnomalyContain.CONTAIN_ROOM)i);
    }
    public void ChangeDiet()
    {
        Anomaly.CurrentFood = (AnomalyFood.FOOD)((int)(Anomaly.CurrentFood + 1) % AnomalyFood.FOOD.GetNames(typeof(AnomalyFood.FOOD)).Length);
        Anomaly.InfoUpdate();
    }

    public void ChangeRoom(AnomalyContain.CONTAIN_ROOM room)
    {
        Anomaly.Room = room;
        Capital -= Anomaly.GetRoomCost();
        if (CurrentRoom != Rooms[(int)room])
        {
            CurrentRoom.gameObject.SetActive(false);
            CurrentRoom = Rooms[(int)room];
            CurrentRoom.gameObject.SetActive(true);
        }
        InfoUpdate();
    }

    public bool ContactTest()
    {
        CheckEndGame();
        if (EndGame)
            return false;
        bool result = Anomaly.Test(contactTest);
        Anomaly.InfoUpdate();
        CheckContainment();
        TakeAction();
        InfoUpdate();
        return result;
    }

    private void TakeAction()
    {
        if (--Actions == 0)
        {
            Actions = 3;
            Day++;
            Debug.Log("День" + Day.ToString());
            OnEndDay();
        }
    }

    private void CheckContainment()
    {
        for (int i = 0; i < _anomalies.Count; i++)
        {
            _anomalies[i].CalculateContainment();
        }
    }
    public void InfoUpdate()
    {
        Info.text = "Информация: ";
        Info.text += "\nКапитал: " + Capital;
        for (int i = 0; i < KPList.Count; i++) 
            Info.text += "\n" + KPList[i].Name + ": " + KPList[i].Count; 
    }

    public void AddKPoint(AnomalyInfo.INFO info)
    {
        for (int i = 0; i < KPList.Count; i++)
        {
            if (info == KPList[i].InfoType)
                KPList[i].Count++;
        }
    }

    public int GetAnomalyCount()
    {
        return _anomalies.Count;
    }
    public int GetKPointsOfType(AnomalyInfo.INFO infoType)
    {
        for(int i = 0; i < KPList.Count; i++)
        {
            if (infoType == KPList[i].InfoType)
                return KPList[i].Count;
        }
        return 0;
    }

    public void AddAnomaly(AnomalyBase anomaly)
    {
        _anomalies.Add(anomaly);
    }

    public void SwitchAnomaly(int num)
    {
        if(num < _anomalies.Count)
        {
            Anomaly.HideShowSprite();
            Anomaly = (AnomalyTest)_anomalies[num];
            Anomaly.HideShowSprite();
            Anomaly.InfoUpdate();
            InfoUpdate();
        }
    }

    public class KPoint
    {
        public string Name { get; private set; }
        public AnomalyInfo.INFO InfoType { get; }
        public int Count { get; set; }

        public KPoint(string name, AnomalyInfo.INFO info)
        {
            Name = name;
            InfoType = info;
            Count = 0;
        }
    }
}
