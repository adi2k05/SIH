using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class bl_Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Settings")]
    [SerializeField, Range(1, 15)] private float Radio = 5;
    [SerializeField, Range(0.01f, 1)] private float SmoothTime = 0.5f;
    [SerializeField, Range(0.5f, 4)] private float OnPressScale = 1.5f;
    public Color NormalColor = Color.white;
    public Color PressColor = Color.white;
    [SerializeField, Range(0.1f, 5)] private float Duration = 1;

    [Header("Reference")]
    [SerializeField] private RectTransform StickRect;
    [SerializeField] private RectTransform CenterReference;

    private Vector3 DeathArea;
    private Vector3 currentVelocity;
    private bool isFree = false;
    private int lastId = -2;
    private Image stickImage;
    private Image backImage;
    private Canvas m_Canvas;
    private float diff;
    private Vector3 PressScaleVector;

    void Start()
    {
        if (StickRect == null)
        {
            Debug.LogError("Please assign StickRect!");
            enabled = false;
            return;
        }

        m_Canvas = transform.root.GetComponentInChildren<Canvas>();

        if (m_Canvas == null)
        {
            Debug.LogError("Joystick needs a Canvas in the scene!");
            enabled = false;
            return;
        }

        DeathArea = CenterReference.position;
        diff = CenterReference.position.magnitude;
        PressScaleVector = new Vector3(OnPressScale, OnPressScale, OnPressScale);

        backImage = GetComponent<Image>();
        stickImage = StickRect.GetComponent<Image>();

        if (backImage != null && stickImage != null)
        {
            backImage.CrossFadeColor(NormalColor, 0.1f, true, true);
            stickImage.CrossFadeColor(NormalColor, 0.1f, true, true);
        }
    }

    void Update()
    {
        DeathArea = CenterReference.position;
        if (!isFree) return;

        StickRect.position = Vector3.SmoothDamp(StickRect.position, DeathArea, ref currentVelocity, smoothTime);
        if (Vector3.Distance(StickRect.position, DeathArea) < .1f)
        {
            isFree = false;
            StickRect.position = DeathArea;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (lastId == -2)
        {
            lastId = data.pointerId;
            StopAllCoroutines();
            StartCoroutine(ScaleJoystick(true));
            OnDrag(data);
            if (backImage != null)
            {
                backImage.CrossFadeColor(PressColor, Duration, true, true);
                stickImage.CrossFadeColor(PressColor, Duration, true, true);
            }
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.pointerId == lastId)
        {
            isFree = false;
            Vector3 position = m_Canvas.TouchPosition(GetTouchID);

            if (Vector2.Distance(DeathArea, position) < radio)
                StickRect.position = position;
            else
                StickRect.position = DeathArea + (position - DeathArea).normalized * radio;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        isFree = true;
        currentVelocity = Vector3.zero;

        if (data.pointerId == lastId)
        {
            lastId = -2;
            StopAllCoroutines();
            StartCoroutine(ScaleJoystick(false));
            if (backImage != null)
            {
                backImage.CrossFadeColor(NormalColor, Duration, true, true);
                stickImage.CrossFadeColor(NormalColor, Duration, true, true);
            }
        }
    }

    IEnumerator ScaleJoystick(bool increase)
    {
        float _time = 0;
        while (_time < Duration)
        {
            Vector3 v = StickRect.localScale;
            v = Vector3.Lerp(StickRect.localScale, increase ? PressScaleVector : Vector3.one, (_time / Duration));
            StickRect.localScale = v;
            _time += Time.deltaTime;
            yield return null;
        }
    }

    public int GetTouchID
    {
        get
        {
#if UNITY_EDITOR
            return -1; // in editor we use mouse
#else
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
                return 0; // always return first touch for joystick
            return -1;
#endif
        }
    }

    private float radio { get { return (Radio * 5 + Mathf.Abs((diff - CenterReference.position.magnitude))); } }
    private float smoothTime { get { return (1 - (SmoothTime)); } }

    public float Horizontal { get { return (StickRect.position.x - DeathArea.x) / Radio; } }
    public float Vertical { get { return (StickRect.position.y - DeathArea.y) / Radio; } }
}