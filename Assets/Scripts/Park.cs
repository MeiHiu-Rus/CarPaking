using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem fx;
    private ParticleSystem.MainModule fxMainModule;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    private void Start()
    {
        fxMainModule = fx.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car)) {
            if (car.route == route) {
                Game.Instance.OnCarEntersPark?.Invoke(route);
                StartFX();
                audioManager.PlaySFX(audioManager.parking);
            }
        }
    }

    private void StartFX()
    {
        fxMainModule.startColor = route.carColor;
        fx.Play();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}