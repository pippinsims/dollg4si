using System;
using System.Threading.Tasks;
using UnityEngine;

public class FadeController : MonoBehaviour
{   
    public static FadeController fadeControllerInstance; 
    [SerializeField] public float fadeTime;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (fadeControllerInstance == null)
            fadeControllerInstance = this; 
        
    }

    async Task Fade(float target)
    {
        print("run");
        float start = GetComponent<CanvasGroup>().alpha, t = 0;
        while(t<fadeTime){
            print(GetComponent<CanvasGroup>().alpha);
            t += Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(start, target, t/fadeTime);
            await Task.Yield();
        }
        GetComponent<CanvasGroup>().alpha = target;
    }

    public async Task FadeToBlack()
    {
        await Fade(1); 
    }

    public async Task FadeIn()
    {
        await Fade(0); 
    }
}
