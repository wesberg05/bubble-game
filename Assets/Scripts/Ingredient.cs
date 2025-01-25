using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ingredient : MonoBehaviour
{
    ////define unity components
    private Transform t;
    private BoxCollider2D c;

    //define colors
    public enum COLOR { NO_COLOR = -1, YELLOW, RED, BLUE, PURPLE }
    public COLOR color = 0;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = transform;
        c = GetComponent<BoxCollider2D>();
    }

}
