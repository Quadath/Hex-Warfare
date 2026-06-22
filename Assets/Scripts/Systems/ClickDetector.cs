using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class ClickDetector : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        
        public PlanetView planetView;
	    
        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out hit))
                {
                    planetView.OnClicked(hit.point);
                }
            }
        }
    }
}
