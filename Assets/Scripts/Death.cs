using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject Hero;
    private Vector3 _start = new Vector3();
    private void Start()
    {
        _start = Hero.transform.position;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Hero.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Hero.transform.position = _start;
        }
    }
}
