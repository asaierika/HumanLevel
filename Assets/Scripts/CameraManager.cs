using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // main camera is the cam that will follow player around
    public static CameraManager main;
    [SerializeField] private string label = "main";
    public Transform player;
    private Vector3 pos;
    public float minX = -2f;
    public float maxX = 5f;
    public float minY = -1f;
    public float maxY = 0f;
    [SerializeField] private float transitSpeed;
    private float camZ = -1;
    // Whether camera is in the process of switching to a new character
    private bool inTransit = false;
    public Action onCameraTransit;
    public Action onCameraTransitComplete;

    void Awake() {
        if (main == null && label.Equals("main")) {
            Debug.Log("assigned main camera");
            main = this;
        }
    }

    void Start() {
        // Default follows Kizuna Body
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, TrackDemi);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, TrackSpirit);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, TrackPartner);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, TrackDemi);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, TrackSpirit);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, TrackPartner);
    }

    private void LateUpdate() {
        if (inTransit) return;

        pos = transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y;

        if (pos.x < minX) pos.x = minX;

        if (pos.x > maxX) pos.x = maxX;
       
        if (pos.y < minY) pos.y = minY;

        if (pos.y > maxY) pos.y = maxY;

        transform.position = pos;
    }

    void TrackDemi(object input = null) {
        StartCoroutine(TransitToFollowNextCharacter(GameObject.FindWithTag("Player").transform));
    }

    void TrackSpirit(object input = null) {
        StartCoroutine(TransitToFollowNextCharacter(GameObject.FindWithTag("Spirit").transform));
    }

    void TrackPartner(object input = null) {
        StartCoroutine(TransitToFollowNextCharacter(GameObject.FindWithTag("Partner").transform));
    }

    // Moves directly to follow the character the player is playing as 
    IEnumerator TransitToFollowNextCharacter(Transform nextCharacter) {
        inTransit = true;
        CharacterMovement.playerFrozen = true;
        onCameraTransit?.Invoke();

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = nextCharacter.position;
        targetPosition.z = camZ;
        targetPosition.x = Mathf.Max(Mathf.Min(maxX, targetPosition.x), minX);
        targetPosition.y = Mathf.Max(Mathf.Min(maxY, targetPosition.y), minY);

        float transitDuration = (targetPosition - startPosition).magnitude / transitSpeed;
        float timeElapsed = 0;
        while (timeElapsed / transitDuration < 1) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / transitDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        player = nextCharacter;
        CharacterMovement.playerFrozen = false;
        inTransit = false;
        onCameraTransitComplete?.Invoke();
    }
}
