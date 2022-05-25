using System.Net.Mime;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //скорость игрока
    public float health;
    public Text healthDisplay;
    private Rigidbody2D rb; // его риджитбоди
    private Vector2 moveInput; // считываение в какую сторону мы движемся
    private Vector2 moveVelocity; //итоговая скорость игрока в каком-то направлении
    public Animator anim; // указываем аниматор    
    public GameObject PlayerEffect; // эффект
    public GameObject Zelye; // эффект
    public GameObject effect;
    private bool facingRight = true; // true когда игрок смотрит вправо
    public GameObject shield;
    [Header("Weapons")]
    public List<GameObject> unlockedWeapons; // лист с разблок пушками
    public GameObject[] allWeapons; // массив со всеми пушками
    public Image weaponIcon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
        anim = GetComponent<Animator>(); //указываем аниматор в стартовой функции
        
    }

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
        if(Input.GetKeyDown(KeyCode.Q)){
            SwitchWeapons();
        }
        healthDisplay.text = "HP: " + health;
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

    // public void ChangeHealth(int healthValue){
    //     health += healthValue;
    //     healthDisplay.text = "HP: " + health;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Zelye")){
            health += 5;
            healthDisplay.text = "HP: " + health;
            Instantiate(Zelye, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        } else if(other.CompareTag("Shield")) { // если коснулись щита он активируется и уничтожается иконка
            shield.SetActive(true);
            Destroy(other.gameObject);
        } else if(other.CompareTag("Gun2")) {
            for(int i = 0; i < allWeapons.Length; i++){
                if(other.name == allWeapons[i].name){
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            SwitchWeapons();
            Destroy(other.gameObject);
        }
    }

    public void SwitchWeapons() { // меняем оружия
        for(int i = 0; i < unlockedWeapons.Count; i++){
            if(unlockedWeapons[i].activeInHierarchy){
                unlockedWeapons[i].SetActive(false);
                if(i != 0){
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                } else {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite; 
                }
                weaponIcon.SetNativeSize();
                break;
            }
        }
    }
}
