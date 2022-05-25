using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;

    void Start(){
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){ // если в зону вошле игрок
            other.transform.position += playerChangePos; // то к его позиции прибавятся изменения
            cam.transform.position += cameraChangePos; // и с камерой тоже
        }
    }
}
