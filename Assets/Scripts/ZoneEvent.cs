using UnityEngine;
using UltEvents;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class ZoneEvent : MonoBehaviour
{
    public string[] IPWhitelist;
    public UltEvent ZoneEnterEvent = new();
    public UltEvent ZoneExitEvent = new();

    void OnTriggerEnter(Collider other)
    {
        OnTriggerETC("Enter", ZoneEnterEvent, other.gameObject);
    }

    public void OnTriggerETC(string key, UltEvent events, GameObject other)
    {
        var ip = other.GetComponentInParent<ZoneIP>();
        if (ip) Debug.Log($"Trigger {key}: {ip.IP}");
        else
        {
            Debug.Log($"Trigger {key}: {other.gameObject.layer}");
            return;
        }
        if (IPWhitelist.Contains(ip.IP))
        {
            Debug.Log($"Zone {key}");
            events?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        OnTriggerETC("Exit", ZoneExitEvent, other.gameObject);
    }
}
