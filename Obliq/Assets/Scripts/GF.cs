using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General Functions
public class GF
{
    // takes in a game object with an Entity Component holding the statemachine,
    // and the state to be changed to
    public static void ChangeState(GameObject owner, State state)
    {
        owner.GetComponent<Entity>().statemachine_.ChangeState(state);
    }

    // GetComponent<> Short
    public static T GC<T>(GameObject owner)
    {
        return owner.GetComponent<T>();
    }

    // returns mouse world position at main camera
    public static Vector2 WorldMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        return new Vector2((vector.x * Mathf.Cos(angle) + vector.y * -Mathf.Sin(angle)), (vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle)));
    }
}
