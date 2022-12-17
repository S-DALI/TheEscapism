using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MonstersHealth : MonoBehaviour
    {
        [SerializeField] public Animator Monster;
        [SerializeField] public int MaxHealth =100;
        public int currentHealth = 0;
        public delegate void Action(float a);
        public event Action HealthChanged;
        [SerializeField] protected Canvas HPBar;
        protected float TimerDisappear;
        protected float DisappearHpBar=10f;
        [SerializeField] protected Animator HPBarDisappear;

        [Header("Audio")]
        [SerializeField] private AudioSource Hurt;

        public void Start()
        {
            currentHealth = MaxHealth;
        }
        public void GetDamage(int damage)
        {
            if (currentHealth > 0)
            {
                
                if (HPBar.gameObject.activeSelf == false)
                {
                    HPBar.gameObject.SetActive(true);
                }
                HPBarDisappear.SetTrigger("Appear");
                Monster.SetTrigger("Get_Damage");
                Hurt.Play();
                currentHealth -= damage;
                if (currentHealth <= 0)
                {
                    Die();
                }
                else
                {
                    float currentHealthAsPercantage = (float)currentHealth / MaxHealth;
                    HealthChanged?.Invoke(currentHealthAsPercantage);
                }
            }

        }
        public virtual void Die()
        {
           
            Hero.Instance.CurrentKill++;
            this.enabled = false;
            HealthChangedEvent();
            Monster.SetBool("Bandit_leave", true);
            HPBar.gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Collider2D>().enabled = false;

        }
        public void Destroy()
        {
            Destroy(this.gameObject);
        }
        protected virtual void Update()
        {
            if(HPBar.gameObject.activeSelf == true)
            {
                TimerDisappear += Time.deltaTime;
                if(TimerDisappear>=DisappearHpBar)
                {
                    TimerDisappear = 0;
                    HPBarDisappear.SetTrigger("Disappear");
                }
            }
        } 
        IEnumerator WaitDissappear()
        {
            yield return new WaitForSeconds(0.5f);
            HPBar.gameObject.SetActive(false);
        }
        public void HealthChangedEvent()
        {
            HealthChanged?.Invoke(0);
        }
    }
}
