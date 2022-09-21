using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Playables;

public class PowerUpNotificationManager : MonoBehaviour
{
    public GameObject prefab;

    public Transform spawnPos,HUD;

    public Sprite testSprite;
    public Color testColor;
    public float testTime;
    public float yDstBetween;
    public float smoothSpeed;

    public List<PowerUpNotification> activeNotifications = new List<PowerUpNotification>();

    // Start is called before the first frame update
    public void Start()
    {
        PickUp(testColor,testSprite,testTime);
    }

    private void Update()
    {
        //Update Active Notification
        for (int i = 0; i < activeNotifications.Count; i++)
        {
            var notification = activeNotifications[i];
            if(notification == null)
            {
                activeNotifications.Remove(notification);
                continue;
            }

            var newPos = new Vector3
            {
                x = notification.transform.position.x,
                y = spawnPos.position.y + yDstBetween * i,
                z = notification.transform.position.z,

            };

            notification.MoveToPos(newPos, smoothSpeed);
        }
    }
    public void PickUp(Color color,Sprite icon, float time)
    {
         var obj = Instantiate(prefab, spawnPos.position, Quaternion.identity,HUD);

         var notif = obj.GetComponent<PowerUpNotification>();
         if (notif)
         {
             activeNotifications.Insert(0,notif);

             notif.Initialize(color,icon, time );
         }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PowerUpNotificationManager))]
[CanEditMultipleObjects]
public class PowerUpNotificationManagerEditor : Editor
{
    PowerUpNotificationManager manager;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying) return;
        
        manager = target as PowerUpNotificationManager;
        
        if(GUILayout.Button("Add Notification"))
        {
            manager.Start();
        }
    }
}
# endif
