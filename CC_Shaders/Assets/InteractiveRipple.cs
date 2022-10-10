using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractiveRipple : MonoBehaviour
{
    private Material _material;
    private Color _previousColor;
    private static readonly int RippleStartTime = Shader.PropertyToID("_RippleStartTime");
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int RippleColor = Shader.PropertyToID("_RippleColor");
    private static readonly int RippleCenter = Shader.PropertyToID("_RippleCenter");

    private void Start() {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
        
        _previousColor = _material.GetColor(BaseColor);
        _material.SetColor(RippleColor, _previousColor);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            CastClickRay();
        }
    }

    private void CastClickRay() {
        var camera = Camera.main;
        var mousePosition = Input.mousePosition;
        
        var ray = camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));
        
        if(Physics.Raycast(ray, out var hit) && hit.collider.gameObject == gameObject) {
            StartRipple(hit.point);
        }
    }

    private void StartRipple(Vector3 center) {
        Color rippleColor = Color.HSVToRGB(Random.value, 1, 0.8f);

        _material.SetVector(RippleCenter, center);
        
        _material.SetFloat(RippleStartTime, Time.time);
        _material.SetColor(BaseColor, _previousColor);
        _material.SetColor(RippleColor, rippleColor);

        _previousColor = rippleColor;
    }
}
