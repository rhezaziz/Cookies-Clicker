using UnityEngine;
using DG.Tweening;
public class Clicker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem particle;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        animatedPress();
        GameManager.instance.clicking();
    }

    private void OnMouseUp()
    {
        animatedUnpress();

        particle.Play();
    }

    void animatedPress()
    {
        transform.DOScale(Vector2.one * .85f, .1f);
    }

    void animatedUnpress()
    {
        transform.DOScale(Vector2.one, .1f);
    }
}
