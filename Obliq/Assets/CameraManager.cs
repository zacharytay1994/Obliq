using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private float OriginalShakeDuration;

    [SerializeField] public bool TrackMouse;
    [Space(10)]


    [SerializeField] public bool shakeEnabled;
    // Desired duration of the shake effect
    public float shakeDuration = 0.1f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.1f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;
    [Space(20)]
    // The initial position of the GameObject
    public Vector3 initialPosition;
    [Space(20)]
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 PlayerLocation;
    [SerializeField] private Vector3 CameraLocation;
    [SerializeField] public Vector3 CameraTarget;
    [Range(-10.0f, 10.0f)]
    public float xoffset;
    [Range(-10.0f, 10.0f)]
    public float yoffset;
    [Space(20)]

    [Tooltip("0 = no movement, 1 = instant tracking")]
    [Range(0.0f, 1.0f)]
    [SerializeField] public float TrackingSpeed;  //1 for instant tracking, 0 for no movement
    [Space(20)]


    [SerializeField] private Vector3 offset;
    [Space(20)]

    public Camera Camera;

    private float camerawidth;
    private float cameraheight;
    private float mousepositionx;
    private float mousepositiony;
    private float xratio;
    private float yratio;
    [Tooltip("Adjusts camera-mouse panning speed")]
    [Range(0, 10)]
    [SerializeField] private float CameraPanStrength;
    [Space(20)]
    [SerializeField] private bool ShakeCamera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            PlayerLocation = player.transform.position;
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
            offset = transform.position - player.transform.position;
        }




        CameraLocation = transform.position;
        camerawidth = Camera.pixelWidth;
        cameraheight = Camera.pixelHeight;



    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        
        CameraLocation = transform.position;
        camerawidth = Camera.pixelWidth;
        cameraheight = Camera.pixelHeight;

        if (player != null)
        {
            PlayerLocation = player.transform.position;
            if (TrackMouse)
            {
                trackMouse();
            }
            else
            {
                CameraTarget = PlayerLocation + new Vector3(CameraPanStrength * xratio + xoffset, CameraPanStrength * yratio + yoffset, -20);
            }

            CameraLocation = Vector3.Lerp(transform.position, CameraTarget, TrackingSpeed * Time.deltaTime * 10);
                       
        }
        else
        {
            if (TrackMouse)
            {
                Vector2 temp2 = Input.mousePosition; //Mouse Position
                Camera.ViewportToScreenPoint(temp2);
                mousepositionx = temp2.x;
                mousepositiony = temp2.y;

                xratio = (2 * ((mousepositionx) / camerawidth) - 1) * (camerawidth / cameraheight);
                yratio = (2 * ((mousepositiony) / cameraheight) - 1);
                
                CameraTarget = new Vector3(CameraPanStrength * xratio + xoffset, CameraPanStrength * yratio + yoffset, -20);        //Camera pan when mouse moves

            }
            else
            {
                CameraTarget = new Vector3(CameraPanStrength * xratio + xoffset, CameraPanStrength * yratio + yoffset, -20);
            }

            CameraLocation = Vector3.Lerp(transform.position, CameraTarget, TrackingSpeed * Time.deltaTime * 10);

        }

        if (ShakeCamera)
        {
            Shake();
            ShakeCamera = false;
        }

        CameraShake();
    }

    void trackMouse()
    {
        Vector2 temp2 = Input.mousePosition; //Mouse Position
        Camera.ViewportToScreenPoint(temp2);
        mousepositionx = temp2.x;
        mousepositiony = temp2.y;

        xratio = (2 * ((mousepositionx) / camerawidth) - 1) * (camerawidth / cameraheight);
        yratio = (2 * ((mousepositiony) / cameraheight) - 1);

        PlayerLocation = player.transform.position + offset;
        CameraTarget = PlayerLocation + new Vector3(CameraPanStrength * xratio + xoffset, CameraPanStrength * yratio + yoffset, -20);        //Camera pan when mouse moves

    }


    void CameraShake()
    {
        if (shakeDuration > 0)
        {
            if (shakeEnabled)       //Camera Shake bool
            {
                transform.localPosition = CameraLocation + Random.insideUnitSphere * shakeMagnitude * (shakeDuration* shakeDuration / OriginalShakeDuration* OriginalShakeDuration);
            }
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = CameraLocation;
        }
    }

    public void Shake()
    {
        OriginalShakeDuration = 1;
        shakeDuration = 1;
        shakeMagnitude = 0.15f;
        dampingSpeed = 1;
    }
    public void Shake(float shaked)
    {
        OriginalShakeDuration = shaked;
        shakeDuration = shaked;
        shakeMagnitude = 0.15f;
        dampingSpeed = 1;
    }

    public void Shake(float shaked, float shakem)
    {
        OriginalShakeDuration = shaked;
        shakeDuration = shaked;
        shakeMagnitude = shakem;
        dampingSpeed = 1;
    }

    public void Shake(float shaked, float shakem, float dampings)
    {
        OriginalShakeDuration = shaked;
        shakeDuration = shaked;
        shakeMagnitude = shakem;
        dampingSpeed = dampings;
    }
}
