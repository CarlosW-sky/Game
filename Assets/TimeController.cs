using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


namespace TimeController
{
    public class TimeController : MonoBehaviour
    {   //时间加速
        [SerializeField] private float timeMultiplier;
        //开始时间
        [SerializeField] private float startHour;
        //时间显示
        [SerializeField] private TextMeshProUGUI timeText;
        //太阳光
        [SerializeField] private Light sunLight;
        //日出时间
        [SerializeField] private float sunriseHour;
        //日落时间
        [SerializeField] private float sunsetHour;
        //白天环境光
        [SerializeField] private Color dayAmbientLight;
        //黑夜环境光
        [SerializeField] private Color nightAmbientLight;
        //光线变化曲线
        [SerializeField] private AnimationCurve lightChangeCurve;
        //最大太阳光强度
        [SerializeField] private float maxSunLightIntensity;
        //月光
        [SerializeField] private Light moonLight;
        //最大月光强度
        [SerializeField] private float maxMoonLightIntensity;
        //阿曼达任务
        [SerializeField] private float amandaQuest;
        //时间
        private DateTime currentTime;
        //日出时间
        private TimeSpan sunriseTime;
        //日落时间
        private TimeSpan sunsetTime;

        // Start is called before the first frame update
        void Start()
        {
            currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

            sunriseTime = TimeSpan.FromHours(sunriseHour);
            sunsetTime = TimeSpan.FromHours(sunsetHour);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTimeOfDay(); //1
            RotateSun(); //2
            UpdateLightSettings();//3
        }
        //1
        private void UpdateTimeOfDay()
        {
            currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
                                         
            if (timeText != null)
            {
                // 24form hour00:00
                timeText.text = currentTime.ToString("HH:mm");
            }
        }
        //2
        private void RotateSun()
        {
            float sunLightRotation;

            if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
            {
                TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
                TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

                double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

                sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
            }
            else
            {
                TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
                TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

                double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

                sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
            }

            sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        }
        //3
        private void UpdateLightSettings()
        {
            float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
            sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
            moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
            RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
        }

        private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
        {
            TimeSpan difference = toTime - fromTime;

            if (difference.TotalSeconds < 0)
            {
                difference += TimeSpan.FromHours(24);
            }
            return difference;
        }
    }
}

