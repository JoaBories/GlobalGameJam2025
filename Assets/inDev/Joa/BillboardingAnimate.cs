using System.Collections.Generic;
using UnityEngine;

public class BillboardingAnimate : MonoBehaviour
{
    Vector3 cameraViewDir;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] float timeBetweenFrame;

    public static bool pause = false;

    private SpriteRenderer sp;

    private float nextFrameTimer;
    private int currentFrameIndex;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        if (sprites.Count != 0)
        {
            sp.sprite = sprites[0];
        }

        //tweak a bit time

        float scale = timeBetweenFrame / 15;
        timeBetweenFrame += Random.Range(-1f, 1f) * scale;
        nextFrameTimer += Random.Range(-2f, 2f) * scale;
    }

    private void Update()
    {
        cameraViewDir = Camera.main.transform.forward;
        cameraViewDir.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraViewDir);

        if (sprites.Count != 0 && !pause)
        {
            nextFrameTimer += Time.deltaTime;
            if (nextFrameTimer >= timeBetweenFrame)
            {
                if (currentFrameIndex == sprites.Count)
                {
                    currentFrameIndex = 0;
                }
                else
                {
                    currentFrameIndex++;
                }
                nextFrameTimer = 0;
                sp.sprite = sprites[currentFrameIndex];
            }
        }
    }
}
