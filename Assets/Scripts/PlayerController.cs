using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Roads
{
    LEFT,
    RIGHT,
    CENTRAL
}

public class PlayerController : MonoBehaviour
{

    public static event Action<bool> OnGroundedChange;

    [SerializeField]
    float speed = 5f;

    public bool isDead = false;

    Transform transformPlayer;

    Animator animator;
    [SerializeField]
    private bool mobileControl = true;

    [SerializeField]
    GameObject shuriken;

    [SerializeField]
    Transform shurikenSpawnPoint;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private float swipeOffset = 20;

    private float _nextFire = 0.0f;
    private Vector2 startPos;

    const float left = -3.5f;
    const float center = 0;
    const float right = 3.5f;
    public bool isGrounded = false;
    private PhysicsSimulation physicsSimulation;

    //Variable con getter, que nunca se le asignara valor solo se puede consultar
    public Roads currentRoad
    {
        get
        {
            if (transformPlayer.position.x == center)
                return Roads.CENTRAL;

            if (transformPlayer.position.x == right)
                return Roads.RIGHT;

            return Roads.LEFT;
        }
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();
        physicsSimulation = GetComponent<PhysicsSimulation>();
    }

    void Update()
    {
        if (!isDead)
            transformPlayer.Translate(transformPlayer.forward * speed * Time.deltaTime);

        if (mobileControl)
            MobileControl();
        else
            KeyBoardControl();


    }

    //PLAYER MOVEMENT
    public void TurnLeftPlayer()
        {
        
            if (transformPlayer.position.x == center)
            {
                transformPlayer.position = new Vector3(left, transformPlayer.position.y, transformPlayer.position.z);
                return;
            }
            if (transformPlayer.position.x == right)
            {
                transformPlayer.position = new Vector3(center, transformPlayer.position.y, transformPlayer.position.z);
            }
        }

        public void TurnRightPlayer()
        {
            if (transformPlayer.position.x == center)
            {
                transformPlayer.position = new Vector3(right, transformPlayer.position.y, transformPlayer.position.z);
                return;
            }
            if (transformPlayer.position.x == left)
            {
                transformPlayer.position = new Vector3(center, transformPlayer.position.y, transformPlayer.position.z);
            }
        }


        private void OnDrawGizmos()
        {
            Vector3 pathLeft = new Vector3(left, transform.position.y, transform.position.z);
            Vector3 pathCenter = new Vector3(center, transform.position.y, transform.position.z);
            Vector3 pathRight = new Vector3(right, transform.position.y, transform.position.z);

            Gizmos.DrawLine(pathLeft, pathLeft + new Vector3(0, 0, 50));
            Gizmos.DrawLine(pathCenter, pathCenter + new Vector3(0, 0, 50));
            Gizmos.DrawLine(pathRight , pathRight + new Vector3(0, 0, 50));
        }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            animator.SetTrigger("Attack");
            Instantiate(shuriken, shurikenSpawnPoint.position, shuriken.transform.rotation);
            _nextFire = Time.time + _fireRate;
        }

    }

    

    private void KeyBoardControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)//TODO: Refactor, crear funcion con este bloqeu de codigo(y el de mas abajo)
            {
                animator.SetTrigger("Jump");
                isGrounded = false;
                if (OnGroundedChange != null) //Por si acaso no hay listeners del evento
                    OnGroundedChange(false); //lanzamos el evento
                physicsSimulation.Jump();
            }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            TurnLeftPlayer();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TurnRightPlayer();
        }

    }

    private void MobileControl()
    {

        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(touch.position);
            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    Debug.Log("start touch");
                    startPos = touch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    Debug.Log("end touch");

                    //La posicion final es mayor(en x) a la posicion inicial: swipe a la derecha
                    //swipeOffset es la cantidad de desplazamiento minima de seguridad para que el swipe se active
                    if (touch.position.x > startPos.x + swipeOffset)
                        {
                            TurnRightPlayer();
                        }
                    //La posicion final es menor(en x) a la posicion inicial: swipe a la izquierda
                    if (touch.position.x < startPos.x - swipeOffset)
                        {
                            TurnLeftPlayer();
                        }

                    //la posicion 0,0 esta situada en la parte superior izquierda de la pantalla (al revés de lo que pasa con la UI)
                    //cuando haces swipe hacia arriba la posición y tiende a disminuir 
                    //en este caso se le resta el swipeOffset en vez de sumarlo
                    if (touch.position.y > startPos.y - swipeOffset)
                        {
                            if (isGrounded)
                            {
                                animator.SetTrigger("Jump");
                                isGrounded = false;
                            if (OnGroundedChange != null)
                                OnGroundedChange(false);
                            physicsSimulation.Jump();
                            }
                        }
                        break;
                    
            }
        }
       

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isDead = true;
            animator.SetTrigger("DeathEnemy");
            GameController.instance.Dead();
        }
        if (collision.gameObject.tag == "Obstacle")

        {
            isDead = true;
            animator.SetTrigger("DeathObstacle");
            GameController.instance.Dead();
        }

        //con esto evitamos el error de salto multiple y solo se puede saltar cuando el player toca al suelo
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            if (OnGroundedChange != null) 
                OnGroundedChange(true);
        }

        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameController.instance.AddCoins();
        }

    }
 

}

