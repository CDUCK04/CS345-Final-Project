using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    public void Start()
    {
        mainCamera = Camera.main;
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        slider.value = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = mainCamera.transform.rotation;
        transform.position = target.position + offset;
    }
}
