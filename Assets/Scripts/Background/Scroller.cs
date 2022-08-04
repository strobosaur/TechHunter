using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float x1, y1, x2, y2, x3, y3;

    // START
    void Start()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(Random.Range(0, _image.uvRect.size.x),Random.Range(0, _image.uvRect.size.y)), _image.uvRect.size);

        x3 = x2 = x1 = Random.Range(0.01f, 0.03f);
        y3 = y2 = y1 = Random.Range(0.01f, 0.03f);

        InvokeRepeating("RandomizeSpeeds", 0f, 2f);
    }

    // UPDATE
    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(x2,y2) * Time.deltaTime, _image.uvRect.size);

        x2 = Mathf.Lerp(x2, x3, 0.1f * Time.deltaTime);
        y2 = Mathf.Lerp(y2, y3, 0.1f * Time.deltaTime);
    }

    // RANDOMIZE SPEEDS
    private void RandomizeSpeeds()
    {
        x3 = x1 * Random.Range(0.75f, 1.5f);
        y3 = y1 * Random.Range(0.75f, 1.5f);
    }
}
