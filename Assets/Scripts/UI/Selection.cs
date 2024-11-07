using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string selectableTag2 = "Keycard";
    [SerializeField] private string selectableTag3 = "Holder";
    [SerializeField] private string selectableTag4 = "Plataform";
    [SerializeField] private string selectableTag5 = "Generator";
    [SerializeField] private string selectableTag6 = "Scanner";



    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject playerRef;
    private IPlayerUI playerUIdata;
    private bool isHoldingObject;

    private Transform _selection;
    public TextMeshProUGUI instructionsText;

    void Start()
    {
        playerUIdata = playerRef.GetComponent<IPlayerUI>();
    }

    void Update()
    {

        CarriableTimeObj foundObject = PlayerSelection.GetObjectReference<CarriableTimeObj>(2, cameraPosition);




        if (PauseMenu.isPaused == false)
        {

            instructionsText.text = "";

            if (playerUIdata.isCarryingSomething() && isHoldingObject == false)
            {
                instructionsText.text = "Throw: Right Mouse";
            }

            if (playerUIdata.isFocusedOnSomethingType1())
            {
                instructionsText.text = "Rewind Object: 1";
            }
            else
            {
                if (playerUIdata.isFocusedOnSomethingType2())
                {
                    instructionsText.text = "Rewind Object: 1 \nFreeze Object: 2 \nManual Rewind: 3";

                    if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.Stoped)
                    {
                        instructionsText.text = "Rewind Object: 1 \nManual Rewind: 3";
                    }
                    if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.Rewinding)
                    {
                        instructionsText.text = "Freeze Object: 2 \nManual Rewind: 3";
                    }

                    if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledRewinding)
                    {
                        instructionsText.text = "Forward Object: E";
                    }
                    if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledStoped)
                    {
                        instructionsText.text = "Reverse Object: Q \nForward Object: E";
                    }

                    if (playerUIdata.GetCurrentFocusState() == TimeBodyStates.ControlledReverseRewinding)
                    {
                        instructionsText.text = "Reverse Object: Q";
                    }
                }
            }

            if (foundObject != null)
            {
                instructionsText.text = "Pick up: Left Mouse";
            }

            // Remove outline from the previously selected object
            if (_selection != null)
            {
                var renderer = _selection.GetComponent<Renderer>();
                if (renderer != null && originalMaterial != null)
                {
                    renderer.material = originalMaterial;
                }
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
                    var renderer = selection.GetComponent<Renderer>();
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
                        if (renderer != null)
                        {
                            // Store original material and apply outline
                            originalMaterial = renderer.material;
                            renderer.material = outlineMaterial;
                        }
                        _selection = selection;

                    }
                }
                if (selection.CompareTag(selectableTag2) && playerUIdata.isCarryingSomething() == false)
                {
                    instructionsText.text = "     Keycard \nPick Up: Left Mouse (Hold)";

                    isHoldingObject = true;
                }

                if (playerUIdata.isCarryingSomething() && isHoldingObject == true)
                {
                    instructionsText.text = "   Use in Scanner";
                }
                else
                {
                }


                if (selection.CompareTag(selectableTag3) && playerUIdata.isCarryingSomething() == false)
                {
                    instructionsText.text = "        Button \nLeft Mouse: Press Button \n";
                    isHoldingObject = false;
                }

                if (selection.CompareTag(selectableTag5) && playerUIdata.isCarryingSomething() == false && playerUIdata.isFocusedOnSomethingType2() == false)
                {
                    instructionsText.text = "Generator: Needs Battery \nLeft Mouse: Turn On Generator \n      Focus: F \n";
                    isHoldingObject = false;
                }

                if (selection.CompareTag(selectableTag4) && playerUIdata.isCarryingSomething() == false)
                {
                    instructionsText.text = "Button Plataform \n Stand to press";
                    isHoldingObject = false;

                }
                if (selection.CompareTag(selectableTag6) && playerUIdata.isCarryingSomething() == false)
                {
                    instructionsText.text = "   Keycard Scanner \n Left Mouse: Use Keycard \n Right Mouse: Remove Keycard";
                    isHoldingObject = false;

                }
            }
        }
    }
}