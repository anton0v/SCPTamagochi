using System.Collections;
using UnityEngine;
using static AnomalyContain;

public class AnomalyContain : AnomalyBehavior
{
    public enum CONTAIN_WEAPON {GUN, TERMO, ELECTRO, CHEM}
    public enum CONTAIN_ROOM { METAL, ACULT, HOUSE}

    public CONTAIN_ROOM Room { get; set; }
    public CONTAIN_WEAPON Weapon { get; set; }

    private CONTAIN_ROOM _preferRoom;
    private CONTAIN_WEAPON _weakness;
    private CONTAIN_WEAPON _ineffective;

    class TagContainWeapon : Tag
    {
        private CONTAIN_WEAPON _containWeapon;
        TagContainWeapon(string name, CONTAIN_WEAPON weapon, bool isWeakness) : base(name)
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

    class TagRoom : Tag
    {
        CONTAIN_ROOM _room;
        TagRoom(string name, CONTAIN_ROOM room) : base(name)
        {
            _room = room;
        }

        void SetRoom(AnomalyBase anomaly)
        {
            ((AnomalyContain)anomaly)._preferRoom = _room;
        }
    }

}