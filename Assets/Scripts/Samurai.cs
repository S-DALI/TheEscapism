using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class Samurai : Singleton<Samurai>
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] public int HP = 100;
        [SerializeField] private float jumpF = 8f;
        [SerializeField] private Transform attackPoint;
        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        protected bool CheckCollider = false;
        private bool Is_player_on_floor;
        private Animator animator;
        private HeroCombat her;
        private float HorizontalDirection = 1;
        [SerializeField] private LayerMask canStayLayerMask;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponent<Animator>();
            her = GetComponent<HeroCombat>();
        }
        void Start()
        {

        }

        void FixedUpdate()
        {
            CheckGround();
        }
        void Update()
        {
            bool onground = CheckCollider;
            if (onground && !Input.GetButton("Horizontal")  && !Input.GetKeyDown("w")) state = States.IDLE;
            if (Input.GetButton("Horizontal"))
            {
                Run();
            }
            if (onground && Input.GetKeyDown("w") )
                Jump();

            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetButton("Horizontal") && onground)
            {                
                
                her.Attack();

            }
        }

        private void Run()
        {
            if (CheckCollider) state = States.Run;
            Vector3 run = transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + run, speed * Time.deltaTime);
            sprite.flipX = run.x > 0.0f;
            if(HorizontalDirection>0 && Input.GetAxis("Horizontal")<0|| HorizontalDirection<0 && Input.GetAxis("Horizontal")>0) 
            {
                attackPoint.localPosition = new Vector3(-attackPoint.localPosition.x, attackPoint.localPosition.y, 0);
            }
            
            if(Input.GetAxis("Horizontal")!=0)
            {
                HorizontalDirection = Input.GetAxis("Horizontal");
            }
        }

        private void Jump()
        {
            rb.AddForce(transform.up * jumpF, ForceMode2D.Impulse);
        }

        private void CheckGround()
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f,canStayLayerMask);
            CheckCollider = collider.Length >= 1;
            if (!CheckCollider) state = States.Jump;
        }
        public enum States
        {
            IDLE,
            Run,
            Jump
        }
        private States state
        {
            get { return (States)animator.GetInteger("state"); }
            set { animator.SetInteger("state", (int)value); }
        }


        public  void GetDamage(int damage)
        {
            HP -= damage;
            Debug.Log(HP);
            if(HP<=0)
            {
                SceneManager.LoadScene(0);
            }
        }
       
       
    }
}
