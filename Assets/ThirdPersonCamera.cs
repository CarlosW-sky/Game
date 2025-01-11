using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonCamera
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Transform target; // 跟随的目标角色
        public float distance = 5.0f; // 摄像机与角色之间的距离
        public float sensitivityX = 4.0f; // 水平旋转灵敏度
        public float sensitivityY = 2.0f; // 垂直旋转灵敏度
        public float minY = -20f; // 垂直旋转最小角度
        public float maxY = 80f; // 垂直旋转最大角度
        public float walkSoundInterval = 0.5f; // 脚步声音播放间隔

        [Header("震动参数")]
        public float shakeDuration = 0.1f; // 震动持续时间
        public float shakeMagnitude = 0.1f; // 震动幅度

        private AudioSource audioSource;
        public AudioClip walkSound; // 脚步音效

        private float currentX = 0.0f;
        private float currentY = 0.0f;
        private Vector3 originalPosition;
        private bool isWalking = false;
        private float nextStepTime = 0f;

        // 初始化音效和位置
        private void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = walkSound;
            audioSource.loop = false;
            originalPosition = transform.position; // 记录摄像机的初始位置
        }

        private void Update()
        {
            // 检查是否在移动
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            isWalking = (horizontal != 0 || vertical != 0);

            // 处理摄像机旋转
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
            currentY = Mathf.Clamp(currentY, minY, maxY);

            if (isWalking)
            {
                // 触发脚步音效
                if (Time.time >= nextStepTime)
                {
                    audioSource.Play();
                    nextStepTime = Time.time + walkSoundInterval;
                }

                // 触发摄像机震动效果
                StartCoroutine(CameraShake());
            }
        }

        private void LateUpdate()
        {
            // 计算摄像机的位置和角度
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 direction = new Vector3(0, 0, -distance);
            transform.position = target.position + rotation * direction;

            // 使摄像机看向角色
            transform.LookAt(target.position);
        }

        private IEnumerator CameraShake()
        {
            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeMagnitude;
                transform.position = randomPoint;

                elapsed += Time.deltaTime;
                yield return null;
            }

            // 震动结束后，摄像机回到原位
            transform.position = originalPosition;
        }
    }

}
