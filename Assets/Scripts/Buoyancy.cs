using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private OceanController ocean;
    [Tooltip("When assigned, uses this transform as startPosition")]
    [SerializeField] private Transform startPositionTransform;

    [Header("Settings")]
    [SerializeField] private float halfSizeX = 1f;
    [SerializeField] private float halfSizeZ = 1f;
    [SerializeField] private float buoyancyLerp = 2f;
    [SerializeField] private Vector3 positionOffset =  Vector3.zero;

    [Header("Multipliers")]
    [SerializeField] private float offsetXZMultiplier = 1f;
    [SerializeField] private float offsetYMultiplier = 1f;
    [SerializeField] private float tiltMultiplier = 1f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        if (ocean == null)
        {
            Debug.LogError("Buoyancy: OceanController reference not set!");
            enabled = false;
            return;
        }

        startPosition = transform.position;
        startRotation = transform.rotation;

        // Override with startPositionTransform if assigned
        if (startPositionTransform != null)
        {
            startPosition = startPositionTransform.position;
            startRotation = startPositionTransform.rotation;
        }
    }

    private void Update()
    {
        // Apply startPositionTransform position/rotation if assigned
        if (startPositionTransform != null)
        {
            startPosition = startPositionTransform.position;
            startRotation = startPositionTransform.rotation;
        }

        // Apply positionOffset
        Vector3 basePosition = startPosition + positionOffset;

        // Get the wave displacement at the boat's center
        Vector3 centerOffset = GetWaveDisplacement(basePosition, Time.time);

        // Calculate slope angles using X and Z offsets
        float y = centerOffset.y * offsetYMultiplier;

        float yFront = GetWaveDisplacement(basePosition + new Vector3(halfSizeX, 0, 0), Time.time).y * offsetYMultiplier;
        float ySide = GetWaveDisplacement(basePosition + new Vector3(0, 0, halfSizeZ), Time.time).y * offsetYMultiplier;

        float rotX = Mathf.Atan2(y - yFront, halfSizeX) * Mathf.Rad2Deg * tiltMultiplier;
        float rotZ = Mathf.Atan2(y - ySide, halfSizeZ) * Mathf.Rad2Deg * tiltMultiplier;

        // Apply offset relative to starting position
        Vector3 targetPos = basePosition + new Vector3(
            (centerOffset.x - basePosition.x) * offsetXZMultiplier,
            y,
            (centerOffset.z - basePosition.z) * offsetXZMultiplier
        );

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * buoyancyLerp);

        // Apply rotation relative to startRotation
        Quaternion targetRot = startRotation * Quaternion.Euler(rotX, 0f, rotZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * buoyancyLerp);
    }

    private Vector3 GetWaveDisplacement(Vector3 position, float time)
    {
        Vector3 displaced = position;

        for (int i = 0; i < ocean.waveData.Length; i++)
        {
            Vector2 dir = new Vector2(ocean.waveData[i].x, ocean.waveData[i].y).normalized;
            float wavelength = ocean.waveData[i].z;
            float steepness = ocean.waveData[i].w;
            float speed = ocean.waveSpeeds[i];

            float k = 2f * Mathf.PI / wavelength;
            float a = steepness / k;
            float phase = k * Vector2.Dot(new Vector2(position.x, position.z), dir) - speed * time;

            displaced.x += dir.x * a * Mathf.Cos(phase);
            displaced.z += dir.y * a * Mathf.Cos(phase);
            displaced.y += a * Mathf.Sin(phase);
        }

        return displaced;
    }
}