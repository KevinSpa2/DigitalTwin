using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] 
    private RectTransform uiHandleRectTransform; 

    [SerializeField] 
    private Color backgroundActiveColor, handleActiveColor;

    [SerializeField]
    private GameObject controlsButton, plcController;

    private Image backgroundImage, handleImage;
    private Color backgroundDefaultColor, handleDefaultColor;
    private Toggle toggle;
    private Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        // Check if uiHandleRectTransform is assigned
        if (uiHandleRectTransform == null)
        {
            Debug.LogError("uiHandleRectTransform is niet toegekend");
            return;
        }

        // Save the initial position of the handle
        handlePosition = uiHandleRectTransform.anchoredPosition;

        // Get the Image components for background and handle
        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        // Save the default colors for background and handle
        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    void Update()
    {
        this.DisableControls();
    }

    void OnSwitch(bool on)
    {
        // Start animations for handle position, background color, and handle color
        StartCoroutine(AnimateHandlePosition(on));
        StartCoroutine(AnimateBackgroundColor(on));
        StartCoroutine(AnimateHandleColor(on));
    }

    // Coroutine to animate handle position
    IEnumerator AnimateHandlePosition(bool on)
    {
        Vector2 start = uiHandleRectTransform.anchoredPosition;
        Vector2 end = on ? handlePosition * -1 : handlePosition;
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            uiHandleRectTransform.anchoredPosition = Vector2.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        uiHandleRectTransform.anchoredPosition = end;
    }

    // Coroutine to animate background color
    IEnumerator AnimateBackgroundColor(bool on)
    {
        Color start = backgroundImage.color;
        Color end = on ? backgroundActiveColor : backgroundDefaultColor;
        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            backgroundImage.color = Color.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        backgroundImage.color = end;
    }

    // Coroutine to animate handle color
    IEnumerator AnimateHandleColor(bool on)
    {
        Color start = handleImage.color;
        Color end = on ? handleActiveColor : handleDefaultColor;
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            handleImage.color = Color.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        handleImage.color = end;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    // Method to enable/disable controls based on the toggle state
    private void DisableControls()
    {
        if (toggle.isOn)
        {
            controlsButton.GetComponent<Button>().interactable = true;
            plcController.SetActive(false);
        }
        else
        {
            controlsButton.GetComponent<Button>().interactable = false;
            plcController.SetActive(true);
        }
    }
}
