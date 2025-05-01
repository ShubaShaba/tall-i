using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField, Range(0, 1)] private float paleAmount = 0.4f;
    [SerializeField] private Color originalColor;
    private MaterialPropertyBlock block;
    private Renderer selectionRenderer;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject playerRef;
    private IPlayerUI playerUIdata;
    private bool isHoldingObject;

    private static readonly int ColorID = Shader.PropertyToID("_Color");

    private Transform _selection;
    public TextMeshProUGUI instructionsText;

    private void Highlight(Transform selectable)
    {
        selectionRenderer = selectable.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            block = new MaterialPropertyBlock();
            selectionRenderer.GetPropertyBlock(block);
            
            if (block.HasColor(ColorID))
                originalColor = block.GetColor(ColorID);
            else
                originalColor = selectionRenderer.sharedMaterial.GetColor(ColorID);

            block.SetColor(ColorID, Color.Lerp(originalColor, Color.white, paleAmount));
            selectionRenderer.SetPropertyBlock(block);
        }
    }

    private void ResetOutline()
    {
        if (_selection != null)
        {
            block.SetColor(ColorID, originalColor);
            selectionRenderer.SetPropertyBlock(block);
        }
    }

    // private void HintText() {
    //      instructionsText.text = "";

    //         if (playerUIdata.isCarryingSomething() && isHoldingObject == false)
    //         {
    //             instructionsText.text = "Throw: Right Mouse";
    //         }

    //         if (playerUIdata.isFocusedOnSomethingType1())
    //         {
    //             instructionsText.text = "Rewind Object: 1";
    //         }
    //         else
    //         {
    //             if (playerUIdata.isFocusedOnSomethingType2())
    //             {
    //                 instructionsText.text = "Rewind Object: 1 \nFreeze Object: 2 \nManual Rewind: 3";

    //                 if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.Stoped)
    //                 {
    //                     instructionsText.text = "Rewind Object: 1 \nManual Rewind: 3";
    //                 }
    //                 if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.Rewinding)
    //                 {
    //                     instructionsText.text = "Freeze Object: 2 \nManual Rewind: 3";
    //                 }

    //                 if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledRewinding)
    //                 {
    //                     instructionsText.text = "Forward Object: E";
    //                 }
    //                 if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledStoped)
    //                 {
    //                     instructionsText.text = "Reverse Object: Q \nForward Object: E";
    //                 }

    //                 if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledReverseRewinding)
    //                 {
    //                     instructionsText.text = "Reverse Object: Q";
    //                 }
    //             }
    //         }

    //         if (foundObject != null)
    //         {
    //             instructionsText.text = "Pick up: Left Mouse";
    //         }
    // }

    void Start()
    {
        playerUIdata = playerRef.GetComponent<IPlayerUI>();
    }

    void Update()
    {

        CarriableTimeObj foundObject = PlayerSelection.GetObjectReference<CarriableTimeObj>(2, cameraPosition);

        if (PauseMenu.isPaused == false)
        {

            // Remove outline from the previously selected object
            if (_selection != null)
            {
                ResetOutline();
                _selection = null;
            }

            // Perform raycast to find selectable object
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;

                if (selection.CompareTag(selectableTag))
                {
                    if (foundObject == null && playerUIdata.isCarryingSomething() == false)
                    {
                        isHoldingObject = false;

                        instructionsText.text = "Focus on Object: F";
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
                        Highlight(selection);
                        _selection = selection;
                    }
                }
            }
        }
    }
}