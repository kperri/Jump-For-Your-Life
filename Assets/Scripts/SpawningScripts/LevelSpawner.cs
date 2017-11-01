using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject fire;

    Vector3 spawnWall;
    Vector3 spawnerPosition;
    Vector3 fireRotation;
    Quaternion rightWallRotation;
    bool fireOneRight;
    bool fireTwoRight;
    bool fireThreeRight;
    float wallLength;
    float fireAndWallXPosition;
    GameObject player;
    GameObject firePool;
    Component[] allTheFires;
    float randomNumber;
    float randomNumber2;
    float randomNumber3;
    float randomNumber4;
    int numberOfFires;

    // Use this for initialization
    void Start ()
    {
        spawnWall = new Vector3 (-2.8f, 14.43f, 0);
        spawnerPosition = new Vector3 (0, 10, 0);
        rightWallRotation = new Quaternion (180.0f, 0.0f, 0.0f, 0.0f);
        wallLength = 9.62f;
        fireAndWallXPosition = 2.8f;
        player = GameObject.FindGameObjectWithTag ("Player");
        UnityEngine.Random.InitState ((int)System.DateTime.Now.Ticks);
        fireRotation = new Vector3 (0, 180, 0);
        PoolManager.instance.CreateNewPool (rightWall, 10);
        PoolManager.instance.CreateNewPool (leftWall, 10);
        PoolManager.instance.CreateNewPool (fire, 40);
        allTheFires = new Component[41];
        firePool = GameObject.Find ("fire pool");
        allTheFires = firePool.GetComponentsInChildren (typeof (Transform), true);
    }

    void Update ()
    {
        // creating random numbers to check which wall to randomly instantiate fires onto
        randomNumber = UnityEngine.Random.Range (0f, 1f);
        randomNumber2 = UnityEngine.Random.Range (0f, 1f);
        randomNumber3 = UnityEngine.Random.Range (0f, 1f);
        randomNumber4 = UnityEngine.Random.Range (0f, 1f);
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        // When the player collides with a Spawn Wall it will then use the Spawn and SpawnFire function to instaniate more walls and fires above the previous ones
        if (coll.CompareTag ("Player"))
        {
            //Resources.UnloadUnusedAssets ();
            spawnerPosition.y += wallLength;
            transform.position = spawnerPosition;
            Spawn ();
            SpawnFire ();
            MoveFirePosition ();
        }
    }

    void Spawn ()
    {
        // Creates new walls aboves the players position so that they may continue the game indefinitely.
        spawnWall.y += wallLength;
        PoolManager.instance.ReuseObject (leftWall, spawnWall, rightWallRotation);
        spawnWall.x = fireAndWallXPosition;
        PoolManager.instance.ReuseObject (rightWall, spawnWall, rightWallRotation);
        spawnWall.x = -fireAndWallXPosition;
    }

    void SpawnFire ()
    {
        // this set of If Else tells the game to only spawn 3-4 fires 
        // if the player hasn't reached a Score of 500 yet
        // if the player has reached 500 the game will proceed to spawn 4 fires each time
        if (player.transform.position.y >= 500f)
        {
            numberOfFires = 4;
        }
        else if (randomNumber > 0.8f)
        {
            numberOfFires = 4;
        }
        else if (randomNumber <= 0.8f)
        {
            numberOfFires = 3;
        }

        /* 
        This set of if else statements asks to check the Random number generated at the time this function was called and check to see if it was above or below 0.5.
		If below 0.5 it will spawn a fire on the right wall. If above 0.5 it will spawn a fire on the left wall
		Each new fire generated is checked by a new Random Number to avoid spawning all the fires on the same wall.
		The fires are then Instantiated using the fire prefab, a Vector3 that takes in either 3 or -3 for x depending on the wall
		The y position for the Vector3 Linerally Interpolates between the top of the wall and the bottom of the wall using the same random number used to check which wall the fire should be spawned onto
		The z position is the same as the fire prefab's z position and finally it gives a rotation of either (0, 0, 0) on the left wall or (0, 180, 0) on the right wall
		This ensures that the fires on the right wall are facing the right direction in the game
		Wall half length = 3.675 Full Length = 7.35 at 0.75 scale
		Wall half length = 4.9 Full length = 9.8 at 1 scale
        */
        if (randomNumber < 0.5f && numberOfFires >= 1)
        {
            fireOneRight = true;
            PoolManager.instance.ReuseObject (fire, new Vector3 (fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber), fire.transform.position.z), Quaternion.Euler (fireRotation));
        }
        else if (randomNumber >= 0.5f && numberOfFires >= 1)
        {
            fireOneRight = false;
            PoolManager.instance.ReuseObject (fire, new Vector3 (-fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber), fire.transform.position.z), fire.transform.rotation);
        }
        if (randomNumber2 < 0.5f && numberOfFires >= 2)
        {
            fireTwoRight = true;
            PoolManager.instance.ReuseObject (fire, new Vector3 (fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber2), fire.transform.position.z), Quaternion.Euler (fireRotation));
        }
        else if (randomNumber2 >= 0.5f && numberOfFires >= 2)
        {
            fireTwoRight = false;
            PoolManager.instance.ReuseObject (fire, new Vector3 (-fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber2), fire.transform.position.z), fire.transform.rotation);
        }
        if (((!fireOneRight && fireTwoRight) || (!fireOneRight && !fireTwoRight)) && numberOfFires >= 3)
        {
            fireThreeRight = true;
            PoolManager.instance.ReuseObject (fire, new Vector3 (fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber3), fire.transform.position.z), Quaternion.Euler (fireRotation));
        }
        else if (((fireOneRight && !fireTwoRight) || (fireOneRight && fireTwoRight)) && numberOfFires >= 3)
        {
            fireThreeRight = false;
            PoolManager.instance.ReuseObject (fire, new Vector3 (-fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber3), fire.transform.position.z), fire.transform.rotation);
        }
        if (((!fireOneRight && !fireTwoRight) || (!fireOneRight && !fireThreeRight) || (!fireTwoRight && !fireThreeRight)) && numberOfFires >= 4)
        {
            PoolManager.instance.ReuseObject (fire, new Vector3 (fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber4), fire.transform.position.z), Quaternion.Euler (fireRotation));
        }
        else if (((fireOneRight && fireTwoRight) || (fireOneRight && fireThreeRight) || (fireTwoRight && fireThreeRight)) && numberOfFires >= 4)
        {
            PoolManager.instance.ReuseObject (fire, new Vector3 (-fireAndWallXPosition, Mathf.Lerp (spawnWall.y - 4.215f, spawnWall.y + 4.215f, randomNumber4), fire.transform.position.z), fire.transform.rotation);
        }
    }

    void MoveFirePosition ()
    {
        //fire half length = 0.685 full length = 1.37
        // 4.215 is the highest point on a wall subtracted by half the fire length to avoid merging two fires spawned on separate walls
        for (int i = 0; i < allTheFires.Length; i++)
        {
            for (int j = 0; j < allTheFires.Length; j++)
            {
                if (allTheFires[i] == allTheFires[j])
                {
                    continue;
                }
                else if (allTheFires[i] != null && allTheFires[j] != null && allTheFires[i].gameObject.activeSelf && allTheFires[j].gameObject.activeSelf && Mathf.Abs (allTheFires[i].transform.position.y - allTheFires[j].transform.position.y) <= 1.5 && allTheFires[i].transform.position.x == allTheFires[j].transform.position.x)
                {
                    if (allTheFires[j].transform.position.y >= spawnWall.y)
                    {
                        Vector3 newPosition;
                        newPosition = new Vector3 (allTheFires[j].transform.position.x, 0, allTheFires[j].transform.position.z);
                        newPosition.y = UnityEngine.Random.Range (spawnWall.y - 4.215f, spawnWall.y - 0.685f);
                        allTheFires[j].transform.position = newPosition;
                    }
                    else if (allTheFires[j].transform.position.y < spawnWall.y)
                    {
                        Vector3 newPosition;
                        newPosition = new Vector3 (allTheFires[j].transform.position.x, 0, allTheFires[j].transform.position.z);
                        newPosition.y = UnityEngine.Random.Range (spawnWall.y + 0.685f, spawnWall.y + 4.215f);
                        allTheFires[j].transform.position = newPosition;
                    }
                    MoveFirePosition ();
                }
            }
        }
    }
}