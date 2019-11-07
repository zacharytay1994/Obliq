using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private GameObject camera;    //camera to track

    [Range(1, 5)]
    [SerializeField] private int NumberOfTilesX;
    [Range(1, 5)]
    [SerializeField] private int NumberOfTilesY;

    [Space(10)]
    [Range(-2.0f, 2.0f)]
    [SerializeField] private float XTilingOffset;


    [Range(-2.0f, 2.0f)]
    [SerializeField] private float YTilingOffset;


    [SerializeField] private List<GameObject> Backgrounds;

    [Tooltip("Target position, after parallax calculation, before looping calculation")]
    [SerializeField] private List<float> xTarget;
    [SerializeField] private List<float> yTarget;

    [Tooltip("Distance from the Background object to camera, before looping calculation (In Unity units)")]
    [SerializeField] private List<float> distancetocamerax;
    [SerializeField] private List<float> distancetocameray;
    [SerializeField] private List<Vector3> TargetPos;

    [SerializeField] private List<int> jumpnumberx;
    [SerializeField] private List<int> jumpnumbery;

    [Space(20)]
    [SerializeField] private bool MakeYScaleTheSameAsXScale;
    [Space(5)]

    [Range(0.0f, 1.0f)]
    [Tooltip("1 for follow camera, 0 for static.")]
    [SerializeField] private float XTrackingScale;

    [Space(5)]
    [Range(0.0f, 1.0f)]
    [Tooltip("1 for follow camera, 0 for static. (For Yscale, leave at a high value very near 1)")]
    [SerializeField] private float YTrackingScale;

    private float length;
    private float height;
    private float xscale;
    private float yscale;
    private float gap;
    private int count;




    [SerializeField] private bool Loopx;
    [SerializeField] private bool Loopy;

    [Space(20)]
    [Tooltip("X-Offset based on background size")]
    [SerializeField] private float X_Offset;

    [Space(5)]
    [Tooltip("Y-Offset based on background size")]
    [SerializeField] private float Y_Offset;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        count = transform.childCount;

        Backgrounds.Clear();
        xTarget.Clear();
        yTarget.Clear();
        distancetocamerax.Clear();
        distancetocameray.Clear();
        TargetPos.Clear();
        jumpnumberx.Clear();
        jumpnumbery.Clear();


        xscale = transform.GetChild(0).transform.localScale.x;
        yscale = transform.GetChild(0).transform.localScale.y;
        length = ((GetComponentInChildren<SpriteRenderer>().sprite.rect.width) / GetComponentInChildren<SpriteRenderer>().sprite.pixelsPerUnit) * xscale;
        height = ((GetComponentInChildren<SpriteRenderer>().sprite.rect.height) / GetComponentInChildren<SpriteRenderer>().sprite.pixelsPerUnit) * yscale;
        count = transform.childCount;

        if (count == 1)
        {
            if (Loopx)
            {
                for (int i = 0; i < (NumberOfTilesX - 1); i++)
                {
                    Instantiate(transform.GetChild(0).gameObject, new Vector3(transform.GetChild(0).position.x + length * (i + 1), transform.GetChild(0).position.y), transform.GetChild(0).rotation, gameObject.transform);

                }

                if (Loopy)
                {
                    for (int j = 0; j < (NumberOfTilesY - 1); j++)
                    {
                        for (int i = 0; i < (NumberOfTilesX); i++)
                        {

                            Instantiate(transform.GetChild(0).gameObject, new Vector3(transform.GetChild(0).position.x + length * (i), transform.GetChild(0).position.y + height * (j + 1)), transform.GetChild(0).rotation, gameObject.transform);

                        }

                    }
                }
            }
            else if (Loopy)
            {
                for (int i = 0; i < (NumberOfTilesY - 1); i++)
                {
                    Instantiate(transform.GetChild(0).gameObject, new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + height * (i + 1)), transform.GetChild(0).rotation, gameObject.transform);
                }

            }

        }


        count = transform.childCount;



        for (int i = 0; i < NumberOfTilesX * NumberOfTilesY; i++)
        {
            Backgrounds.Add(transform.GetChild(i).gameObject);
            xTarget.Add(transform.GetChild(i).position.x - camera.transform.position.x);
            yTarget.Add(transform.GetChild(i).position.y - camera.transform.position.y);
            distancetocamerax.Add(length * (Backgrounds.Count - 1));
            distancetocameray.Add(length * (Backgrounds.Count - 1));
            TargetPos.Add(new Vector3(0, 0));
            jumpnumberx.Add(0);
            jumpnumbery.Add(0);
            xscale = transform.GetChild(i).transform.localScale.x;
            yscale = transform.GetChild(i).transform.localScale.y;

        }

        gap = Backgrounds[Backgrounds.Count - 1].transform.position.x - Backgrounds[0].transform.position.x;
        //length = (Backgrounds[Backgrounds.Count - 1].transform.position.x - Backgrounds[0].transform.position.x) / (Backgrounds.Count - 1);


        length = ((GetComponentInChildren<SpriteRenderer>().sprite.rect.width) / GetComponentInChildren<SpriteRenderer>().sprite.pixelsPerUnit) * xscale;
        height = ((GetComponentInChildren<SpriteRenderer>().sprite.rect.height) / GetComponentInChildren<SpriteRenderer>().sprite.pixelsPerUnit) * yscale;
    }

    private void Update()
    {
        if (MakeYScaleTheSameAsXScale)
        {
            YTrackingScale = XTrackingScale;
        }

        if (Loopx && Loopy)
        {
            LoopXY();
        }
        else if (Loopx)
        {
            LoopX();
        }
        else if (Loopy)
        {
            LoopY();
        }
        else
        {
            NoLoop();
        }
    }

    private void LoopXY()
    {

        for (int i = 0; i < NumberOfTilesX * NumberOfTilesY; i++)
        {
            float xscaleoffset = camera.transform.position.x * XTrackingScale;
            float yscaleoffset = camera.transform.position.y * YTrackingScale;
            TargetPos[i] = new Vector3((i * length) + xscaleoffset, Mathf.FloorToInt(i / NumberOfTilesX) * height + yscaleoffset);
        }





        for (int i = 0; i < NumberOfTilesX * NumberOfTilesY; i++)
        {
            TargetPos[i] = new Vector3(TargetPos[i].x + (length * X_Offset), TargetPos[i].y + (height * Y_Offset));

            xTarget[i] = TargetPos[i].x;
            yTarget[i] = TargetPos[i].y;
            distancetocamerax[i] = camera.transform.position.x - xTarget[i];
            distancetocameray[i] = camera.transform.position.y - yTarget[i];




            jumpnumberx[i] = Mathf.FloorToInt(distancetocamerax[i] / (length * NumberOfTilesX));

            TargetPos[i] = new Vector3((TargetPos[i].x + jumpnumberx[i] * length * NumberOfTilesX) + length + (XTilingOffset * length), TargetPos[i].y);


            jumpnumbery[i] = Mathf.FloorToInt(distancetocameray[i] / (height * NumberOfTilesY));

            TargetPos[i] = new Vector3(TargetPos[i].x, TargetPos[i].y + (jumpnumbery[i] + 1) * height * NumberOfTilesY - height + (YTilingOffset * height));





            Backgrounds[i].transform.position = TargetPos[i];
        }

    }


    private void LoopX()
    {
        for (int i = 0; i < NumberOfTilesX; i++)
        {
            float xscaleoffset = camera.transform.position.x * XTrackingScale;
            float yscaleoffset = camera.transform.position.y * YTrackingScale;
            TargetPos[i] = new Vector3((i * length) + xscaleoffset, Mathf.FloorToInt(i / NumberOfTilesX) * height + yscaleoffset);
        }





        for (int i = 0; i < NumberOfTilesX; i++)
        {
            TargetPos[i] = new Vector3(TargetPos[i].x + (length * X_Offset), TargetPos[i].y + (height * Y_Offset));

            xTarget[i] = TargetPos[i].x;
            yTarget[i] = TargetPos[i].y;
            distancetocamerax[i] = camera.transform.position.x - xTarget[i];
            distancetocameray[i] = camera.transform.position.y - yTarget[i];





            jumpnumberx[i] = Mathf.FloorToInt((distancetocamerax[i]) / (length * (NumberOfTilesX)));

            TargetPos[i] = new Vector3((TargetPos[i].x + jumpnumberx[i] * length * (NumberOfTilesX)) + length + (XTilingOffset * length), TargetPos[i].y);







            Backgrounds[i].transform.position = TargetPos[i];
        }

    }

    private void LoopY()
    {
        for (int i = 0; i < NumberOfTilesY; i++)
        {
            float xscaleoffset = camera.transform.position.x * XTrackingScale;
            float yscaleoffset = camera.transform.position.y * YTrackingScale;
            TargetPos[i] = new Vector3(xscaleoffset, Mathf.FloorToInt(i) * height + yscaleoffset);
        }


        for (int i = 0; i < NumberOfTilesY; i++)
        {
            TargetPos[i] = new Vector3(TargetPos[i].x + (length * X_Offset), TargetPos[i].y + (height * Y_Offset));

            xTarget[i] = TargetPos[i].x;
            yTarget[i] = TargetPos[i].y;

            distancetocamerax[i] = camera.transform.position.x - xTarget[i];
            distancetocameray[i] = camera.transform.position.y - yTarget[i];




            jumpnumbery[i] = Mathf.FloorToInt(distancetocameray[i] / (height * (NumberOfTilesY)));

            TargetPos[i] = new Vector3(TargetPos[i].x, TargetPos[i].y + (jumpnumbery[i] + 1) * height * (NumberOfTilesY) - height + (YTilingOffset * height));





            Backgrounds[i].transform.position = TargetPos[i];
        }
    }

    private void NoLoop()
    {

        float xscaleoffset = camera.transform.position.x * XTrackingScale;
        float yscaleoffset = camera.transform.position.y * YTrackingScale;
        TargetPos[0] = new Vector3(xscaleoffset, Mathf.FloorToInt(0) * height + yscaleoffset);

        TargetPos[0] = new Vector3(TargetPos[0].x + (length * X_Offset), TargetPos[0].y + (height * Y_Offset));

        Backgrounds[0].transform.position = TargetPos[0];



    }
}
