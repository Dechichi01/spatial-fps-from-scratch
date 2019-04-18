using Improbable.Gdk.Subscriptions;
using Improbable.Gdk.Health;
using UnityEngine;

namespace Fps.Playground
{
    public class HealthModifierServer : MonoBehaviour
    {
        [Require] HealthComponentCommandReceiver healthCommandReceiver;
        [Require] HealthComponentWriter healthWriter;

        private void OnEnable()
        {
            healthCommandReceiver.OnModifyHealthRequestReceived += OnModifyHealthRequestReceived;
        }

        private void OnModifyHealthRequestReceived(HealthComponent.ModifyHealth.ReceivedRequest obj)
        {
            Debug.Log("Request received from: " + obj.CallerWorkerId);

            HandleHealthModifier(obj.Payload);

            var response = new HealthComponent.ModifyHealth.Response(obj.RequestId, new Improbable.Common.Empty());
            healthCommandReceiver.SendModifyHealthResponse(response);
        }

        private void HandleHealthModifier(HealthModifier modifyReq)
        {
            var oldHealth = healthWriter.Data.Health;
            var newHealth = Mathf.Clamp(oldHealth + modifyReq.Amount, 0, healthWriter.Data.MaxHealth);
            var died = newHealth <= 0;

            healthWriter.SendUpdate(new HealthComponent.Update() { Health = newHealth });

            var healthModifiedInfo = new HealthModifiedInfo(modifyReq, oldHealth, newHealth, died);
            healthWriter.SendHealthModifiedEvent(healthModifiedInfo);
        }
    }

}