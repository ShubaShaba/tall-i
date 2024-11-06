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
    private string defaultInstructions = "";

    void Start()
    {
        playerUIdata = playerRef.GetComponent<IPlayerUI>();
    }

    void Update()
    {

        CarriableTimeObj foundObject = PlayerSelection.GetObjectReference<CarriableTimeObj>(2, cameraPosition);
        
        

        if (PauseMenu.isPaused == false)
        {
            if (foundObject != null){
                instructionsText.text = "Pick up: Left Mouse";
                
                }else{
                instructionsText.text = "";
                }   
                
                         
            if (playerUIdata.isCarryingSomething()){
                instructionsText.text = "Throw: Right Mouse";
             }

        if (playerUIdata.isFocusedOnSomethingType2()){
            instructionsText.text = "Rewind Object: 1 \nFreeze Object: 2 \nManual Rewind: 3";
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
                    if (foundObject == null && playerUIdata.isFocusedOnSomethingType2() == false){
                    instructionsText.text = "Focus on Object: F";
                }
                       

                

            if (playerUIdata.isFocusedOnSomethingType2()){

                GetComponent<Renderer>().material = originalMaterial;

            }

             if (playerUIdata.isCarryingSomething()){

                GetComponent<Renderer>().material = originalMaterial;

            }
                    if (renderer != null)
                    {
                        // Store original material and apply outline
                        originalMaterial = renderer.material;
                        renderer.material = outlineMaterial;

                        
                    
                    }
                    _selection = selection;
 
                }
            }                    

        }
    }
}
