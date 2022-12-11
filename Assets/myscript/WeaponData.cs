using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Weapondata", fileName = " new Weapon data")]
    public class WeaponData : ScriptableObject
    {
        [Header("武器物件")]
        public GameObject weaponfre;
        [Header("武器等級資料")]
        public WeaponLevelData[] weaponLevelData;
        [Header("武器等級資料")]
        public bool withCharacterDirection;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

    [System.Serializable]
    public class WeaponObject
    {
         [Header("武器速度")]
         public Vector2 speed;
         [Header("武器生成位置")]
         public Vector3 pointSpawn;
    }

    [System.Serializable]
    public class WeaponLevelData
    {
        [Header("武器生成間隔"),Range(0,10)]
        public float intervalSpawn=3;

        [Header("武器攻擊力"), Range(0, 10000)]
        public float attack;
        [Header("武器數量速度生成位置")]
        public WeaponObject[] weaponObjects;
        
    }
}
