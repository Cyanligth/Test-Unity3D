using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraChange : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera player;
    [SerializeField]
    private CinemachineVirtualCamera view;
    [SerializeField]
    private CinemachineVirtualCamera zoom;

    private void Awake()
    {
        player.Priority = 30;
        view.Priority = 20;
        zoom.Priority = 10;
    }
    private void OnZoom(InputValue value)
    {
        if(value.isPressed)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        zoom.Priority = 30;
        player.Priority = 20;
        view.Priority = 10;

    }
    private void ZoomOut()
    {
        zoom.Priority = 10;
        player.Priority = 30;
        view.Priority = 20;
    }

    private void OnPlayerCam(InputValue value)
    {
        player.Priority = 30;
        view.Priority = 20;
        zoom.Priority = 10;
    }

    private void OnViewCam(InputValue value)
    {
        player.Priority = 20;
        view.Priority = 30;
        zoom.Priority = 10;
    }
}
