using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // здоровье врага
    public float speed; // скорость врага
    private Animator anim; // анимация для врага

    private float timeBtwAttack; // добавляем врагу перезарядку
    public float startTimeBtwAttack; // 
    public int damage; // урон врагу
    private float stopTime; //оставнока врага на некоторое время при попадании в него
    public float startStopTime;
    public float normalSpeed; // обычная скорость врага
    private Player player;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }
    private void Update() { // движение врага на героя
        //чтобы враг останавливался ненанодлго
        if(stopTime <= 0) {
            speed = normalSpeed; 
        } else {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        anim = GetComponent<Animator>();
        if(health <= 0){ // если у врага не осталось здоровья
            Destroy(gameObject); // он уничтожается
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage) {
        stopTime = startStopTime;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        health -= damage; // вычитаем из здоровья врага урон, который он получает
    }

    public void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(timeBtwAttack <= 0){
                anim.SetTrigger("EnemyAttack");
            }else {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack(){ // атака врага
        //Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }

}
