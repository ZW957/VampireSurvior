using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// �Z���t��
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {   
        [SerializeField,Header("WeaponData")]
        private WeaponData weaponData;
        [SerializeField]
        private int level;

        private WeaponLevelData weaponLevel => weaponData.weaponLevelData[level];//���weaponData��weaponLevelData���

        private void Awake()
        {
            //SpawnWeapon();
            InvokeRepeating("SpawnWeapon", 0, weaponLevel.intervalSpawn);//���ƩI�s(��k�W�١A�}�l�ɶ�(����h�[�I�s��k�A�����W�v)
        }
       
        
        private void SpawnWeapon()
        {
            WeaponObject[] weaponObjects = weaponData.weaponLevelData[level].weaponObjects;
            for (int i = 0; i < weaponObjects.Length; i++)
            {
                //��Ҥ� �ͦ�(����,�y��,����)
                //transform.TransformDirection�ܧΤ�V
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
        /// ���a�ɯŮ�
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
