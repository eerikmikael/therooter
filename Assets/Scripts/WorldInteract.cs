using UnityEngine;

public class WorldInteract : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                if(hit.transform.TryGetComponent(out RootlessEnemy rootlessEnemy))
                {
                    rootlessEnemy.Hit(1f);
                }
            }            
        }
    }
}
