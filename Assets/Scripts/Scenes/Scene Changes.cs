using UnityEngine;
using UnityEngine.UI;

public class IntroImageSwitcher : MonoBehaviour
{
    public Image displayImage;              // The UI Image to display on screen
    public Sprite[] images;                 // Your 2D images to switch between
    public float switchTime = 2f;           // Time between switches
    private int currentIndex = 0;

    private void Start()
    {
        if (images.Length > 0)
        {
            displayImage.sprite = images[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shrimp");
            SwitchImage();
        }
    }

    void SwitchImage()
    {
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            CancelInvoke(nameof(SwitchImage));
            // Optional: Load next scene or continue game
            return;
        }

        displayImage.sprite = images[currentIndex];
    }
}