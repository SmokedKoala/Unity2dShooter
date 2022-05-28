using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // здоровье врага
    public float speed; // скорость врага
    public Animator anim; // анимация для врага
    public GameObject PlayerEffect;
    private float timeBtwAttack; // добавляем врагу перезарядку
    public float startTimeBtwAttack; // 
    public int damage; // урон врагу
    private float stopTime; //оставнока врага на некоторое время при попадании в него
    public float startStopTime;
    //public float normalSpeed; // обычная скорость врага
    private Player player;
    public AudioSource TakeDamageSound;


    [HideInInspector] public bool playerNotInRoom; //гг нет в комнате
    private bool stoppedEnemy; //остановка врага

    private Transform f;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }
    private void Update() { // движение врага на героя
        //чтобы враг останавливался ненанодлго
        if(!playerNotInRoom){
            if(stopTime <= 0) {
                stoppedEnemy = false; 
            } else {
                stoppedEnemy = true;
                stopTime -= Time.deltaTime;
            }
        }else {
            stoppedEnemy = true;
        }
        anim = GetComponent<Animator>();
        if(health <= 0){ // если у врага не осталось здоровья
            Instantiate(PlayerEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // он уничтожается
            player.counter ++;
        }

        // if(player.transform.position.x > transform.position.x){ // если игрок разворачивается
        //     transform.eulerAngles = new Vector3(0, 180, 0); //враг смотрит в соотв. сторону
        // } else {
        //     transform.eulerAngles = new Vector3(0, 0, 0);
        // }
        //transform.Translate(Vector2.left * speed * Time.deltaTime); // тупо идет влево
        if(!stoppedEnemy){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); //ходит за главным героем
        }
    }

    public void TakeDamage(int damage) {
        stopTime = startStopTime;
        health -= damage; // вычитаем из здоровья врага урон, который он получает
        TakeDamageSound.Play();
    }

    public void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(timeBtwAttack <= 0){
                anim.SetTrigger("enemyAttack");
            }else {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack(){ // атака врага
        Instantiate(PlayerEffect, player.transform.position, Quaternion.identity);
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }

}
