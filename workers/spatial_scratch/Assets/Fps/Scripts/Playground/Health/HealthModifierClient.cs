using UnityEngine;
using Improbable.Gdk.Health;
using Improbable.Gdk.Subscriptions;
using Improbable.Gdk.Standardtypes;

namespace Fps.Playground
{
    public class HealthModifierClient : MonoBehaviour
    {
        [Require] HealthComponentCommandSender healthCommandSender;
        [Require] HealthComponentReader healthReader;

        LinkedEntityComponent localPlayerLink;
        LinkedEntityComponent LocalPlayerLink
        {
            get { return localPlayerLink ?? (localPlayerLink = GetComponent<LinkedEntityComponent>()); }
        }

        private void OnEnable()
        {
            if (healthReader != null)
            {
                healthReader.OnHealthModifiedEvent += OnHealthModified;
            }
        }

        private void OnHealthModified(HealthModifiedInfo obj)
        {
            Debug.Log(string.Format(
                "Received health modified event: Before {0}, After {1}", obj.HealthBefore, obj.HealthAfter));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && LocalPlayerLink)
            {
                healthCommandSender.SendModifyHealthCommand(BuildModifyHealthCommand(10), OnModifyHealthResponse);
            }
            else if (Input.GetMouseButtonDown(1) && LocalPlayerLink)
            {
                healthCommandSender.SendModifyHealthCommand(BuildModifyHealthCommand(-10), OnModifyHealthResponse);
            }
        }

        private HealthComponent.ModifyHealth.Request BuildModifyHealthCommand(float delta)
        {
            var zeroStandardType = new IntAbsolute();
            var healthModifier = new HealthModifier(0, delta, zeroStandardType, zeroStandardType);

            return new HealthComponent.ModifyHealth.Request(
                LocalPlayerLink.EntityId,
                healthModifier);
        }

        private void OnModifyHealthResponse(HealthComponent.ModifyHealth.ReceivedResponse response)
        {
            Debug.Log("Received response: " + response.StatusCode + ", " + response.Message);
        }
    }
}

