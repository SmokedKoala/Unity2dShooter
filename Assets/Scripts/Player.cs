using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //скорость игрока
    public float health;
    private Rigidbody2D rb; // его риджитбоди
    private Vector2 moveInput; // считываение в какую сторону мы движемся
    private Vector2 moveVelocity; //итоговая скорость игрока в каком-то направлении
    private Animator anim; // указываем аниматор    
    public GameObject PlayerEffect; // эффект
    public GameObject effect;
    private Animator camAnim;
    private bool facingRight = true; // true когда игрок смотрит вправо
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
        anim = GetComponent<Animator>(); //указываем аниматор в стартовой функции
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // здесь moveInput считывает горизонтальное и вертикальное значение 
        moveVelocity = moveInput.normalized * speed;

        if(moveInput.x == 0){ //если мы стоим на месте
            anim.SetBool("isRunning", false); //проигрывается стандартаня анимация, когда мы стоим
        }
        else {
            anim.SetBool("isRunning", true);//иначе анимация бега
        }

        if(!facingRight && moveInput.x > 0){ // если мы смотрим влево и идем вправо
            Flip(); // то происходит разворот
        } else if(facingRight && moveInput.x < 0){ // если мы смотрим вправо и идем влево
            Flip(); // то происходит разворот
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip(){ // разворачивает игрока в другую сторону
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
