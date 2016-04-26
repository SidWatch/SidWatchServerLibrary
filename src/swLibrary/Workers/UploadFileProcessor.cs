using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using sharpHDF.Library.Objects;
using TreeGecko.Library.AWS.Helpers;
using TreeGecko.Library.Common.Delegates;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace Sidwatch.Library.Workers
{
    public class UploadFileProcessor : IWorker
    {
        private Thread m_ProcessignThread;
        private bool m_Processing;

        public bool IsRunning
        {
            get
            {
                return m_Processing;
            }
        }

        public void Start()
        {
            m_Processing = true;
            ThreadStart ts = Process;
            m_ProcessignThread = new Thread(ts);
            m_ProcessignThread.Start();
        }

        public void Stop()
        {
            m_Processing = false;
        }

        private void Process()
        {
            string bucket = Config.GetSettingValue("S3UploadBucket");
            string tempfolder = Config.GetSettingValue("TempFolder");
            do
            {
                List<string> files = S3Helper.ListFiles(bucket, 5);

                foreach (string file in files)
                {
                    byte[] data = S3Helper.GetFile(bucket, file);

                    string filename = Path.Combine(tempfolder, file);
                    File.WriteAllBytes(filename, data);
                    try
                    {

                        using (Hdf5File hdfFile = new Hdf5File(filename))
                        {
                            Hdf5Group group = hdfFile.Groups["PowerSpectrumData"];

                            if (group != null)
                            {
                                foreach (var dataset in group.Datasets)
                                {
                                    string datasetName = dataset.Name;

                                    DateTime datasetTime;
                                    if (DateTime.TryParse(datasetName, out datasetTime))
                                    {
                                        int nfft = Convert.ToInt32(dataset.Attributes["nfft"].Value);

                                        Array psd = dataset.GetData();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                    finally
                    {
                        
                    }




                    if (!m_Processing)
                    {
                        break;
                    }
                }

                Stop();

            } while (m_Processing);
        }

        public event WorkCompleteHandler WorkComplete;
    }
}
