using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Difficulty
{
    easy = 0,
    medium = 1,
    difficult = 2
}
public class ChunkController : MonoBehaviour
{

    [SerializeField]
    GameObject chunkPrefab;

    [SerializeField]
    Difficulty difficulty;

    public int chunkSize = 1;

    [SerializeField]
    GameObject normal;

    [SerializeField]
    GameObject difficult;


    //PlayerController playerController;

    void Start()
    {

        SetDifficulty();
    }

    void Update()
    {

    }

    //Momento en el que el player llega al final del chunk actual. 
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameController.instance.levelController.CreateChunk();
        }
    }

    private void SetDifficulty()
    {
        difficulty = (Difficulty)Random.Range(0, 3);
        if (difficulty == Difficulty.easy)
        {
            normal.SetActive(false);
            difficult.SetActive(false);
            return;
        }
        if (difficulty == Difficulty.medium)
        {
            difficult.SetActive(false);
            return;
        }
    }
}


