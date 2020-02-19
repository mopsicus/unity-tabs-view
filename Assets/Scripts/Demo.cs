using System;
using System.Collections;
using System.Collections.Generic;
using Mopsicus.Tab;
using UnityEngine;

public class Demo : MonoBehaviour {

    /// <summary>
    /// Link to TabsController
    /// </summary>
    [SerializeField]
    private TabsController Controller = null;

    /// <summary>
    /// Init callback for detect change tab
    /// Activate first tab
    /// </summary>
    void Start () {
        if (Controller == null) {
#if DEBUG
            Debug.LogError ("TabsController should be initialized");
#endif
            return;
        }
        Controller.OnStateChange += OnTabChange;
        Controller.Activate (0, TabDirection.NONE);
    }

    /// <summary>
    /// Callback on tab changed
    /// </summary>
    /// <param name="index">Tab index</param>
    private void OnTabChange (int index) {
        Debug.LogFormat ("Tab {0} shows", index);
    }

}