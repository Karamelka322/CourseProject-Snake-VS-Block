using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator), typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpringiness;
    
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;
    private SnakeInput _input;

    public int Size => _tail.Count;
    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _input = GetComponent<SnakeInput>();
        _tail = _tailGenerator.Generator(_tailSize);

        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void Move(Vector3 nexPosition)
    {
        Vector3 previousPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector3 tempPostion = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringiness * Time.fixedDeltaTime);
            previousPosition = tempPostion;
        }

        _head.Move(nexPosition);
    }

    private void OnBlockCollided()
    {
        Segment deletedSigment = _tail[_tail.Count - 1];
        _tail.Remove(deletedSigment);
        Destroy(deletedSigment.gameObject);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generator(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
