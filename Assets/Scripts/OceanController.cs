using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class OceanController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public bool randomWaves;
    public int numberOfWaves;
    public Vector2 waveLengthRange;
    public Vector2 steepnessRange;
    public Vector2 speedRange;

    public Vector4[] waveData;
    public float[] waveSpeeds;

    Material mat;
    private void Start()
    {
        if (randomWaves)
        {
            waveData = new Vector4[numberOfWaves];
            waveSpeeds = new float[numberOfWaves];
            for (int i = 0; i < numberOfWaves; i++)
            {
                waveData[i] = new Vector4(
                    Random.Range(-1f, 1f), // direction x
                    Random.Range(-1f, 1f), // direction y
                    Random.Range(waveLengthRange.x, waveLengthRange.y), // height
                    Random.Range(steepnessRange.x, steepnessRange.y) // speed
                );
                waveSpeeds[i] = Random.Range(speedRange.x, speedRange.y);
            }
        }

        mat = meshRenderer.material;
        mat.SetFloat("_WavesNum", waveData.Length);
        mat.SetVectorArray("_WaveData", waveData);
        mat.SetFloatArray("_WaveSpeeds", waveSpeeds);
    }

    private void Update()
    {
        transform.position = Vector3.ProjectOnPlane(BoatingManager.instance.player.transform.position, Vector3.up);
    }
}
