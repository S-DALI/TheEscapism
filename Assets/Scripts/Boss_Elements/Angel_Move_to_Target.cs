using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class Angel_Move_to_Target : MonoBehaviour
    {
        [SerializeField] private MonstersHealth angeelHP;
        [SerializeField] private GameObject FireRain;
        [SerializeField] private Transform Spawn;
        [SerializeField] private Transform[] Points;
        private float RandomX;
        private Vector2 SpawnPosition;
        [SerializeField] private float SpawnRate;
        [SerializeField] private Transform LeftPointSpawn;
        [SerializeField] private Transform RightPointSpawn;
        private float NextSpawn = 0f;

        void Update()
        {
            NextSpawn += Time.deltaTime;
            if(angeelHP.currentHealth<=190 && angeelHP.currentHealth>=150)
            {
                transform.position = new Vector3(Points[0].transform.position.x, Points[0].transform.position.y, 0);
            }
            if(angeelHP.currentHealth < 150 && angeelHP.currentHealth >= 100)
            {
                transform.position = new Vector3(Points[1].transform.position.x, Points[1].transform.position.y, 0);
            }
            if (angeelHP.currentHealth < 100 && angeelHP.currentHealth >= 50)
            {
                transform.position = new Vector3(Points[2].transform.position.x, Points[2].transform.position.y, 0);
            }
            if (angeelHP.currentHealth < 50 && angeelHP.currentHealth > 0)
            {
                transform.position = new Vector3(Points[3].transform.position.x, Points[3].transform.position.y, 0);
            }
            if (angeelHP.currentHealth <=0 )
            {
                Destroy(this.gameObject);
            }
            if(NextSpawn>=SpawnRate && angeelHP.currentHealth > 0)
            {
                NextSpawn = 0;
                RandomX = Random.Range(LeftPointSpawn.position.x, RightPointSpawn.position.x);
                SpawnPosition = new Vector2(RandomX, Spawn.transform.position.y);
                Instantiate(FireRain, SpawnPosition, Quaternion.identity);
            }
        }
    }
}

