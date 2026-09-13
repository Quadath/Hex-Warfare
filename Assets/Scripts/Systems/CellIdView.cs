using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using Utils;

public class CellIdView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPrefab;
    [SerializeField] private Transform _canvas;
    
    private Dictionary<int, TextMeshProUGUI> _gos = new Dictionary<int, TextMeshProUGUI>();
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }
    public void DisplayCells(List<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
            if (_gos.TryGetValue(cell.ID, out var go))
            {
                if (Vector3.SqrMagnitude(Vector3Extensions.ToUnity(cell.Center) - _cameraTransform.position) <= 0.4f)
                {
                    go.gameObject.SetActive(true);
                    Vector3 position = Vector3Extensions.ToUnity(cell.Center);
                    Vector3 screenPosition =
                        Camera.main.WorldToScreenPoint(position);
                    go.transform.position = screenPosition;
                }
                else
                {
                    go.gameObject.SetActive(false);
                }
            }
            else
            {
                TextMeshProUGUI text = Instantiate(_textPrefab, _canvas);
                text.text = cell.ID.ToString();
                _gos.Add(cell.ID, text);
                Vector3 position = Vector3Extensions.ToUnity(cell.Center);
                Vector3 screenPosition =
                    Camera.main.WorldToScreenPoint(position);
                _gos[cell.ID].transform.position = screenPosition;
            }
        }
    }
}