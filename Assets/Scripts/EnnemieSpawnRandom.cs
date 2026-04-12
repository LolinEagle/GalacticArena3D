using UnityEngine;
using UnityEngine.AI;

public class EnnemieSpawnRandom : MonoBehaviour{
	[SerializeField] private GameObject	ennemiePrefab;
	[SerializeField] private float		spawnRate;

	private float					spawnRandom;	// Timestamp when to spawn
	private NavMeshTriangulation	t;				// NavMesh data
	private float[]					cumulativeAreas;// Areas of each triangles
	private float					totalArea;		// Total area of the NavMesh

	void	Start(){
		spawnRandom = Time.time + spawnRate;
		t = NavMesh.CalculateTriangulation();

		// Pre-calculate areas for weighted selection
		int	triangleCount = t.indices.Length / 3;
		cumulativeAreas = new float[triangleCount];
		totalArea = 0f;
		for (int i = 0; i < triangleCount; i++){
			Vector3	v1 = t.vertices[t.indices[i * 3]];
			Vector3	v2 = t.vertices[t.indices[i * 3 + 1]];
			Vector3	v3 = t.vertices[t.indices[i * 3 + 2]];

			// Calculate triangle area
			float	area = Vector3.Cross(v2 - v1, v3 - v1).magnitude * 0.5f;
			totalArea += area;
			cumulativeAreas[i] = totalArea;
		}
	}

	private Vector3	GetRandomPointTriangle(Vector3 v1, Vector3 v2, Vector3 v3){
		float	r1 = Random.value;
		float	r2 = Random.value;

		if (r1 + r2 > 1f){
			r1 = 1f - r1;
			r2 = 1f - r2;
		}
		return (v1 + r1 * (v2 - v1) + r2 * (v3 - v1));
	}

	private Vector3	GetRandomPoint(){
		float	randomWeight = Random.Range(0f, totalArea);

		// Find which triangle this weight falls into
		int	index = System.Array.BinarySearch(cumulativeAreas, randomWeight);
		if (index < 0) index = ~index;

		// Get vertices of the selected triangle
		Vector3	a = t.vertices[t.indices[index * 3]];
		Vector3	b = t.vertices[t.indices[index * 3 + 1]];
		Vector3	c = t.vertices[t.indices[index * 3 + 2]];

		// Generate a random point within that specific triangle
		return (GetRandomPointTriangle(a, b, c));
	}

	void	Update(){
		if (Time.time >= spawnRandom){
			Instantiate(ennemiePrefab, GetRandomPoint(), Quaternion.identity);
			spawnRandom = Time.time + spawnRate;
		}
	}
}
