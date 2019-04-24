using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Animator animator;
    bool isEnemyDead = false;
    [SerializeField]
    int health = 1;

	void Start () {
        animator = GetComponent<Animator>();
    }

    void Update()
    {



#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            animator.SetTrigger("Attack");
        }
       
        if (Input.GetKeyDown(KeyCode.F2))
        {
            DeathEnemy();
        }

#else
        //Si estamos ejecutabdo en movil
#endif
    }

    void DeathEnemy()
    {
        isEnemyDead = true;
        animator.SetTrigger("Death");
        GetComponent<Collider>().enabled = false;
    }

    public void Damage()
    {
            health--;
        if (health <= 0)
            DeathEnemy();
    }
}
