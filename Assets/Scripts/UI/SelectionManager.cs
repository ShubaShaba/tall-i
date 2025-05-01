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
        ISelectable foundObject = PlayerSelection.GetObjectReference<ISelectable>(10, cameraPosition);

        if (PauseMenu.isPaused == false)
        {
            // Remove outline from the previously selected object
            if (foundObject != selection)
            {
                selection?.ResetColor();
                selection = foundObject;
            }

            if (playerUIdata.isFocusedOnSomethingType2())
            {

                // renderer.material = originalMaterial;

            }

            if (playerUIdata.isCarryingSomething())
            {

                // renderer.material = originalMaterial;

            }
            if (playerUIdata.isCarryingSomething() == false && playerUIdata.isFocusedOnSomethingType2() == false)
            {
                selection?.Highlight();
            }
        }
    }
}