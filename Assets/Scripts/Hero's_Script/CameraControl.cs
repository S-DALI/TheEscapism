using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private GameObject PortalToThisLevel;
        [SerializeField] private GameObject PortalToNextLevel;
        [SerializeField] private int EnemyInLevel;
        [SerializeField] private Transform player;
        private Vector3 CamPos;
        private void Awake()
        {
            if (!player)
            {
                player = FindObjectOfType<Hero>().transform;
            }
        }

        void Update()
        {
            if (Hero.Instance.CurrentKill < EnemyInLevel)
            {
                PortalToNextLevel.SetActive(false);
            }
            else PortalToNextLevel.SetActive(true);

            if (Hero.Instance.CurrentKill >= EnemyInLevel)
            {
                PortalToThisLevel.SetActive(false);
            }
            else PortalToThisLevel.SetActive(true);
            CamPos = player.position;
            CamPos.z = -10;
            CamPos.y += 2f;
            transform.position = Vector3.Lerp(transform.position, CamPos, Time.deltaTime);
            
        }
    }
}
