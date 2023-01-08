using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kebab.BattleEngine.Ships
{
    [CreateAssetMenu(fileName = "ShipList", menuName = "BattleEngine/ShipList", order = 20)]
    public class ShipListDesignData : ScriptableObject
    {
        public List<SO_Ship> ships = new List<SO_Ship>();
    }
}