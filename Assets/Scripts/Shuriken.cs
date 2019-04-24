using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {




    [SerializeField]
    float rotationSpeed = 5f;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float duration = 3f;

    //Otra opcion para control de tiempos: control tiempo propio
    float startTime;


    void Start()
    {
        Invoke("DestroyMe", duration);

        //Guardamos el tiempo transcurrido desde el comienzo del juego, cuando se crea este shuriken. 
        //startTime = Time.time;
    }
    //tAct = 3s   //Han pasado 3 segundos desde el comienzo del juego
    //tIni = 1s  //Ha pasado 1 segundo desde el comienzo del juego
    //tDelta = tAct-tIni;

    void Update()
    {
        //Ha transcurrido el tiempo de la duiration
        //if (Time.time - startTime > duration)
        //    Destroy(gameObject);

        //eje global movimiento hacia z
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        //eje local
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Damage();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Obstacle")
            Destroy(gameObject);
    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
    //Low performance: Coroutine
    /*
    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    */
}
