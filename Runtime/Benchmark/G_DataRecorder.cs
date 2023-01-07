using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tayx.Graphy.Recorder
{
    public class G_DataRecorder
    {
        struct FrameData
        {
            public int frame;
            public int currentFps;
            public float allocRam;
            public float reservedRam;
        }

        private FrameData m_frameData;
        private List<FrameData> m_statData = null;

        public void StartRecording()
        {//Initalizing Data
            m_statData = new List<FrameData>();
            m_frameData = new FrameData();
        }

        //Can be used as Update()
        public void RecordFrameData()
        {
            //ReWrite Frame Data
            m_frameData.frame = Time.frameCount;
            m_frameData.currentFps = Mathf.RoundToInt(GraphyManager.Instance.CurrentFPS);
            m_frameData.allocRam = GraphyManager.Instance.AllocatedRam;
            m_frameData.reservedRam = GraphyManager.Instance.ReservedRam;

            //Write Frame Data to The Dictionary
            m_statData.Add(m_frameData);
        }

        public void StopRecording()
        {
            // TODO: Record The Data in a File
        }
    }

}