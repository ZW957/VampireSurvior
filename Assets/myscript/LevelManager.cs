using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Weapon
{
    [DefaultExecutionOrder(100)]
    /// <summary>
    /// 等級管理器
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("吸取經驗值半徑")]
        private float getExpradius=3.5f;

        [SerializeField, Header("吸取經驗值速度")]
        private float getExpspeed;

        [SerializeField, Header("經驗值圖層")]
        private LayerMask layerExp;

        private int lv = 1;
        private float expCurrent;
        private Image imgExp;
        private TextMeshProUGUI textLV;

        [SerializeField]
        private float[] expNeeds;
        
        
        #endregion
       
        #region 事件





        private void Awake()
        {
            instance = this;
            imgExp = GameObject.Find("圖片經驗值").GetComponent<Image>();
            textLV=GameObject.Find("文字等級").GetComponent<TextMeshProUGUI>();
            aniupdataelevelandchooseskill = GameObject.Find("升級技能選取介面").GetComponent<Animator>();
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
        /// 生及技能選取介面
        /// </summary>
        private Animator aniupdataelevelandchooseskill;
        public static LevelManager instance;
        public delegate void LevelUP();
        public event LevelUP onlevelUP;

        #region 方法

        [ContextMenu("更新經驗值需求表")]
        private void UpDateExpNeeds()
        {
            expNeeds = new float[99];
            for(int i=0;i< expNeeds.Length; i++)
            {
                expNeeds[i] = (i + 1) * 100 + ((i + 1) * 10);
            }
        }
        /// <summary>
        /// 吸取經驗值
        /// </summary>
        private void GetExpObj()
        {
            //碰撞物體=2物體 覆蓋圖形範圍(本務件座標，取得經驗值半徑，取得經驗值圖層)
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getExpradius, layerExp);
            
            for(int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];                    
                if (hit)//如果碰撞存在
                {
                    //座標=2為.向前移動(碰撞物件座標，本務件座標，速度*一真時間)
                    Vector2 pos= Vector2.MoveTowards(
                        hit.transform.position, 
                        transform.position, 
                        getExpspeed * Time.deltaTime);
                    hit.transform.position = pos;//碰撞物件 座標=座標

                    UpdateExp(hit);
                }
            }

        }
        /// <summary>
        /// 更新經驗值
        /// </summary>
        /// <param name="hit">經驗值碰撞器</param>
        private void UpdateExp(Collider2D hit)
        {
            float dis = Vector2.Distance(hit.transform.position, transform.position);
            if (dis <= 0.5f)
            {
                expCurrent += hit.GetComponent<ExpManager>().exp;//累加經驗
                float expneed = expNeeds[lv - 1];//取得當前等級經驗                

                if (expCurrent >= expneed)//如果當前經驗值>=經驗值需求(升級)
                {
                    expCurrent -= expneed;//將多餘經驗還給玩家
                    UpdataeLevel();
                }

                imgExp.fillAmount = expCurrent / expneed;//圖片填滿長度=當前經驗/經驗需求
                Destroy(hit.gameObject);//刪除經驗植物件
            }
        }

        private void UpdataeLevel()
        {
            lv++;//升級
            Debug.Log(lv);
            textLV.text = "level" + lv;//更新等級界面
            aniupdataelevelandchooseskill.enabled = true;//啟動升級介面動畫
            onlevelUP();//觸發事件
        }
        
        #endregion
    }
}
