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
    [SerializeField] OVRPassthroughLayer passthroughLayer;
    public CanvasGroup canvasGroup;
    public MaterialOpacityMangager materialOpacityMangager;
    public GameObject Door;

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
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);

        overlayKeyboard.characterLimit = 10;
        
        overlayKeyboard.text = InputField.text;
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
                    DOVirtual.Float(0, 1, 2, val => {
                        canvasGroup.alpha = val;
                        materialOpacityMangager.UpdateRenderersAlpha(val);
                    });
                    //canvas.GetChild(i).DOScale(1f, 1f).SetEase(Ease.OutExpo);
                }
            }
        }
        else
        {
            DOVirtual.Float(1, 0, 2, val => {
                canvasGroup.alpha = val;
                materialOpacityMangager.UpdateRenderersAlpha(val);
            }).onComplete = () => {
                for (int i = 0; i < canvas.childCount; i++)
                {
                    canvas.GetChild(i).gameObject.SetActive(i == UI_No);
                    if (i == UI_No)
                    {
                        CurrentUI = canvas.GetChild(i);
                        DOVirtual.Float(0, 1, 2, val => {
                            canvasGroup.alpha = val;
                            materialOpacityMangager.UpdateRenderersAlpha(val);
                        });
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
        DOVirtual.Float(1, 0.2f, 1.5f, angle => {
            passthroughLayer.textureOpacity = angle;
        });
        
        yield return new WaitForSeconds(2.5f);

        float distanceFromCamera = 0.8f; // Adjust as needed
        transform.position = mainCamera.position + (mainCamera.forward * distanceFromCamera) - new Vector3(0, 0f, 0);
        transform.position = new Vector3(transform.position.x, mainCamera.position.y, transform.position.z);
        transform.LookAt(mainCamera);
        transform.eulerAngles += new Vector3(0, 180, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(2f);
        EnableUI(0);

        yield return new WaitForSeconds(5f);
        EnableUI(1);

        yield return new WaitForSeconds(5f);
        EnableUI(2);
        yield return new WaitForSeconds(4f);

        EnableKeyboard();
    }

    public void QREnabler()
    {
        EnableUI(4);
        StartCoroutine(ExecuteWithDelay(() => {
            PassThroughCameraFeed.SetActive(true);
            DOVirtual.Float(0.2f, 1f, 1.5f, angle =>
            {
                passthroughLayer.textureOpacity = angle;
            }).onComplete = () => { 
            EnableUI(-1);
                Door.SetActive(true);
            };

            }, 4f));
    }
}
