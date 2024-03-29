using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class Hero : Singleton<Hero>
    {
        [Header("Hero Parameters")]
        [SerializeField] public float speed = 3f;
        [SerializeField] public int HP = 10;
        [SerializeField] private float jumpF = 8f;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float Impulse;

        [Header("Hearts UI")]
        [SerializeField] private Image[] hearts;
        [SerializeField] private Sprite RedHearts;
        [SerializeField] private Sprite GreyHearts;

        [Header("Other Parameters")]
        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        private Animator animator;
        private HeroCombat her;
        private float HorizontalDirection = 1;
        [SerializeField] private LayerMask canStayLayerMask;
        private float attackTimer = Mathf.Infinity;
        [SerializeField] private float attaclCoolDawn;
        private bool TakeDamage = false;
        private bool GetDamageHero = true;
        public int CurrentKill = 0;
        private int CurrentAttack = 0;
        private SensorScript m_groundSensor;
        private bool m_grounded = false;

        [Header("Stamina UI")]
        [SerializeField] int Stamina = 5;
        [SerializeField] float StaminaCD;
        [SerializeField] private Image[] StaminaImage;
        [SerializeField] private Sprite StaminaColor;
        [SerializeField] private Sprite StaminaGrey;
        private float TimerStamina;

        [Header("Controler")]
        [SerializeField] private Joystick joystick;
        private bool HeroAttack = false;
        private bool HeroBlock = false;

        [Header("Blink Parametrs")]
        [SerializeField] private float BlinkCD;
        private float BlinkTimer;

        [Header("Audio")]
        [SerializeField] private AudioSource jump;
        [SerializeField] private AudioSource Hurt;
        [SerializeField] private AudioSource RunSound;
        [SerializeField] private AudioSource DeadSound;
        protected override void Awake()
        {
            Stamina = 10;
            HP = 10;
            m_grounded = false;
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponent<Animator>();
            her = GetComponent<HeroCombat>();
            m_groundSensor = transform.Find("GroundSensor").GetComponent<SensorScript>();
            RunSound.Play();
            RunSound.Pause();
        }
        void Update()
        {
            attackTimer += Time.deltaTime;
            BlinkTimer += Time.deltaTime;
            if(Stamina<10)
                TimerStamina += Time.deltaTime;

            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                animator.SetBool("Grounded", m_grounded);
            }

            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                animator.SetBool("Grounded", m_grounded);
            }

            if (m_grounded && joystick.Horizontal == 0 && joystick.Vertical < 0.5f && joystick.Vertical<0.5f && HeroBlock == false)
            {
                animator.SetTrigger("IDLE");
            }

            animator.SetFloat("AirSpeedY", rb.velocity.y);
            if (joystick.Horizontal == 0 || !m_grounded || HP<=0)
            {
                RunSound.Pause();
            }

            if (joystick.Horizontal!=0 )
            {
                Run();
            }

            if (m_grounded && joystick.Vertical>0.5f)
            {
                Jump();
            }
          
            if (Stamina < 10 && TimerStamina >= StaminaCD)
            {
                TimerStamina = 0;
                Stamina++;
            }

            if(HP>10)
            {
                HP = 10;
            }

            if(Stamina > 10)
            {
                Stamina = 10;
            }

            for (int i = 0; i < hearts.Length;i++)
            {
                if (i < HP)
                    hearts[i].sprite = RedHearts;
                else
                    hearts[i].sprite = GreyHearts;
                if (i < 10)
                    hearts[i].enabled = true;
                else
                    hearts[i].enabled = false;
            }

            for(int i = 0; i<StaminaImage.Length;i++)
            {
                if (i < Stamina)
                    StaminaImage[i].sprite = StaminaColor;
                else
                    StaminaImage[i].sprite = StaminaGrey;
                if (i < 10)
                    hearts[i].enabled = true;
                else
                    hearts[i].enabled = false;
            }

        }

        public void Blink()
        {
            if (m_grounded && Stamina > 0 && joystick.Horizontal != 0 && BlinkTimer >= BlinkCD && HP>0)
            {
                BlinkTimer = 0;
                Stamina--;
                animator.Play("Blink");
                if (joystick.Horizontal > 0)
                {
                    rb.AddForce(Vector2.right * Impulse);
                }
                if (joystick.Horizontal < 0)
                {
                    rb.AddForce(Vector2.left * Impulse);
                }
            }
        }
        private void Run()
        {

            HeroBlock = false;
            GetDamageHero = true;
            TakeDamage = false;
            if(m_grounded && HP>0)
                RunSound.UnPause();
            if (m_grounded) animator.SetTrigger("Run");
            if (!m_grounded) animator.ResetTrigger("Run");
            Vector3 run = transform.right * joystick.Horizontal;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + run, speed * Time.deltaTime);
            sprite.flipX = run.x < 0.0f;
            if(HorizontalDirection>0 && joystick.Horizontal < 0|| HorizontalDirection<0 && joystick.Horizontal > 0) 
            {
                attackPoint.localPosition = new Vector3(-attackPoint.localPosition.x, attackPoint.localPosition.y, 0);
            }
            
            if (joystick.Horizontal != 0)
            {
                HorizontalDirection = joystick.Horizontal;
            }
        }

        private void Jump()
        {
            animator.SetTrigger("Jump");
            HeroBlock = false;
            GetDamageHero = true;
            TakeDamage = false;
            m_grounded = false;

            rb.velocity = Vector2.up * jumpF;
            jump.Play();
            m_groundSensor.Disable(0.2f);
        }

        public void GetDamage(int damage)
        {
            if (GetDamageHero == true || Stamina <2)
            {

                if (HP > 0)
                {
                    
                    animator.ResetTrigger("Block");
                    animator.SetTrigger("Hurt");
                    if (!m_grounded) animator.SetInteger("AnimState", 1);
                    HP -= damage;
                    Hurt.Play();

                }
                if (HP <= 0)
                {
                    RunSound.Pause();
                    DeadSound.Play();
                    foreach (var a in hearts)
                    {
                        a.sprite = GreyHearts;
                    }
                    this.enabled = false;
                    animator.SetTrigger("Dead");
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                    GetComponent<Collider2D>().enabled = false;
                }
            }
            else
            {
                Stamina-=2;
                TakeDamage = true;
                animator.SetTrigger("BeatAttack");
            }
        }
        public void GetDamageFromTrap()
        {
            if (HP > 0)
            {
                animator.SetTrigger("Hurt");
                HP -= HP;
                Debug.Log(HP);
            }
            if (HP <= 0)
            {
                RunSound.Pause();
                DeadSound.Play();
                foreach(var a in hearts)
                {
                    a.sprite = GreyHearts;
                }
                this.enabled = false;
                animator.SetTrigger("Dead");
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                GetComponent<Collider2D>().enabled = false;
            }
        }
            private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
        }

        public void TakeDamageHero()
        {
            GetDamageHero = true;
            TakeDamage = false;
            HeroBlock = false;
        }

        public void HeroNotAttac()
        {
            HeroAttack = false;
        }
        public void Attack()
        {
            if ( /*m_grounded && */HeroBlock == false && HP > 0)
            {
                HeroBlock = false;
                HeroAttack = true;
                GetDamageHero = true;
                if (attackTimer >= attaclCoolDawn)
                {
                    CurrentAttack++;
                    
                    if (Stamina > 0)
                    {
                        if (CurrentAttack > 3)
                        {
                            CurrentAttack = 1;
                        }
                        if (attackTimer > 0.7f)
                        {
                            CurrentAttack = 1;
                        }
                        animator.SetTrigger("Attack" + CurrentAttack);
                        attackTimer = 0;
                        Stamina--;
                    }
                }
            }
        }


        public void Block()
        {
            if (m_grounded && joystick.Horizontal == 0 && TakeDamage == false && HP > 0)
            {
                HeroBlock = true;
                GetDamageHero = false;
                animator.SetTrigger("Block");
            }
        }
        
    }
}
