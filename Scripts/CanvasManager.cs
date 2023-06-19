using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    private static CanvasManager instance;
    public static CanvasManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<CanvasManager>();
                if (instance == null) {
                    GameObject obj = new GameObject("CanvasManager");
                    instance = obj.AddComponent<CanvasManager>();
                }
            }
            return instance;
        }
    }

    public Canvas Canvas;

    private void Awake() {
        Canvas = GetComponent<Canvas>();
    }
}

