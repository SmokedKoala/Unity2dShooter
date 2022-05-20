using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float speed; //скорость пули
   public float lifetime; // время существования патрона
   public float distance; //дистанцию, на которой патрон летит
   public int damage; // урон от пули
   public LayerMask whatIsSolid; // что пуля будет пробивать (что считать твердым)


    private void Update(){
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid); // нахождение объекта для пробития
        if(hitInfo.collider != null){ //если пуля столкнулась с каким-то коллайдером
            if(hitInfo.collider.CompareTag("Enemy")){ // и у коллайдера тег "Enemy" 
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage); // наносим урон врагу
            }
            Destroy(gameObject); //уничтожаем патрон
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime); //двжиение патрона
    }
}
