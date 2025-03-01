using Michsky.UI.Reach;
using System.Collections;
using UnityEngine;

public class MapNotificationHandler : MonoBehaviour
{
    [Header("Feed Notification")]
    public CustomFeedNotification feedNotification;
    [SerializeField] private float animDuration = 3f;

    AudioSource feedSFXSource;
    [Header("Sound Effects")]
    [SerializeField] AudioClip discoverySFX;

    private void Awake()
    {
        feedSFXSource = GetComponent<AudioSource>();
    }

    public void DisplayDiscoveryDescription(string message)
    {
        if (feedNotification.gameObject.activeSelf)
        {
            StopAllCoroutines();
        }
        StartCoroutine(TransitionAndDisplay(message));
    }

    private IEnumerator TransitionAndDisplay(string message)
    {
        ShowNotification(message);
        yield return new WaitForSeconds(feedNotification.ClosingAnimDuration + 2);
        feedNotification.MinimizeNotification();
    }

    private void ShowNotification(string message)
    {
        feedNotification.notificationText = message;

        feedNotification.UpdateUI();
        feedNotification.ExpandNotification();

        feedSFXSource?.PlayOneShot(discoverySFX);
    }
}
