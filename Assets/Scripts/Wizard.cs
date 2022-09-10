using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Wizard : MonoBehaviour
    {
        [SerializeField] private float Speed;
        [SerializeField] private float JumpForce;
        [SerializeField] private float StoppingDistance;
        [SerializeField] private float RetreatDistance;
        [SerializeField] private Transform GroundCheck;
        [SerializeField] private Transform[] JumpChecks;
        [SerializeField] private float CheckRadius;
        [SerializeField] private float CheckJumpRadius;
        [SerializeField] private LayerMask Ground;
        [SerializeField] private GameObject Rotate;
        [SerializeField] private float Offset;
        [SerializeField] private GameObject FireBoll;
        [SerializeField] private Transform ShotPoint;
        [SerializeField] private float StartTimeShot;
        [SerializeField] private float Distance;
        [SerializeField] private LayerMask PlayerMask;
        [SerializeField] private float agrodistance;
        [SerializeField] private Transform HeroPosition;
        private float TimeShots;
        private bool CanShoot;
        private bool IsGround;
        private bool Jump1;
        private bool Jump2;
        private bool FacingRight = false;
        private Rigidbody2D Rigidbody;
        private Hero PlayerHero;
        private Animator AnimatorWizard;
        private Vector2 Position;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            AnimatorWizard = GetComponent<Animator>();
            PlayerHero = FindObjectOfType<Hero>();
        }

        private void Update()
        {
            float DistanceToPlayer = Vector2.Distance(transform.position, HeroPosition.position);

            IsGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, Ground);
            Jump1 = Physics2D.OverlapCircle(JumpChecks[0].position, CheckJumpRadius, Ground);
            Jump2 = Physics2D.OverlapCircle(JumpChecks[1].position, CheckJumpRadius, Ground);
            CanShoot = Physics2D.OverlapCircle(transform.position, Distance, PlayerMask);
            AnimatorWizard.SetBool("onGround", IsGround);

            if (DistanceToPlayer < agrodistance)
            {
                agrodistance = 500;
                if (IsGround)
                {
                    if (Jump1 || Jump2)
                    {
                        AnimatorWizard.SetTrigger("Jump");
                        Rigidbody.velocity = Vector2.up * JumpForce;
                    }
                }

                if (TimeShots <= 0)
                {

                    Instantiate(FireBoll, ShotPoint.position, Rotate.transform.rotation);
                    TimeShots = StartTimeShot;
                }
                else TimeShots -= Time.deltaTime;

                if (!FacingRight && PlayerHero.transform.position.x > transform.position.x)
                {
                    Flip();
                    Offset = 0f;
                    FireBoll.GetComponent<FireBall>().Direction = Vector2.right;
                }
                else if (FacingRight && PlayerHero.transform.position.x < transform.position.x)
                {
                    Flip();
                    Offset = 180f;
                    FireBoll.GetComponent<FireBall>().Direction = Vector2.left;
                }


                Vector3 difference = PlayerHero.transform.position - Rotate.transform.position;
                float rot2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Rotate.transform.rotation = Quaternion.Euler(0f, 0f, rot2 + Offset);

                if (Vector2.Distance(transform.position, PlayerHero.transform.position) > StoppingDistance)
                {
                    Position = Vector2.MoveTowards(transform.position, PlayerHero.transform.position, Speed * Time.deltaTime);
                    AnimatorWizard.SetTrigger("Move");
                }
                else if (Vector2.Distance(transform.position, PlayerHero.transform.position) < StoppingDistance && Vector2.Distance(transform.position, PlayerHero.transform.position) > RetreatDistance)
                {
                    Position = transform.position;
                    AnimatorWizard.SetTrigger("IDLE");
                }
                else if (Vector2.Distance(transform.position, PlayerHero.transform.position) < RetreatDistance)
                {
                    Position = Vector2.MoveTowards(transform.position, PlayerHero.transform.position, -Speed * Time.deltaTime);
                    AnimatorWizard.SetTrigger("MoveBack");
                }
                transform.position = new Vector2(Position.x, transform.position.y);
            }
        }
        void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Distance);
        }
    }

}

