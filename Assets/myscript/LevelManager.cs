using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Weapon
{
    [DefaultExecutionOrder(100)]
    /// <summary>
    /// ���ź޲z��
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�l���g��ȥb�|")]
        private float getExpradius=3.5f;

        [SerializeField, Header("�l���g��ȳt��")]
        private float getExpspeed;

        [SerializeField, Header("�g��ȹϼh")]
        private LayerMask layerExp;

        private int lv = 1;
        private float expCurrent;
        private Image imgExp;
        private TextMeshProUGUI textLV;

        [SerializeField]
        private float[] expNeeds;
        
        
        #endregion
       
        #region �ƥ�





        private void Awake()
        {
            instance = this;
            imgExp = GameObject.Find("�Ϥ��g���").GetComponent<Image>();
            textLV=GameObject.Find("��r����").GetComponent<TextMeshProUGUI>();
            aniupdataelevelandchooseskill = GameObject.Find("�ɯŧޯ�������").GetComponent<Animator>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.1f, 0.8f, 0.7f, 0.3f);
            Gizmos.DrawSphere(transform.position, getExpradius);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetExpObj();
        }
         #endregion

        /// <summary>
        /// �ͤΧޯ�������
        /// </summary>
        private Animator aniupdataelevelandchooseskill;
        public static LevelManager instance;
        public delegate void LevelUP();
        public event LevelUP onlevelUP;

        #region ��k

        [ContextMenu("��s�g��ȻݨD��")]
        private void UpDateExpNeeds()
        {
            expNeeds = new float[99];
            for(int i=0;i< expNeeds.Length; i++)
            {
                expNeeds[i] = (i + 1) * 100 + ((i + 1) * 10);
            }
        }
        /// <summary>
        /// �l���g���
        /// </summary>
        private void GetExpObj()
        {
            //�I������=2���� �л\�ϧνd��(���ȥ�y�СA���o�g��ȥb�|�A���o�g��ȹϼh)
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getExpradius, layerExp);
            
            for(int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];                    
                if (hit)//�p�G�I���s�b
                {
                    //�y��=2��.�V�e����(�I������y�СA���ȥ�y�СA�t��*�@�u�ɶ�)
                    Vector2 pos= Vector2.MoveTowards(
                        hit.transform.position, 
                        transform.position, 
                        getExpspeed * Time.deltaTime);
                    hit.transform.position = pos;//�I������ �y��=�y��

                    UpdateExp(hit);
                }
            }

        }
        /// <summary>
        /// ��s�g���
        /// </summary>
        /// <param name="hit">�g��ȸI����</param>
        private void UpdateExp(Collider2D hit)
        {
            float dis = Vector2.Distance(hit.transform.position, transform.position);
            if (dis <= 0.5f)
            {
                expCurrent += hit.GetComponent<ExpManager>().exp;//�֥[�g��
                float expneed = expNeeds[lv - 1];//���o��e���Ÿg��                

                if (expCurrent >= expneed)//�p�G��e�g���>=�g��ȻݨD(�ɯ�)
                {
                    expCurrent -= expneed;//�N�h�l�g���ٵ����a
                    UpdataeLevel();
                }

                imgExp.fillAmount = expCurrent / expneed;//�Ϥ��񺡪���=��e�g��/�g��ݨD
                Destroy(hit.gameObject);//�R���g��Ӫ���
            }
        }

        private void UpdataeLevel()
        {
            lv++;//�ɯ�
            Debug.Log(lv);
            textLV.text = "level" + lv;//��s���Ŭɭ�
            aniupdataelevelandchooseskill.enabled = true;//�ҰʤɯŤ����ʵe
            onlevelUP();//Ĳ�o�ƥ�
        }
        
        #endregion
    }
}
