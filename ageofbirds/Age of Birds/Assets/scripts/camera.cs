using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private Vector3 offset;
    void Update()
    {
        /*
        Targetzoom -= FindObjectOfType<bird>().gameObject.transform.localScale.x * zoomFactor;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Targetzoom, Time.deltaTime);
        */
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
