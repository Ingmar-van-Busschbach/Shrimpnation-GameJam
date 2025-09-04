using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneLoader))]
public class IntroTextSwitcher : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;              // The UI Image to display on screen
    public string[] lines;                 // Your 2D images to switch between
    public float switchTime = 2f;           // Time between switches
    private int currentIndex = 0;

    private void Start()
    {
        if (lines.Length > 0)
        {
            text.text = lines[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchText();
        }
    }

    void SwitchText()
    {
        currentIndex++;
        if (currentIndex >= lines.Length)
        {
            CancelInvoke(nameof(SwitchText));
            return;
        }

        text.text = lines[currentIndex];
    }
}