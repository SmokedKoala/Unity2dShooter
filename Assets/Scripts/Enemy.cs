using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; // здоровье врага
    public float speed; // скорость врага
    private Animator anim; // анимация для врага
    private void Update() { // движение врага на героя
        anim = GetComponent<Animator>();
        if(health <= 0){ // если у врага не осталось здоровья
            Destroy(gameObject); // он уничтожается
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage) {
        health -= damage; // вычитаем из здоровья врага урон, который он получает
    }
}
