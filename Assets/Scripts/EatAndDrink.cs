using UnityEngine;

public class EatAndDrink : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Eat()
    {
        Debug.Log("I am Eating this : " + gameObject.name);
        // Perform specific actions, e.g., add health, play animation
        Destroy(gameObject); // Remove the object after eating
    }

  
    
}
