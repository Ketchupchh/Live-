using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    [TextArea]
    [SerializeField] private string helper = "This should be on your camera holder GameObject.";

    public Transform cameraPos;

    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
