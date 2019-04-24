using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biomes
{
    city,
    village,
    snow
}
public class LevelController : MonoBehaviour {

    [SerializeField]
    GameObject[] cityChunks;

    [SerializeField]
    GameObject[] villageChunks;

    [SerializeField]
    GameObject[] snowChunks;

    [SerializeField]
    int initialSize = 3;

    Vector3 lastChunkPosition = new Vector3(0,0,15);
         
    Biomes currentBiome = Biomes.city;

    void Start () {

        GameController.instance.StartGame();
		
	}
	
	void Update () {
		
	}

    public void Initialize()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateChunk();
        }
    }

    public void CreateChunk()
    {
        //TODO: cambio de bioma definir como se da.
        if (Time.timeSinceLevelLoad > 5 && currentBiome == Biomes.city )
        {
            currentBiome = Biomes.village;

            Instantiate(villageChunks[0], lastChunkPosition, Quaternion.identity);

            //Update chunk positions
            int chunkSize = villageChunks[0].GetComponent<ChunkController>().chunkSize;
            lastChunkPosition += transform.forward * 10 * chunkSize - (transform.forward * 5);
        }

        //CITY

        //TODO: Refactor this, create dictionary [biome,randomInt]
        if (currentBiome == Biomes.city)
        { 
            //Create random chunk;
            int randomChunk = Random.Range(0, cityChunks.Length);
            Instantiate(cityChunks[randomChunk],lastChunkPosition,Quaternion.identity);

            //Update chunnk positions
            int chunkSize = cityChunks[randomChunk].GetComponent<ChunkController>().chunkSize;
            lastChunkPosition += transform.forward * 10 * chunkSize - (transform.forward * 5);

        }

        //VILLAGE
        if (currentBiome == Biomes.village)
        { 
            //Create random chunk;
            int randomChunk = Random.Range(1, villageChunks.Length);
            Instantiate(villageChunks[randomChunk],lastChunkPosition,Quaternion.identity);

            //Update chunnk positions
            int chunkSize = villageChunks[randomChunk].GetComponent<ChunkController>().chunkSize;
            lastChunkPosition += (transform.forward * 10 * chunkSize) - (transform.forward * 5);

        }

    }
}
