using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public Transform playerTransform;
    public Transform[] groundSegments;
    public float segmentLength = 1000f;

    private float spawnZ = 0f;

    void Start()
    {
        foreach (Transform segment in groundSegments)
        {
            segment.position = new Vector3(0, -10f, spawnZ);
            spawnZ += segmentLength;
        }
    }

    void Update()
    {
        moveGroundSegments();
    }

    void moveGroundSegments()
    {
        foreach (Transform segment in groundSegments)
        {
            if (playerTransform.position.z - segment.position.z > segmentLength)
            {
                segment.position = new Vector3(0, -10f, spawnZ);
                spawnZ += segmentLength;
            }
        }
    }
}