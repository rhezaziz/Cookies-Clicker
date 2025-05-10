using UnityEngine;
using DG.Tweening;
public class Clicker : MonoBehaviour
{
    public ParticleSystem particle;


    private void OnMouseDown()
    {
        animatedPress();
        GameManager.instance.tambahCookie();
    }

    private void OnMouseUp()
    {
        animatedUnpress();
        GameManager.instance.questManager.ReportQuest(QuestType.Clicking, 1);
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

    public void animated()
    {
        transform.DOKill();
        transform.DOPunchScale(Vector3.one * -.15f, .5f, 5);
        particle.Play();
    }
}
