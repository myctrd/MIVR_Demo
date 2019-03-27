using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour {

    public static CarController instance;

    private Transform m_transform;

    [SerializeField]
    private Transform leftDoor, rightDoor, innerT, outerT, cameraT;

    private int positionIndex = 0; //0-车外， 1-车内

    private float rotateSpeed = 0;

    // Use this for initialization
    void Awake () {
        m_transform = transform;
        if (instance == null)
            instance = this;
	}

    // Update is called once per frame
    void Update () {
        if (m_transform)
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}

    public void RotateLeft()
    {
        if (positionIndex == 1) return;
        rotateSpeed = 20;
    }

    public void RotateRight()
    {
        if (positionIndex == 1) return;
        rotateSpeed = -20;
    }

    public void RotateStop()
    {
        rotateSpeed = 0;
    }

    private bool doorReset = true;

    public void OpenLeftDoor()
    {
        if(!doorReset)
        {
            return;
        }
        doorReset = false;
        Invoke("ResetDoor", 1);
        leftDoor.GetComponent<Animator>().Play("door_open");
    }

    public void CloseLeftDoor()
    {
        if (!doorReset)
        {
            return;
        }
        doorReset = false;
        Invoke("ResetDoor", 1);
        leftDoor.GetComponent<Animator>().Play("door_close");
    }

    public void OpenRightDoor()
    {
        if (!doorReset)
        {
            return;
        }
        doorReset = false;
        Invoke("ResetDoor", 1);
        rightDoor.GetComponent<Animator>().Play("door_open");
    }

    public void CloseRightDoor()
    {
        if (!doorReset)
        {
            return;
        }
        doorReset = false;
        Invoke("ResetDoor", 1);
        rightDoor.GetComponent<Animator>().Play("door_close");
    }

    void ResetDoor()
    {
        doorReset = true;
    }

    public void SwitchPosition()
    {
        if(positionIndex == 0)
        {
            positionIndex = 1;
            cameraT.SetParent(innerT);
        }
        else
        {
            positionIndex = 0;
            cameraT.SetParent(outerT);
        }
        RotateStop();
        leftDoor.GetComponent<Animator>().Play("door_close");
        rightDoor.GetComponent<Animator>().Play("door_close");
        cameraT.localPosition = Vector3.zero;
        cameraT.localRotation = new Quaternion(0, 0, 0, 0);
    }

    [SerializeField]
    private Material material;

    private void Start()
    {
        SetColor(0);
    }

    public void SetColor(int i)
    {
        Color color;
        switch(i)
        {
            case 0:
                color = new Color(255.0f / 255, 79.0f / 255, 0, 1);
                break;
            case 1:
                color = new Color(1, 1, 1, 1);
                break;
            case 2:
                color = new Color(1, 0, 0, 1);
                break;
            case 3:
                color = new Color(114.0f / 255, 104.0f / 255, 104.0f / 255, 1);
                break;
            default:
                color = new Color(255.0f / 255, 79.0f / 255, 0, 1);
                break;
        }

        material.color = color;
    }
}
