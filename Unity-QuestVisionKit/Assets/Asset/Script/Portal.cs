using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    int PortalCounter = 0;
    bool IsRealEnv = true;
    bool IsRealEnvCurrently = true;


    public UnityEvent onRealEnvEnter;
    public UnityEvent onVirtualEnvEnter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InPortal"))
        {
            Debug.Log("Player entered the In Portal!");
            // Do something here like enabling UI or starting animation
        }

        // Optional: check tag or component
        if (other.CompareTag("RealEnv"))
        {
            PortalCounter++;
            Debug.Log("Player entered the RealEnv!");
            IsRealEnv = true;
            // Do something here like enabling UI or starting animation
        }
        else if (other.CompareTag("FakeEnv"))
        {
            PortalCounter++;
            IsRealEnv = false;

            Debug.Log("Player entered the zone!");
            // Do something here like enabling UI or starting animation
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InPortal"))
        {
            Debug.Log("Player exit from the In Portal!");
            if(PortalCounter % 2 == 0)
            {
                if(IsRealEnvCurrently != IsRealEnv)
                {
                    if (IsRealEnv) onRealEnvEnter.Invoke();
                    else { 
                        onVirtualEnvEnter.Invoke();
                        DoorScript.Instance.PostUI.SetActive(true);
                       DoorScript.Instance.ClosePortal();
                    }

                    IsRealEnvCurrently = IsRealEnv;
                }
            }

            PortalCounter = 0;
        }

    }
}
