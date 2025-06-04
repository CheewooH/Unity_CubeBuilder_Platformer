using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCube : MonoBehaviour
{
    [SerializeField] private float visibleTime = 3f;
    [SerializeField] private float hiddenTime = 3f;

    private Renderer _renderer;
    private Collider _collider;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponent<Collider>();
        StartCoroutine(TogglePlatform());
    }

    private IEnumerator TogglePlatform()
    {
        while (true)
        {
            // 나타남
            _renderer.enabled = true;
            _collider.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // 사라짐
            _renderer.enabled = false;
            _collider.enabled = false;
            yield return new WaitForSeconds(hiddenTime);
        }
    }
}
