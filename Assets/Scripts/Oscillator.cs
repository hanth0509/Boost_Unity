using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition; //vi tri ban dau
    [SerializeField] Vector3 movementVector; //vector chuyen dong
    float movementFactor;// he so chuyen dong
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // lay vi tri hien tai   
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //tranh = 0 
        float cycles = Time.time / period; // chu ky
        const float au = Mathf.PI * 2; // gia tri khong doi 6.283
        float rawSinWave = Mathf.Sin(cycles * au);// -1 den 1
        // Debug.Log(rawSinWave);
        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;// di chuyen
        transform.position = startingPosition + offset;
    }
}
