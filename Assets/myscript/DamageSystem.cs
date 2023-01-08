using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Weapon
{

    public class DamageSystem : MonoBehaviour
    {
        [SerializeField, Header("��q"), Range(0, 5000)]
        private float hp;

        [SerializeField, Header("���˥b�|"), Range(0, 50)]
        private float radiusDamage;

        [SerializeField, Header("���˦첾")]
        private Vector2 offsetDamage;

        [SerializeField, Header("�ˮ`�Ӫ���")]
        private GameObject prefabDamage;

        [SerializeField, Header("�ˮ`����첾")]
        private Vector2 offsetDamagepre;

        [SerializeField, Header("���˹ϼh")]
        private LayerMask layerDamage;

        [SerializeField, Header("���˵L�Įɶ�"),Range(0,1)]
        private float timeInvisible=0.2f;//�h�[�����|�y���G���ˮ`

        [SerializeField, Header("�g���"), Range(0,50000)]
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

            if (hit)//�p�G���I������
            {
                if (!isDamage)//�p�G�|������ �A�ͦ��ˮ`�Ȫ���
                {
                    float attack = hit.GetComponent<WeaponAttack>().attack;
                    hp -= attack;
                    if (hp <= 0) Dead();

                    isDamage = true;//�]�w�w����
                    print("GetDamage()"+hit.name);
                    //�ͦ��ˮ`�Ȫ���
                    GameObject tempDamage= Instantiate(prefabDamage,
                                transform.position+transform.TransformDirection(offsetDamagepre),
                                Quaternion.identity);

                    tempDamage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attack.ToString();
                }
               
            }
        }
        /// <summary>
        /// �L�Įɶ��p��
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
