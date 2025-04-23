using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PrePortalScript : MonoBehaviour
{

    [SerializeField] Transform canvas, mainCamera;
    [SerializeField] InputField InputField;
    [SerializeField] GameObject PassThroughCameraFeed;
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    Transform CurrentUI;

    void Start()
    {
        StartCoroutine(ShowUISequence());
    }

    private void Update()
    {
        if (overlayKeyboard != null)
            InputField.text = overlayKeyboard.text;
    }

    public void EnableKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        
    }

    public void EnableUI(int UI_No)
    {
        if(CurrentUI == null)
        {
            for (int i = 0; i < canvas.childCount; i++)
            {
                canvas.GetChild(i).gameObject.SetActive(i == UI_No);
                if (i == UI_No)
                {
                    CurrentUI = canvas.GetChild(i);
                    canvas.GetChild(i).DOScale(1f, 1f).SetEase(Ease.OutExpo);
                }
            }
        }
        else
        {
            CurrentUI.DOScale(0f, 1f).onComplete = () => {
                for (int i = 0; i < canvas.childCount; i++)
                {
                    canvas.GetChild(i).gameObject.SetActive(i == UI_No);
                    if (i == UI_No)
                    {
                        CurrentUI = canvas.GetChild(i);
                        canvas.GetChild(i).DOScale(1f, 1f).SetEase(Ease.OutExpo);
                    }
                }
            };
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
        yield return new WaitForSeconds(1f);

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
