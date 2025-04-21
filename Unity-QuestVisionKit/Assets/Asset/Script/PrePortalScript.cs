using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PrePortalScript : MonoBehaviour
{

    [SerializeField] Transform canvas, mainCamera;
    [SerializeField] InputField InputField;
    [SerializeField] GameObject PassThroughCameraFeed;


    void Start()
    {
        StartCoroutine(ShowUISequence());
    }


    public void EnableUI(int UI_No)
    {
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(i == UI_No);
        }
    }
    public void SaveNumberAndProceed()
    {
        if(Regex.IsMatch(InputField.text, @"^\d{10}$"))
        {
            EnableUI(3);
        }
    }

    IEnumerator ExecuteWithDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    IEnumerator ShowUISequence()
    {
        float distanceFromCamera = 0.8f; // Adjust as needed
        transform.position = mainCamera.position + (mainCamera.forward * distanceFromCamera) - new Vector3(0, 0f, 0);

        transform.LookAt(mainCamera);
        transform.eulerAngles += new Vector3(0, 180, 0);
        yield return new WaitForSeconds(2f);
        EnableUI(0);

        yield return new WaitForSeconds(4f);
        EnableUI(1);

        yield return new WaitForSeconds(3f);
        EnableUI(2);
    }

    public void QREnabler()
    {
        EnableUI(4);
        StartCoroutine(ExecuteWithDelay(() => {
            EnableUI(-1);
            PassThroughCameraFeed.SetActive(true);
        }, 2f));
    }
}
