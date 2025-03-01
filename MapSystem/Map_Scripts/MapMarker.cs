using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(AudioSource))]
public abstract class MapMarker : MonoBehaviour
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    protected AudioSource audioSource;
    protected delegate void PointerClickHandler();
    protected PointerClickHandler clickHandler;
    protected Animator hoverAnimator;
    protected TMP_Text nameText;
    protected MapObject linkedMapObject;

    public virtual void Setup(string iconName, MapObject mapObject)
    {
        audioSource = GetComponent<AudioSource>();
        hoverAnimator = GetComponent<Animator>();
        nameText = GetComponentInChildren<TMP_Text>();
        linkedMapObject = mapObject; 
    }

    public virtual void MouseDown()
    {
        audioSource.PlayOneShot(clickSound);
        Debug.Log("Mouse down");
        clickHandler?.Invoke();
    }

    public virtual void MouseEnter()
    {
        audioSource.PlayOneShot(hoverSound);
        Debug.Log("Mouse enter");
        hoverAnimator?.SetBool("Highlighted", true);
    }

    public virtual void MouseExit()
    {
        Debug.Log("Mouse exit");
        hoverAnimator?.SetBool("Highlighted", false);
    }
}
