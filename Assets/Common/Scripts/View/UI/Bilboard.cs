using UnityEngine;

public class Bilboard : MonoBehaviour
{
    Camera cam;

    void Start() => cam = Camera.main;

    void Update() => transform.LookAt(cam.transform);
}
