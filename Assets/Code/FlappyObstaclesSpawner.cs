using UnityEngine;
using System.Collections.Generic;

public class FlappyObstaclesSpawner : MonoBehaviour {

	public GameObject obstaclePrefab;

	// We are spawning obstacles equidistantly.
	// The distance is defined by this variable.
	public float distanceBetweenObstacles = 10f;

	// How far away from the starting position the first obstacle should be spawned.
	public float distanceToFirstObstacle = 20f;

	// Distance to the right or left from the camera X position at which
	// obstacles will be spawned or destroyed, respectively.
	// Should be big enough so that spawning and destroying don't happen
	// on the screen.
	public float spawnOffset = 20f;

	// Transform to use as a reference for spawning and destroying obstacles.
	// Main camera's transform as a default, as it's in the middle of the screen.
	public Transform center;

	// Spawned obstacles have random Y position and gap height.
	// Min and Max values are defined by these variables.
	public float obstacleMinY = -4f;
	public float obstacleMaxY = 4f;
	public float obstacleMinGapHeight = 4f;
	public float obstacleMaxGapHeight = 8f;

	// Where we should spawn the next obstacle.
	private float nextSpawnX;


	// Queue is more apropriate here as we will always remove only the oldest element.
	private Queue<GameObject> spawnedObstacles = new Queue<GameObject>();

	// Set up the initial values.
	void Start() {
		// Get main camera's transform if center not explicitly set.
		if (center == null) {
			center = Camera.main.transform;
		}
		// Set the position of the first Spawn.
		nextSpawnX = distanceToFirstObstacle;
	}

	void FixedUpdate()
	{
		if (ShouldSpawn())
		{
			// Set obstacle position and gap height randomly.
			float nextSpawnY = Random.Range(obstacleMinY, obstacleMaxY);
			float nextSpawnGapHeight = Random.Range(obstacleMinGapHeight, obstacleMaxGapHeight);

			// Spawn the obstacle.
			Spawn(nextSpawnX, nextSpawnY, nextSpawnGapHeight);

			// Set the next obstacle spawn position.
			nextSpawnX += distanceBetweenObstacles;

			// Destroy old obstacles.
			CleanUp();
		}
	}

	// Checks if we are close enough to the next spawn point
	// to spawn next obstacle.
	private bool ShouldSpawn() {
		return center.position.x + spawnOffset > nextSpawnX;
	}

	// Checks if given obstacle is far enough to be destroyed.
	private bool ShouldDestroy(GameObject obstacle)
	{
		return center.position.x - spawnOffset > obstacle.transform.position.x;
	}

	// Destroys the oldest obstacle if it should be destroyed.
	private void CleanUp()
	{
		if (spawnedObstacles.Count > 0 && ShouldDestroy(spawnedObstacles.Peek()))
		{
			Destroy(spawnedObstacles.Dequeue());
		}
	}

	void Spawn( float x, float y, float gapHeight ) {
		GameObject spawned = GameObject.Instantiate( obstaclePrefab );
        spawned.transform.parent = transform;
		spawned.transform.position = new Vector3( x, y, 0 );
		spawnedObstacles.Add( spawned );

        Transform bottomTransform = spawned.transform.FindChild( "Bottom" );
        Transform topTransform = spawned.transform.FindChild( "Top" );
        float bottomY = -gapHeight/2;
        float topY = +gapHeight/2;
        bottomTransform.localPosition = Vector3.up * bottomY;
        topTransform.localPosition = Vector3.up * topY;
    }

}
