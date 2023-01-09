using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tayx.Graphy.Recorder
{
    public class G_DataRecorder
    {
        [Serializable]
        public struct FrameData
        {
            public int frame;
            public int currentFps;
            public float allocRam;
            public float reservedRam;
        }

        private FrameData m_frameData;
        private List<FrameData> m_statData = null;
        private string m_filePath;
        private bool m_writeToFile = false;
        private StreamWriter m_outputfile = null;

        public List<FrameData> RecordedData => m_statData;

        /// <summary>
        /// Starts Recoding Data.
        /// </summary>
        public void StartRecording()
        {//Initalizing Data
            m_statData = new List<FrameData>();
            m_frameData = new FrameData();
        }
        /// <summary>
        /// Starts Recoding Data.
        /// </summary>
        /// <param name="recodingPath">File Path to write data to</param>
        /// <param name="writeAtEnd">if true will write data in end else will write when recording ended</param>
        public void StartRecording(string recodingPath,bool writeAtEnd = true)
        {
            StartRecording();
            m_filePath = recodingPath;
            m_writeToFile = !writeAtEnd;
            if (m_writeToFile) m_outputfile = new StreamWriter(m_filePath, true);
            else m_outputfile = null;
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
            Debug.Log(JsonUtility.ToJson(m_frameData, true));

            if (m_writeToFile)
            {
                m_outputfile.WriteLine(JsonUtility.ToJson(m_frameData, true));
            }
        }

        /// <summary>
        /// Stops Recoding Data.
        /// </summary>
        public void StopRecording()
        {
            // TODO: Record The Data in a File
            m_outputfile.Close();
            if(m_writeToFile && m_filePath!= null)
            {
                WriteListToJsonFile(m_filePath, RecordedData);
            }
        }


        private void WriteListToJsonFile(string filePath, List<FrameData> list)
        {
            using (StreamWriter outputfile = new StreamWriter(filePath))
            {
                foreach (FrameData data in list)
                {
                    outputfile.WriteLine(JsonUtility.ToJson(data, true));
                }
            }
        }
    }

}