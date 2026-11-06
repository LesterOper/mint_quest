using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MintAccelerate : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _colliderClick;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _minY = -5.88f;
    [SerializeField] private float _maxY = -2.9f;
    
    
    private Rigidbody2D rb;
    private float dirY;
    private float _moveSpeed = 12.5f;
    private float minimumAcceleration = 0.15f;

    private bool _canTap = false;
    private bool _canRotateText = true;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirY = Input.acceleration.y * _moveSpeed;
        if (Input.acceleration.y > minimumAcceleration)
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY));

        if (transform.position.y > -10.3f)
        {
            if (!_canTap)
            {
                _canTap = true;
                _colliderClick.enabled = true;
            }
        }
        else if (transform.position.y < -10.3f)
        {
            _canTap = false;
            _colliderClick.enabled = false;
        }

        if (dirY > 0 && _canRotateText)
        {
            _canRotateText = false;
            _text.transform.DORotate(new Vector3(0, 0, 180), 0f).OnComplete((() => _canRotateText = true));
        }
        else if (dirY < 0 && _canRotateText)
        {
            _canRotateText = false;
            _text.transform.DORotate(Vector3.zero, 0f).OnComplete((() => _canRotateText = true));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, dirY);
    }
    
    
}
