using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector3 positionOffset = new Vector3(0, 6, -10);
    [SerializeField] Vector3 lookAtOffset = new Vector3(0, 2, 0);
    [SerializeField] float moveDuration = .7f;
    [SerializeField] float lookAtDuration = .7f;
    
    void Update()
    {
        DOTween.Kill(transform);
        transform.DOMove(playerController.transform.position + positionOffset, moveDuration).SetEase(Ease.Linear);
        transform.DOLookAt(playerController.transform.position + lookAtOffset, lookAtDuration).SetEase(Ease.Linear);
    }
}
