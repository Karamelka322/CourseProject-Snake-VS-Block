using UnityEngine;

[RequireComponent(typeof(Block), typeof(SpriteRenderer))]
public class BlockGradient : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    private SpriteRenderer _sprite;
    private Block _block;

    private float _startValueColor;
    private int _maxValueFilling;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _block = GetComponent<Block>();

        _startValueColor = 1f;
        _maxValueFilling = _block.MaxDestroyPrice;
    }

    private void OnEnable()
    {
        _block.FillingUpdated += OnUpdatedColor;
    }

    private void OnDisable()
    {
        _block.FillingUpdated -= OnUpdatedColor;       
    }

    private void OnUpdatedColor(int filling)
    {
        float valueColor = (_startValueColor / _maxValueFilling) * filling;
        _sprite.color = _gradient.Evaluate(valueColor);
    }
}
