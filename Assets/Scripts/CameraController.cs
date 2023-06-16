using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 _pos;

    private void Awake()
    {
        if (!_player) _player = FindObjectOfType<PlayerHealth>().transform;
    }
    private void Update()
    {
        _pos = transform.position;
        _pos.z = -10f;
        float offsetY = 2f;
        transform.position = Vector3.Lerp(_pos, new Vector3(_player.position.x, _player.transform.position.y + offsetY, -10f),Time.deltaTime);
        //_pos = transform.position;
        //
        //transform.position = Vector3.Lerp(transform.position, _pos * Time.deltaTime);
    }
}
