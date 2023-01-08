using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {   
        [SerializeField,Header("WeaponData")]
        private WeaponData weaponData;
        [SerializeField]
        private int level;

        private WeaponLevelData weaponLevel => weaponData.weaponLevelData[level];//抓取weaponData內weaponLevelData資料

        private void Awake()
        {
            //SpawnWeapon();
            InvokeRepeating("SpawnWeapon", 0, weaponLevel.intervalSpawn);//重複呼叫(方法名稱，開始時間(延遲多久呼叫方法，重複頻率)
        }
       
        
        private void SpawnWeapon()
        {
            WeaponObject[] weaponObjects = weaponData.weaponLevelData[level].weaponObjects;
            for (int i = 0; i < weaponObjects.Length; i++)
            {
                //實例化 生成(物件,座標,角度)
                //transform.TransformDirection變形方向
                GameObject tempWeapon =Instantiate(
                    weaponData.weaponfre,
                    transform.position+transform.TransformDirection( weaponObjects[i].pointSpawn),
                    Quaternion.identity);
                tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponObjects[i].speed);

                Vector2 speedmove;
                //if (weaponData.withCharacterDirection) speedmove = transform.right * weaponObjects[i].speed
                if (weaponData.withCharacterDirection) speedmove = transform.TransformDirection(weaponObjects[i].speed);
                else speedmove = weaponObjects[i].speed;

                tempWeapon.GetComponent<Rigidbody2D>().AddForce(speedmove);
                tempWeapon.AddComponent<WeaponAttack>().attack = weaponLevel.attack;
            }

        }


        /// <summary>
        /// 玩家升級時
        /// </summary>
        private void WhenPlayerLevelUp()
        {
            if (this) {
                CancelInvoke();
                enabled = false;
            } 
        }
    }

}
