using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Controller2D))]
public class FallShirmpJager : MonoBehaviour
{
    Controller2D controller;
    [SerializeField] GameObject parachute;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.collisionInfo.below)
        { // If airborne
            parachute.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            parachute.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
