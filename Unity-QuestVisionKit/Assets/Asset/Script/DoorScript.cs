using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    public GameObject PostUI;
    public static DoorScript Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        animator.SetBool("Open", true);
    }

    // Update is called once per frame
    public void ClosePortal()
    {
        animator.SetBool("Open", false);

    }
}
