using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    void Start()
    {
        // Create a new Canvas
        GameObject canvasGameObject = new GameObject("Canvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();

        // Create the first button
        GameObject button1 = CreateButton("DOD", new Vector2(0, 50));
        button1.transform.SetParent(canvas.transform, false);
        Button buttonComponent1 = button1.GetComponent<Button>();
        buttonComponent1.onClick.AddListener(() => ChangeScene("DOD"));

        // Create the second button
        GameObject button2 = CreateButton("DefaultEcs", new Vector2(0, -50));
        button2.transform.SetParent(canvas.transform, false);
        Button buttonComponent2 = button2.GetComponent<Button>();
        buttonComponent2.onClick.AddListener(() => ChangeScene("DefaultEcs"));
    }

    private GameObject CreateButton(string name, Vector2 position)
    {
        GameObject button = new GameObject(name);
        button.transform.position = position;

        // Set up Button component
        Button buttonComponent = button.AddComponent<Button>();
        Image imageComponent = button.AddComponent<Image>();
        buttonComponent.targetGraphic = imageComponent;

        // Set up Text component
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(button.transform);
        textObject.transform.localPosition = Vector3.zero;

        Text textComponent = textObject.AddComponent<Text>();
        textComponent.text = name;
        textComponent.color = Color.black;
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;

        return button;
    }

    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
