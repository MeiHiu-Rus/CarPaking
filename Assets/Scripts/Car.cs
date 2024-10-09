using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route route;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem smokeFX;

    [SerializeField] Rigidbody rb;
    [SerializeField] float danceValue;
    [SerializeField] float durationMultiplier;


    [SerializeField] private AudioSource idleAudioSource;
    [SerializeField] private AudioSource moveAudioSource;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip moveSound;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


        idleAudioSource.clip = idleSound;
        moveAudioSource.clip = moveSound;


        idleAudioSource.loop = true;
        idleAudioSource.Play();
        moveAudioSource.loop = true;
    }

    private void Start()
    {

        bodyTransform.DOLocalMoveY(danceValue, .1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car otherCar))
        {
            StopDancingAnim();
            rb.DOKill(false);

            //add explosion
            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);
            smokeFX.Play();
            audioManager.PlaySFX(audioManager.smoke);
            audioManager.PlaySFX(audioManager.carAcciden);

            Game.Instance.OnCarCollision?.Invoke();
        }
    }

    private void AddExplosionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f, point, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }

    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > .5f ? angle : -angle;
    }

    public void Move(Vector3[] path)
    {

        if (idleAudioSource.isPlaying)
        {
            idleAudioSource.Stop();
            moveAudioSource.Play();
        }

        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
            .SetLookAt(.01f, false)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {

                moveAudioSource.Stop();
                idleAudioSource.Play();
            });
    }

    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true);
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}