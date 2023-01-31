using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnomalyContain;
using static AnomalyFood;

public class AnomalyContain : AnomalyBehavior
{
    public enum CONTAIN_WEAPON {GUN, TERMO, ELECTRO, CHEM}
    public enum CONTAIN_ROOM { NONE, METAL, OCCULT, HOUSE}

    public CONTAIN_ROOM Room { get; set; }
    public CONTAIN_WEAPON Weapon { get; set; }

    private CONTAIN_ROOM _preferRoom;
    private CONTAIN_WEAPON _weakness;
    private CONTAIN_WEAPON _ineffective;
    private int[] _roomCost = new int[] { 0, 50, 40, 40};

    static protected TagRoom TagRoomOccult;
    static protected TagRoom TagRoomMetal;
    static protected TagRoom TagRoomHouse;
    static protected List<TagRoom> RoomTagList;

    protected new void Awake()
    {
        base.Awake();
        if (TagRoomOccult == null) TagRoomOccult = new TagRoom("Оккультный", CONTAIN_ROOM.OCCULT);
        if (TagRoomMetal == null) TagRoomMetal = new TagRoom("Разрушительный", CONTAIN_ROOM.METAL);
        if (TagRoomHouse == null) TagRoomHouse = new TagRoom("Любит комфорт", CONTAIN_ROOM.HOUSE);
        if (RoomTagList == null)
        {
            RoomTagList = new List<TagRoom>();
            RoomTagList.Add(TagRoomOccult);
            RoomTagList.Add(TagRoomMetal);
            RoomTagList.Add(TagRoomHouse);
        }
    }

    public override void CalculateContainment()
    {
        base.CalculateContainment();
        if (Room != _preferRoom) DecreaseAngerCnt();
    }
    protected override int ResearchChance()
    {
        int chance = (_preferRoom == Room) ? base.ResearchChance() : base.ResearchChance() - 10;
        return chance;
    }

    public int GetRoomCost()
    {
        return _roomCost[(int)Room];
    }

    protected class TagContainWeapon : Tag
    {
        private CONTAIN_WEAPON _containWeapon;
        public TagContainWeapon(string name, CONTAIN_WEAPON weapon, bool isWeakness) : base(name)
        {
            _containWeapon = weapon;
            if (isWeakness)
                SetTag = SetWeakness;
            else
                SetTag = SetIneffective;
        }

        private void SetWeakness(AnomalyBase anomaly)
        {
            ((AnomalyContain)anomaly)._weakness = _containWeapon;
        }

        private void SetIneffective(AnomalyBase anomaly)
        {
            ((AnomalyContain)anomaly)._ineffective = _containWeapon;
        }
    }

    protected class TagRoom : Tag
    {
        private CONTAIN_ROOM _room;
        public TagRoom(string name, CONTAIN_ROOM room) : base(name)
        {
            TagId = 4;
            _room = room;
            SetTag = SetRoom;
        }

        public TagRoom(TagRoom tag) : base(tag.Name)
        {
            TagId = 4;
            _room = tag._room;
            SetTag = tag.SetTag;
        }

        void SetRoom(AnomalyBase anomaly)
        {
            ((AnomalyContain)anomaly)._preferRoom = _room;
        }
    }

}