using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; //скорость игрока

    private Rigidbody2D rb; // его риджитбоди

    private Vector2 moveInput; // считываение в какую сторону мы движемся

    private Vector2 moveVelocity; //итоговая скорость игрока в каком-то направлении
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // здесь moveInput считывает горизонтальное и вертикальное значение
        moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
