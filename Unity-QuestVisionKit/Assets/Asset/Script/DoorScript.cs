using UnityEngine;
using System;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    public GameObject PostUI;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip PortalAppear, PortalInnerLoop;
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
        GameObject.Find("Ambient SFX mellow").GetComponent<AudioSource>().volume = 0.1f;

        animator.SetBool("Open", true);
        audioSource.PlayOneShot(PortalAppear);
        StartCoroutine(ExecuteWithDelay(() => {
            audioSource.clip = PortalInnerLoop;
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            audioSource.Play();
        },2f));
        }

    IEnumerator ExecuteWithDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    // Update is called once per frame
    public void ClosePortal()
    {
        animator.SetBool("Open", false);

    }
}
