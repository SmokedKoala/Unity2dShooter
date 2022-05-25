using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies; // спавн врагов
    public Transform[] enemySpawners; // спавн точек красных

    [Header("Powerups")]
    public GameObject zelye;

    [HideInInspector] public List<GameObject> enemiesList; // динамический лист врагов

    private bool spawned; //проверка проспавнились ли враги

    private void Start(){
        GameObject.FindGameObjectsWithTag("Room");
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !spawned){
            spawned = true;
            foreach(Transform spawner in enemySpawners) {
                int rand = Random.Range(0, 11);
                if(rand < 9) {
                    GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemyy = Instantiate(enemy, spawner.position, Quaternion.identity) as GameObject;
                    enemyy.transform.parent = transform;
                    enemiesList.Add(enemyy);
                }else if(rand == 9){
                    Instantiate(zelye, spawner.position, Quaternion.identity);
                }
            }
            StartCoroutine(CheckEnemies()); 
        }
    }
    IEnumerator CheckEnemies(){ // проверка кол врагов
        yield return new WaitForSeconds(1f); // секунда для спавна врагов
        yield return new WaitUntil(() => enemiesList.Count == 0);
    }
}
