/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {

        public GameObject m_objAnimal;

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            m_objAnimal = transform.GetChild(0).gameObject;
            if (!m_objAnimal)
            {
#if UNITY_EDITOR
                Debug.Log("thang nay khong co thang con!");
#endif
            }
            else
            {
                m_objAnimal.SetActive(false);
            }

        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        public void OnTrackingFound()
        {
            Debug.Log("Da tracking dc");
            if (!GameConfig.m_isStart)
            {
                return;
            }
            if (m_objAnimal && !m_objAnimal.activeInHierarchy)
            {
                m_objAnimal.SetActive(true);
            }
            Controller.Instance.AddAnimalTracked(m_objAnimal.transform.GetChild(0).gameObject);
            ManagerObject.Instance.SpawnObjectByType(ObjectType.PARTICLE, PoolName.pool);

        }


        public void OnTrackingLost()
        {
            if (!GameConfig.m_isStart)
            {
                return;
            }
            if (m_objAnimal && m_objAnimal.activeInHierarchy)
            {
                m_objAnimal.SetActive(false);
                //SwitchMode.Instance.nameAnimal = "";
            }
            if (m_objAnimal)
            {
                Controller.Instance.RemoveAnimalTracked(m_objAnimal.transform.GetChild(0).gameObject);

            }
        }

        #endregion // PRIVATE_METHODS
    }
}
