using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Weapon
{

    public class DamageSystem : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 5000)]
        private float hp;

        [SerializeField, Header("受傷半徑"), Range(0, 50)]
        private float radiusDamage;

        [SerializeField, Header("受傷位移")]
        private Vector2 offsetDamage;

        [SerializeField, Header("傷害植物件")]
        private GameObject prefabDamage;

        [SerializeField, Header("傷害物件位移")]
        private Vector2 offsetDamagepre;

        [SerializeField, Header("受傷圖層")]
        private LayerMask layerDamage;

        [SerializeField, Header("受傷無敵時間"),Range(0,1)]
        private float timeInvisible=0.2f;//多久內不會造成二次傷害

        [SerializeField, Header("經驗值"), Range(0,50000)]
        private float exp;
        [SerializeField]
        private GameObject prefabExp;

        private float timer;
        private bool isDamage;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetDamage();
            InvisibleTimer();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.1f, 0.3f, 0.5f);
            Gizmos.DrawSphere(
                transform.position + transform.TransformDirection(offsetDamage),
                radiusDamage);
        }
        private void GetDamage()
        {
            Collider2D hit = Physics2D.OverlapCircle(
                transform.position + transform.TransformDirection(offsetDamage),
                radiusDamage,
                layerDamage);            

            if (hit)//如果有碰撞物件
            {
                if (!isDamage)//如果尚未受傷 再生成傷害值物件
                {
                    float attack = hit.GetComponent<WeaponAttack>().attack;
                    hp -= attack;
                    if (hp <= 0) Dead();

                    isDamage = true;//設定已受傷
                    print("GetDamage()"+hit.name);
                    //生成傷害值物件
                    GameObject tempDamage= Instantiate(prefabDamage,
                                transform.position+transform.TransformDirection(offsetDamagepre),
                                Quaternion.identity);

                    tempDamage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attack.ToString();
                }
               
            }
        }
        /// <summary>
        /// 無敵時間計時
        /// </summary>
        private void InvisibleTimer()
        {
            if (isDamage)
            {
                if (timer < timeInvisible) timer += Time.deltaTime;
                else
                {
                    timer = 0;
                    isDamage=false;
                }
            }
        }

        private void Dead()
        {
            GameObject tempExp= Instantiate(prefabExp, transform.position, Quaternion.identity);
            tempExp.GetComponent<ExpManager>().exp = exp;
            Destroy(gameObject);
        }
    }
}
