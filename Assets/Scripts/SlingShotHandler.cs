using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Slingshot stats")]
    [SerializeField] private float _maxDistance = 3.5f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea _slingShotArea;

    [Header("Bird")]
    [SerializeField] private GameObject _angryBirdPrefab;

    private Vector2 _slingShotLinesPosition;

    private bool _clickedWithinArea;

    private GameObject _spawnedAngryBird;

    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        SpawnAngryBird();
    }


    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _slingShotArea.IsWithinSlingShotArea())
        {
            _clickedWithinArea = true;
        }
        if (Mouse.current.leftButton.isPressed && _clickedWithinArea)
        {
            DrawSlingShot();
            PositionAndRotateAngryBird();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _clickedWithinArea = false;
        }
    }

    #region SlingShot Methods

    private void DrawSlingShot()
    {
  
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position , _maxDistance);

        setLines(_slingShotLinesPosition);
    }

    private void setLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);
        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }

    #endregion

    #region Angry Bird Methods

    private void SpawnAngryBird()
    {
        setLines(_idlePosition.position);

        _spawnedAngryBird = Instantiate(_angryBirdPrefab, _idlePosition.position, Quaternion.identity);
    }

    private void PositionAndRotateAngryBird()
    {
        _spawnedAngryBird.transform.position = _slingShotLinesPosition;
    }

    #endregion
}
