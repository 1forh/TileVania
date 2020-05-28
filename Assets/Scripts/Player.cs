using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;


    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var x = horizontalInput * _speed * Time.deltaTime;
        Vector3 position = new Vector3(x, 0, transform.position.z);
        transform.Translate(position);
    }
}
