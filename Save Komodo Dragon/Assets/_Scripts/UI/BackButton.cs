using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button button;
    private void Awake() {
        button = GetComponent<Button>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.onClick.Invoke();
        }
    }
}
