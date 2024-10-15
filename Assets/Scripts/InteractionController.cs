using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private KeyCode _grabKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _explosionKey = KeyCode.Mouse1;

    private Grabber _grabber;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _yUpPosition = 2f;

    private ExplosionShooter _explosionShooter;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        _grabber = new Grabber(_camera, _grabKey, _ground, _yUpPosition);
        _explosionShooter = new ExplosionShooter(_camera, _explosionKey, _particleSystem, _explosionForce, _explosionRadius);
    }

    private void Update()
    {
        _grabber.Grab();
        _explosionShooter.Explode();
    }
}
