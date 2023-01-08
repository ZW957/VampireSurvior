using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// 敵人系統:追蹤玩家
    /// </summary>
    public class EmemySystem : MonoBehaviour
    {
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float speed = 3.5f;
        private string nameTarget = "player";
        private Transform tarTarget;
        [SerializeField, Header("停止距離"), Range(0, 10)]
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
        /// 怪物追蹤玩家
        /// </summary>
        private void Track()
        {
            Vector3 posTarget = tarTarget.position;
            Vector3 posCurrent = transform.position;
            Flip(posCurrent.x, posTarget.x);
            //Vector3.Distance(posCurrent, posTarget)當前座標與目標座標間距離
            if (Vector3.Distance(posCurrent, posTarget) <= stopDistance) return;//玩家座標與怪物座標間距離<=怪物停止距離 

            posCurrent = Vector3.MoveTowards(posCurrent, posTarget, speed*Time.deltaTime);
            transform.position = posCurrent;
        }

        /// <summary>
        /// 怪物移動到距離玩家某距離stopDistance便會停下
        /// 圖像化 繪製一些符號、圖案或區塊，來標記一些特殊的區域。例如：重生點、死亡區或生怪區等。這些區塊我們只想要在編輯時看到，執行遊戲時是不能讓玩家看到的。
        /// OnDrawGizmos()不論有沒有選中這個物件，一定會被畫出來 
        /// OnDrawGizmosSelected()只有被選中這個物件，才會被畫出來
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.1f,0.1f, 0.3f);//圖示顏色
            Gizmos.DrawSphere(transform.position, stopDistance);//繪製球體圖示
        }

        /// <summary>
        /// 翻面
        /// </summary>
        /// <param name="xCurrent">此物件x</param>
        /// <param name="xTarget">目標物件x</param>
        private void Flip(float xCurrent,float xTarget)
        {
            float angle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        /// <summary>
        /// 玩家升級時
        /// </summary>
        private void WhenPlayerLevelUp()
        {
           if(this) enabled = false;
        }
    }

}
