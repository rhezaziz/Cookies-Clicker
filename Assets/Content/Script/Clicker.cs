using UnityEngine;
using DG.Tweening;
public class Clicker : MonoBehaviour
{
    public ParticleSystem particle;


    //check object ditekan
    private void OnMouseDown()
    {
        animatedPress();
        GameManager.instance.tambahCookie();
    }


    //check object dilepas setelah ditekan
    private void OnMouseUp()
    {
        animatedUnpress();
        GameManager.instance.questManager.ReportQuest(QuestType.Clicking, 1);
        particle.Play();
    }

    /// <summary>
    /// animasi cookie pressed
    /// </summary>
    void animatedPress()
    {
        transform.DOScale(Vector2.one * .85f, .1f);
    }

    /// <summary>
    /// animasi cookie Unpressed
    /// </summary>
    void animatedUnpress()
    {
        transform.DOScale(Vector2.one, .1f);
    }
    /// <summary>
    /// Animasi cookie 
    /// </summary>
    public void animated()
    {
        transform.DOKill();
        transform.DOPunchScale(Vector3.one * -.15f, .5f, 5);
        particle.Play();
    }
}
