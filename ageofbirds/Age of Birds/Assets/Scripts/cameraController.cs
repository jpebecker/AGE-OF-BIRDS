using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;

    public Transform cameraTransform;
    public Transform followTransform;

    public float cameraSpeed, cameraForce, rotationForce;

    private Vector3 NewPos, NewZoom, MouseDragStartPos, MouseDragCurrentPos, rotateStartPos, rotateCurrentPos;
    public Vector3 zoomForce;

    public float minZoom, MaxZoom;
    private Quaternion newRotation;

    void Start()
    {
        instance = this;

        NewPos = transform.position;
        newRotation = transform.rotation;
        NewZoom = cameraTransform.localPosition;
    }
    void LateUpdate()
    {
        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            CalculoDoInputdeMovimento();
            CalculoDoMouse();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }

       
    }

    void CalculoDoMouse()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            NewZoom += Input.mouseScrollDelta.y * zoomForce;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                MouseDragStartPos = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                MouseDragCurrentPos = ray.GetPoint(entry);

                NewPos = transform.position + MouseDragStartPos - MouseDragCurrentPos;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPos = Input.mousePosition;

            Vector3 diferenca = rotateStartPos - rotateCurrentPos;

            rotateStartPos = rotateCurrentPos;

            newRotation *= Quaternion.Euler(Vector3.up * (-diferenca.x / 5f));
        }
    }

    void CalculoDoInputdeMovimento()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            NewPos += (transform.forward * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            NewPos += (transform.forward * -cameraSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            NewPos += (transform.right * cameraSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            NewPos += (transform.right * -cameraSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationForce);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationForce);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            NewZoom += zoomForce;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            NewZoom -= zoomForce;
        }


        NewZoom.y = Mathf.Clamp(NewZoom.y, -minZoom, MaxZoom);
        NewZoom.z = Mathf.Clamp(NewZoom.z, -MaxZoom, minZoom);

        transform.position = Vector3.Lerp(transform.position, NewPos, Time.deltaTime * cameraForce);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * cameraForce);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, NewZoom, Time.deltaTime * cameraForce);
    }
}
