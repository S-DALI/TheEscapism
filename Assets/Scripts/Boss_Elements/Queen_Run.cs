using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Queen_Run : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private float Speed;
        [SerializeField] private float JumpForce;
        [SerializeField] private Transform[] JumpSensor;
        [SerializeField] private Transform GroundSensor;
        [SerializeField] private float CheckJumpRadius;
        [SerializeField] private float CheckGroundRadius;
        [SerializeField] private float AgroDistance;
        [SerializeField] private LayerMask GroundMask;
        [SerializeField] private float StoppingDistance;
        [SerializeField] private Transform Rotate;
        [SerializeField] private GameObject FireBall;
        [SerializeField] private Transform ShotPoint;
        [SerializeField] private LayerMask HeroLayer;

        private Rigidbody2D QueenRB;
        private Animator QueenAnim;
        private bool Jump0;
        private bool Jump1;
        private bool Jump2;
        private bool Jump3;
        private bool isGround;
        private Vector2 Position;

        void Start()
        {
            QueenRB = GetComponent<Rigidbody2D>();
            QueenAnim = GetComponent<Animator>();
        }

        void Update()
        {
            float DistanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);
            isGround = Physics2D.OverlapCircle(GroundSensor.position, CheckGroundRadius, GroundMask);
            Jump0 = Physics2D.OverlapCircle(JumpSensor[0].position, CheckJumpRadius);
            Jump1 = Physics2D.OverlapCircle(JumpSensor[1].position, CheckJumpRadius);
            Jump2 = Physics2D.OverlapCircle(JumpSensor[2].position, CheckJumpRadius);
            Jump3 = Physics2D.OverlapCircle(JumpSensor[3].position, CheckJumpRadius,HeroLayer);
            QueenAnim.SetBool("onGround", isGround);
            if (DistanceToPlayer <= AgroDistance)
            {
                AgroDistance = 100;

                if (Player.position.x < transform.position.x && transform.localScale.x > 0 || Player.position.x > transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    FireBall.GetComponent<FireBall>().Direction = Vector2.left;
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    FireBall.GetComponent<FireBall>().Direction = Vector2.right;
                }

                if (Jump3)
                {
                    QueenAnim.SetTrigger("Jump");
                    QueenRB.velocity = Vector2.up * JumpForce;

                }
                if (isGround)
                {
                    if (Jump0||Jump1||Jump2)
                    {
                        QueenAnim.SetTrigger("Jump");
                        QueenRB.velocity = Vector2.up * JumpForce;

                    }
                }

                if (DistanceToPlayer > StoppingDistance)
                {
                    Position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
                    QueenAnim.SetTrigger("Move");
                }
                else
                {
                    QueenAnim.SetTrigger("IDLE");
                }
                Vector3 difference = Player.transform.position - Rotate.transform.position;
                transform.position = new Vector2(Position.x, transform.position.y);
                float rot2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Rotate.transform.rotation = Quaternion.Euler(0f, 0f, rot2);

            }
        }

        private void FireBallSpawn()
        {
            Instantiate(FireBall, ShotPoint.position, Rotate.transform.rotation);

        }
    }
}
