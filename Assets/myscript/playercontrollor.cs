using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{ 
    /// <summary>
    /// player控制器
    /// </summary>
    public class playercontrollor : MonoBehaviour
    {
        [SerializeField,Header("移動速度"),Range(0,100)]
        private float speed = 3.5f;
        private Animator playerani;
        private Rigidbody2D playerrig;
        private string playerWalk = "isRun";

        private void Awake()
        {
            playerani = GetComponent<Animator>();
            playerrig = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }
        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            print($"x軸:{x} y軸:{y}");
            playerrig.velocity = new Vector2(x , y)* speed;
            UpdateAnimaation(x, y);
            Flip(x);
        }

        private void UpdateAnimaation(float x,float y)
        {
            playerani.SetBool(playerWalk, x != 0 || y != 0);
        }
        private void Flip(float x)
        {
            if (Mathf.Abs(x) < 0.1f) return;
            transform.eulerAngles = new Vector2(0, x > 0 ? 0 : 180);
        }
    }
}

