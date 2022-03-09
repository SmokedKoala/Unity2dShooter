using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset; // велчина для ручной корректировки вращения ружья

    public GameObject bullet; //патрон
    public Transform shotPoint; //дуло пушки 

    private float timeBtwShots; //время между выстрелами
    public float startTimeBtwShots; // 

    void Update()
    { // рассчитываем позицию курсора мыши и заставляем пушку вращаться в его направлении
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);       

        if(timeBtwShots <= 0){ //выстрел происходит только когда перезарядка кончилась
            if(Input.GetMouseButton(0)){ // если нажата левая кнопка мыши появляется пуля в позии shotpoint, с тем же вращением, что и пушка
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots; //после выстрела начать перезарядку
            }
        } else { //иначе перезарядка продолжается
            timeBtwShots -= Time.deltaTime; 
        }
    }
}
