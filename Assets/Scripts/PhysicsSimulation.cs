using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulation : MonoBehaviour {

    //Ya que estamos en un continuo de tiempo, la formula de v=s/t, se puede usar sin la t, con lo que la variacion de la 
    //posicion es igual a la velocidad (y en el caso del salto).
    [SerializeField]
    private float jumpForce = 30;

    private float jumpSpeedY = 0;

    private Transform transformPlayer;

    private PlayerController playerController;
    bool jumping = false;

    void Awake()
    {
        PlayerController.OnGroundedChange += GroundAction;
    }

    void Start () {
        transformPlayer = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
	}
	
	void Update () {
	    if (jumping)
        {
            transformPlayer.Translate(transformPlayer.up * jumpSpeedY * Time.deltaTime);

            //Segundo sistema de deteccion de suelo, refuerzo del OnColision
            if (transformPlayer.position.y < 0.1f)
            {
                transformPlayer.position = new Vector3(transformPlayer.position.x, 0.1f, transformPlayer.position.z);
                playerController.isGrounded = true;
            }
            
            jumpSpeedY += Physics.gravity.y * Time.deltaTime;        
        }
        else //si no estamos saltando bloqueamos la posicion Y para que el player no salga volando
        {
            //TODO: esto funciona tambien para la village?
            if (playerController.currentRoad.Equals(Roads.CENTRAL))
                transformPlayer.position = new Vector3(transformPlayer.position.x, 0.1f, transformPlayer.position.z);
            else
                transformPlayer.position = new Vector3(transformPlayer.position.x, 0.3f, transformPlayer.position.z);
        }
    }

    public void Jump()
    {
        //Physics.gravity //-9.8m/s
        jumping = true;
        //en el momento del salto la velocidad se iguala a la fuerza de inicio,luego se le va restando la gravedad
        jumpSpeedY = jumpForce;


    }
    public void GroundAction(bool isGrounded)
    {
        if (isGrounded)
            jumping = false;
    }
}
