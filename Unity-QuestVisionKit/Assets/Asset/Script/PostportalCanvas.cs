using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LocationInfo
{
    public string name;
    public Sprite mainImage;
    public string locHeaderText;
    public string detailText;
    public string top5PlacesTitle;
    public string top5Places;
    public GameObject relatedObject;
}

public class PostportalCanvas : MonoBehaviour
{

    [SerializeField] Transform UpperPlane, LowerPlane,DetailSrc,EndSrc;
    [Space(10)]

    public Image mainImage;
    public TMP_Text locHeaderText;
    public TMP_Text detailText;
    public TMP_Text top5PlacesTitle;
    public TMP_Text top5Places;
    [Space(10)]
    [SerializeField] List<LocationInfo> LocationData;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //OnOptionSelect(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOptionSelect(int OptionNo)
    {
        LowerPlane.DOScale(0f, 1f).onComplete = () => {
            LowerPlane.gameObject.SetActive(false);
            StartCoroutine(ExecuteWithDelay(() => {
                UpperPlane.gameObject.SetActive(true);
                SetOptionData(OptionNo);
                UpperPlane.DOScale(1f, 1f).onComplete = () => { 
                
                };
            }, 1f));
        };
    }

    public void ExploreOtherLocations()
    {
        UpperPlane.DOScale(0f, 1f).onComplete = () => {
            UpperPlane.gameObject.SetActive(false);
            StartCoroutine(ExecuteWithDelay(() => {
                LowerPlane.gameObject.SetActive(true);
                LowerPlane.DOScale(1f, 1f).onComplete = () => {

                };
            }, 1f));
        };
    }

    public void LeaveRealm()
    {
        DetailSrc.DOScale(0f, 1f).onComplete = () => {
            UpperPlane.gameObject.SetActive(false);
            StartCoroutine(ExecuteWithDelay(() => {
                LowerPlane.gameObject.SetActive(true);
                LowerPlane.DOScale(1f, 1f).onComplete = () => {

                };
            }, 1f));
        };
    }


    void SetOptionData(int Option)
    {
        EndSrc.gameObject.SetActive(false);
        DetailSrc.gameObject.SetActive(true);
        mainImage.sprite = LocationData[Option].mainImage;
        locHeaderText.text = LocationData[Option].locHeaderText;
        detailText.text = LocationData[Option].detailText;
        top5PlacesTitle.text = LocationData[Option].top5PlacesTitle;
        top5Places.text = LocationData[Option].top5Places;
        for (int i = 0; i < LocationData.Count; i++)
        {
            LocationData[i].relatedObject.SetActive(i == Option);
        }
    }

    IEnumerator ExecuteWithDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    // Call this to restart the current scene
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Call this to quit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();

    }
}
