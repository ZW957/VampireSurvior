using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    [DefaultExecutionOrder(200)]
    public class TopDownCharacterController : MonoBehaviour
    {
        [SerializeField,Header("移動速度"),Range(0,100)]
        public float speed=3.5f;

        private Animator ani;
        private Rigidbody2D rig;

        private string parWalk = "走路開關";
        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            //LevelManager.instance.onlevelUP += WhenPlayerLevelUp;
        }
        private void Start()
        {
            ani = GetComponent<Animator>();
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                ani.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                ani.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                ani.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                ani.SetInteger("Direction", 0);
            }

            dir.Normalize();
            ani.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        private void Move()
        {
            
        }
        /// <summary>
        /// 玩家升級時
        /// </summary>
        private void WhenPlayerLevelUp()
        {
            if (this) enabled = false;
        }
    }
}
