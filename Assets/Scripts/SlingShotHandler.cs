using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;


    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            DrawSlingShot();
        }
    }

    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        setLines(touchPosition);
    }

    private void setLines(Vector2 position)
    {
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);
        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }
}
