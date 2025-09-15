using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishingMinigame : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float visibilityTweenDuration = .3f;
    [SerializeField] private float cursorStartWait = .4f;
    [SerializeField] private float cursorSpeedIncreaseDuration = .3f;
    [SerializeField] private float cursorSpeedDecreaseDuration = .3f;
    [SerializeField] private float cursorMaxSpeed = 80f;
    [SerializeField] private float zoneSizeDegrees = 46f;
    [SerializeField] private float completeZoneTweenDuration = .3f;
    [SerializeField] private float cursorColorTweenDuration = .3f;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private Color defaultCursorColor = new Color(.5f, .5f, .5f, 1);
    
    [Header("Instances")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform zonePrefab;
    [SerializeField] RectTransform cursor;
    [SerializeField] Image cursorImage;
    
    List<RectTransform> currentZones = new  List<RectTransform>();
    private float cursorSpeed = 0;
    private bool playingMinigame = false;
    
    void TweenVisible()
    {
        canvasGroup.DOFade(1, visibilityTweenDuration).SetEase(Ease.OutBack);
        transform.DOScale(Vector3.one, visibilityTweenDuration).SetEase(Ease.OutBack);
    }
    
    void TweenInvisible()
    {
        canvasGroup.DOFade(0, visibilityTweenDuration * .6f).SetEase(Ease.InCubic);
        transform.DOScale(Vector3.zero, visibilityTweenDuration * .6f).SetEase(Ease.InCubic);
    }

    void CorrectCursorClickTween()
    {
        IEnumerator CorrectCursorClickTweenIEnumerator()
        {
            cursorImage.DOColor(correctColor, .05f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(.05f + .1f);
            
            if (playingMinigame) { cursorImage.DOColor(defaultCursorColor, cursorColorTweenDuration).SetEase(Ease.InCubic); }
        }
        
        StartCoroutine(CorrectCursorClickTweenIEnumerator());
    }
    
    void CompleteZone(RectTransform zone)
    {
        IEnumerator CompleteZoneIEnumerator()
        {
            currentZones.Remove(zone);
            if (currentZones.Count == 0)
            {
                StopMinigame();
                print("WON");
            }
            
            zone.GetComponent<Image>().DOFade(0, completeZoneTweenDuration).SetEase(Ease.InCubic);
            
            yield return new WaitForSeconds(completeZoneTweenDuration);

            if (zone.gameObject)
            {
                Destroy(zone.gameObject);
            }
        }
        
        StartCoroutine(CompleteZoneIEnumerator());
    }
    
    void IncorrectCursorClickTween()
    {
        cursorImage.DOColor(incorrectColor, .05f).SetEase(Ease.OutBack);
    }
    
    float To360Angle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f; 
        return angle;
    }

    void SpawnZones(int amount)
    {
        // Reset currentZones list
        for (int i = 0; i < currentZones.Count; i++)
        {
            Destroy(currentZones[i].gameObject);
        }
        currentZones = new List<RectTransform>();
        
        // Get angles
        float[] GetAngles(int amount)
        {
            float[] angles = new float[amount];
            float startAngle = Random.Range(0, 360/amount); //Random.Range(5, (360/amount)-zoneSizeDegrees-5);
            
            for (int i = 0; i < amount; i++)
            {
                float angle = (startAngle + i * (360f / amount)) % 360f;
                angles[i] = angle;
            }
            
            return angles;
        }
        
        float[] angles = GetAngles(amount);
        
        // Spawn Zones
        for (int i = 0; i < amount; i++)
        {
            RectTransform zoneClone = Instantiate(zonePrefab, Vector3.one, Quaternion.identity);
            zoneClone.SetParent(transform);
            zoneClone.rotation = Quaternion.Euler(0, 0, angles[i]);
            zoneClone.localScale = Vector3.one;
            zoneClone.offsetMin = Vector2.zero;
            zoneClone.offsetMax = Vector2.zero;
            zoneClone.SetSiblingIndex(i + 1);
            currentZones.Add(zoneClone);
        }
        
        cursor.SetSiblingIndex(amount+1);
    }
    
    [Button]
    public void StartMinigame()
    {
        // Prepare
        cursorSpeed = 0;
        cursor.rotation = Quaternion.Euler(0, 0, 0);
        cursorImage.color = defaultCursorColor;
        SpawnZones(3);
        
        // Visible
        TweenVisible();

        IEnumerator StartCursor()
        {
            yield return new WaitForSeconds(cursorStartWait);
            
            DOTween.To(
                () => cursorSpeed,
                x => cursorSpeed = x,
                cursorMaxSpeed,
                cursorSpeedIncreaseDuration
            ).SetEase(Ease.OutCubic);

            playingMinigame = true;
            
            yield return new WaitForSeconds(cursorStartWait);
        }
        StartCoroutine(StartCursor());
    }
    
    [Button]
    void StopMinigame()
    {
        IEnumerator StopMinigameIEnumerator()
        {
            DOTween.To(
                () => cursorSpeed,
                x => cursorSpeed = x,
                0,
                cursorSpeedDecreaseDuration
            ).SetEase(Ease.OutCubic);
            
            yield return new WaitForSeconds(cursorSpeedDecreaseDuration * .7f);
            
            TweenInvisible();
        }

        playingMinigame = false;
        StartCoroutine(StopMinigameIEnumerator());
    }

    void Start()
    {
        canvasGroup.alpha = 0;
        transform.localScale =  Vector3.zero;
    }

    void Update()
    {
        cursor.rotation = Quaternion.Euler(0, 0, cursor.eulerAngles.z - (cursorSpeed * Time.deltaTime));
        
        if (Input.GetMouseButtonDown(0) && playingMinigame)
        {
            // Check if a zone at current cursor position
            float currentCursorAngle = To360Angle(cursor.eulerAngles.z);
            RectTransform currentInZone = null;

            for (int i = 0; i < currentZones.Count; i++)
            {
                float currentZoneAngle = To360Angle(currentZones[i].eulerAngles.z);

                if (
                    (currentCursorAngle <= currentZoneAngle && currentCursorAngle >= currentZoneAngle - zoneSizeDegrees) ||
                    (currentCursorAngle >= 360 - zoneSizeDegrees && currentCursorAngle >= 360 + (currentZoneAngle - zoneSizeDegrees))
                    ) // Doesn't account for zone being partially through 0.
                {
                    currentInZone = currentZones[i];
                    break;
                }
            }

            if (currentInZone != null)
            {
                CorrectCursorClickTween();
                CompleteZone(currentInZone);
            }
            else
            {
                IncorrectCursorClickTween();
                StopMinigame();
                print("LOST");
            }
        }
    }
}
