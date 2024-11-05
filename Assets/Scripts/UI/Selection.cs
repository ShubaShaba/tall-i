using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private GameObject playerRef;
    private IPlayerUI playerUIdata;
    private Transform _selection;
    public TextMeshProUGUI instructionsText;
    private string defaultInstructions = "Pick up Object: Left Mouse";

    void Start()
    {
        playerUIdata = playerRef.GetComponent<IPlayerUI>();
    }

    void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            instructionsText.text = defaultInstructions;
        

            

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
                        if (playerUIdata.isCarryingSomething()){
                            instructionsText.text = "Throw Object: Right Mouse";
                            }

            if (playerUIdata.isFocusedOnSomethingType2()){
                instructionsText.text = "Rewind Object: 1 \nFreeze Object: 2 \nManual Rewind: 3";
                GetComponent<Renderer>().material = originalMaterial;

            }
                    if (renderer != null)
                    {
                        // Store original material and apply outline
                        originalMaterial = renderer.material;
                        renderer.material = outlineMaterial;

                        instructionsText.text = "Focus: F";
                    
                    }
                    _selection = selection;
 
                }
            }                    

        }
    }
}
