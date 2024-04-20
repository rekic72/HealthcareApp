using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class HealthcareApp : MonoBehaviour{
    //reference to imageManager in Unity
    private ARTrackedImageManager _trackedImagesManager;

    //Array of game objects
    public GameObject[] ArPrefabs;

    //dictionary array of created objects
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //Event HAndler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //loops through everything detected
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            
            //loop over array
            foreach (var curPrefab in ArPrefabs)
            {
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    //add created prefab to the array list
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        //set all detected prefebs to active/deactive depending on whether it
        //has been detected or not
        foreach (var trackedImage in eventArgs.updated)
        {
            _instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        //lost tracked images
        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}
