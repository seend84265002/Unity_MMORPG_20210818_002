using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APINonStaticPactice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer spr;
    public Camera cam2;
    public SpriteRenderer spr2;
    public Transform bird1;
    public Rigidbody2D bird2;
    private void Start()
    {
        print("攝影機的深度"+cam.depth);
        print("圖片的顏色" + spr.color);

        cam2.backgroundColor = Random.ColorHSV();
        spr2.flipY = true;
       

    }
    private void Update()
    {
        bird1.Rotate(0, 0, 3);
        bird2.AddForce(new Vector2(0, 10));

    }
}
