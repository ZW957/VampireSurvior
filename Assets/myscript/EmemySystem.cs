using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// �ĤH�t��:�l�ܪ��a
    /// </summary>
    public class EmemySystem : MonoBehaviour
    {
        [SerializeField, Header("���ʳt��"), Range(0, 10)]
        private float speed = 3.5f;
        private string nameTarget = "player";
        private Transform tarTarget;
        [SerializeField, Header("����Z��"), Range(0, 10)]
        private float stopDistance = 1.5f;

        private void Awake()
        {
            tarTarget = GameObject.Find(nameTarget).transform;
            LevelManager.instance.onlevelUP += WhenPlayerLevelUp;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Track();
        }

        /// <summary>
        /// �Ǫ��l�ܪ��a
        /// </summary>
        private void Track()
        {
            Vector3 posTarget = tarTarget.position;
            Vector3 posCurrent = transform.position;
            Flip(posCurrent.x, posTarget.x);
            //Vector3.Distance(posCurrent, posTarget)��e�y�лP�ؼЮy�ж��Z��
            if (Vector3.Distance(posCurrent, posTarget) <= stopDistance) return;//���a�y�лP�Ǫ��y�ж��Z��<=�Ǫ�����Z�� 

            posCurrent = Vector3.MoveTowards(posCurrent, posTarget, speed*Time.deltaTime);
            transform.position = posCurrent;
        }

        /// <summary>
        /// �Ǫ����ʨ�Z�����a�Y�Z��stopDistance�K�|���U
        /// �Ϲ��� ø�s�@�ǲŸ��B�Ϯשΰ϶��A�ӼаO�@�ǯS���ϰ�C�Ҧp�G�����I�B���`�ϩΥͩǰϵ��C�o�ǰ϶��ڭ̥u�Q�n�b�s��ɬݨ�A����C���ɬO���������a�ݨ쪺�C
        /// OnDrawGizmos()���צ��S���襤�o�Ӫ���A�@�w�|�Q�e�X�� 
        /// OnDrawGizmosSelected()�u���Q�襤�o�Ӫ���A�~�|�Q�e�X��
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.1f,0.1f, 0.3f);//�ϥ��C��
            Gizmos.DrawSphere(transform.position, stopDistance);//ø�s�y��ϥ�
        }

        /// <summary>
        /// ½��
        /// </summary>
        /// <param name="xCurrent">������x</param>
        /// <param name="xTarget">�ؼЪ���x</param>
        private void Flip(float xCurrent,float xTarget)
        {
            float angle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        /// <summary>
        /// ���a�ɯŮ�
        /// </summary>
        private void WhenPlayerLevelUp()
        {
           if(this) enabled = false;
        }
    }

}
