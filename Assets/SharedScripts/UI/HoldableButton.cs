using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GameMath.UI

{
    public class HoldableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {    
        public float rotationSpeedY = 60f;    
        private bool isPointerDown;
        public GameObject crane;
        public GameObject cable;
        public float cableXDistanceFromOrigin =-22;
        public GameObject hook;
        public GameObject trolley;
        public bool IsHeldDown => isPointerDown;
        private float currentAngle;
        public Slider slider1;
        public Slider slider2;
        public GameObject farLimit;        
        public GameObject nearLimit;
        public GameObject collisionDetector;

        void Start()
        {
            
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
        }

        
        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }

        void Update()
        {
            if (slider2 != null)
            {
                Vector3 newPosition = trolley.transform.localPosition;
                newPosition.x = Mathf.Lerp(nearLimit.transform.localPosition.x, farLimit.transform.localPosition.x, slider2.value);
                trolley.transform.localPosition = newPosition;

                // Update cable distance based on trolley position
                cableXDistanceFromOrigin = Mathf.Sqrt(Mathf.Pow(newPosition.x, 2) + Mathf.Pow(newPosition.z, 2));
            }

            if (isPointerDown)
            {
                currentAngle += rotationSpeedY * Time.deltaTime;

                float angleInRadians = currentAngle * Mathf.Deg2Rad;
                //Crane operations
                crane.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
                //Cable operations
                cable.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
                cable.transform.position = new Vector3(
                   Mathf.Cos(angleInRadians) * cableXDistanceFromOrigin,
                   cable.transform.position.y,
                   Mathf.Sin(angleInRadians) * cableXDistanceFromOrigin);

               //Hook operations
               hook.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
               hook.transform.position = new Vector3(
                   Mathf.Cos(angleInRadians) * cableXDistanceFromOrigin,
                   hook.transform.position.y,
                   Mathf.Sin(angleInRadians) * cableXDistanceFromOrigin);

                //Trolley operations
                trolley.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
                trolley.transform.position = new Vector3(
                   Mathf.Cos(angleInRadians) * cableXDistanceFromOrigin,
                   trolley.transform.position.y,
                   Mathf.Sin(angleInRadians) * cableXDistanceFromOrigin);

                //Limit operations
                farLimit.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
                farLimit.transform.position = new Vector3(
                   Mathf.Cos(angleInRadians) * cableXDistanceFromOrigin,
                   farLimit.transform.position.y,
                   Mathf.Sin(angleInRadians) * cableXDistanceFromOrigin);
                nearLimit.transform.Rotate(0, -rotationSpeedY * Time.deltaTime, 0);
                nearLimit.transform.position = new Vector3(
                   Mathf.Cos(angleInRadians) * cableXDistanceFromOrigin,
                   nearLimit.transform.position.y,
                   Mathf.Sin(angleInRadians) * cableXDistanceFromOrigin);


            }
            if (slider1.value > 0)
            {
                Vector3 newScale = cable.transform.localScale;
                newScale.y = Mathf.Lerp(0.1f, 2.5f, slider1.value);
                cable.transform.localScale = newScale;

                Vector3 newHookPosition = hook.transform.localPosition;
                newHookPosition.y = 40.2f-36.8f * slider1.value;
                hook.transform.localPosition = newHookPosition;

            }

            
        }
    }
        
}