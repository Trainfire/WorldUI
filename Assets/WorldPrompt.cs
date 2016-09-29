using UnityEngine;
using Framework;

[RequireComponent(typeof(WorldElement))]
class WorldPrompt : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private float _slideOffset;

    private WorldElement _worldElement;
    private CanvasGroup _canvasGroup;
    private TweenFloat _tweenAlpha;
    private TweenVector2 _tweenVector;

    void Start()
    {
        _worldElement = GetComponent<WorldElement>();
        _worldElement.Showed += OnShow;
        _worldElement.Hidden += OnHide;

        _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        _tweenAlpha = new TweenFloat();
        _tweenAlpha.Duration = 0.1f;
        _tweenAlpha.OnTweenValue += OnTweenAlpha;

        _tweenVector = new TweenVector2();
        _tweenVector.Duration = 0.1f;
        _tweenVector.OnTweenValue += OnTweenSlider;
    }

    void OnShow()
    {
        _tweenAlpha.From = _canvasGroup.alpha;
        _tweenAlpha.To = 1f;
        _tweenAlpha.Play();

        _tweenVector.From = Vector2.zero;
        _tweenVector.To = Vector2.up * _slideOffset;
        _tweenVector.Play();
    }

    void OnHide()
    {
        _tweenAlpha.From = _canvasGroup.alpha;
        _tweenAlpha.To = 0f;
        _tweenAlpha.Play();

        _tweenVector.From = _panel.GetComponent<RectTransform>().anchoredPosition;
        _tweenVector.To = Vector2.zero;
        _tweenVector.Play();
    }

    void OnTweenAlpha(float alpha)
    {
        _canvasGroup.alpha = alpha;
    }

    void OnTweenSlider(Vector2 v)
    {
        _panel.GetComponent<RectTransform>().anchoredPosition = v;
    }
}
