﻿using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class DetectionArea : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;

        public Action OnDetection;

        private void OnEnable()
        {
            triggerObserver.TriggerEnter += HandTriggerEnter;
        }

        private void HandTriggerEnter(Collider other)
        {
            OnDetection?.Invoke();
        }

        private void OnDisable()
        {
            triggerObserver.TriggerEnter -= HandTriggerEnter;
        }
    }
}