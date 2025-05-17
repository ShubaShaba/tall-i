using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject playerRef;
    private IPlayerUI playerUIdata;
    private TextMeshProUGUI instructionsText;
    private ISelectable selection;

    void Start()
    {
        playerUIdata = playerRef.GetComponent<IPlayerUI>();
    }

    void Update()
    {
        // Should be equal to max focus distance
        ISelectable foundObject = PlayerSelection.GetObjectReference<ISelectable>(100, cameraPosition);

        if (!PauseMenu.isPaused)
        {
            // Remove outline from the previously selected object
            if (foundObject != selection)
            {
                selection?.ResetColor();
                selection = foundObject;
            }

            if (!playerUIdata.isCarryingSomething())
            {
                selection?.Highlight();
            }
        }
    }
}