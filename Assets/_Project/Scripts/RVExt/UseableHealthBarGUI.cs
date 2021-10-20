using RVModules.RVLoadBalancer;
using UnityEngine;
using UnityEngine.UI;

namespace RVExt
{
    public class UseableHealthBarGUI : MonoBehaviour
    {
        private IUseable useable;

        [SerializeField]
        private Image healthImage;

        [SerializeField]
        private Text infoText;

        [SerializeField]
        private Text healthNumberText;

        private Transform camTransform;
        private new Transform transform;

        private bool visible = true;

        [SerializeField]
        private float visibilityDistance = 8;

        private void Awake()
        {
            transform = base.transform;
            useable = transform.parent.GetComponent<IUseable>();
            infoText.text = transform.name;
            camTransform = Camera.main.transform;
            useable.OnKilled.AddListener(() => Destroy(gameObject));

            LoadBalancerSingleton.Instance.Register(this, CheckDistance, 5);
            LoadBalancerSingleton.Instance.Register(this, Tick, 20);
            Hide();
        }

        private void Tick(float _dt)
        {
            if (!visible) return;
            var camPos = camTransform.position;
            var position = transform.position;
            transform.LookAt(position + (position - camPos));
            healthImage.fillAmount = useable.HitPoints / useable.MaxHitPoints;
            healthNumberText.text = $"{(int) useable.HitPoints}/{(int) useable.MaxHitPoints}HP";
        }

        private void OnDestroy() => LoadBalancerSingleton.Instance?.Unregister(this);

        private void CheckDistance(float _dt)
        {
            if (!visible)
            {
                if (Vector3.Distance(transform.position, camTransform.position) < visibilityDistance) Show();
            }
            else
            {
                if (Vector3.Distance(transform.position, camTransform.position) > visibilityDistance) Hide();
            }
        }

        private void Hide()
        {
            visible = false;
            gameObject.SetActive(false);
        }

        private void Show()
        {
            visible = true;
            Tick(0);
            gameObject.SetActive(true);
        }
    }
}