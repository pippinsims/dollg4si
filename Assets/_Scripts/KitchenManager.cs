using UnityEngine;
using UnityEngine.InputSystem;

public class KitchenManager : MonoBehaviour
{
    public GameObject soupCan;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.5) 
        {
            timer = 0;
            for(int i = 0; i <= Random.Range(1, 3); i++ )
            {
            Instantiate(soupCan, transform.position, Quaternion.identity);
            }
        }
    }
}
