using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Amazon.CognitoSync.Model;
using HDF5DotNet;
using MongoDB.Bson;
using TreeGecko.Library.AWS.Helpers;
using TreeGecko.Library.Common.Delegates;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.HDF5.Objects;

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

                        using (HDF5File hdf5File = new HDF5File(filename, true))
                        {
                            using (HDF5Group group = hdf5File.GetGroup("frequency_spectrum_data"))
                            {
                                int nfft = 0;

                                using (HDF5Attribute nfftAttribute = group.GetAttribute("NFFT"))
                                {
                                    nfft = nfftAttribute.AsInt32();
                                }

                                List<string> datasetNames = group.GetChildDatasetNames();

                                foreach (string datasetName in datasetNames)
                                {
                                    using (HDF5Dataset dataset = group.GetDataset(datasetName))
                                    {
                                        DateTime datasetTime;

                                        using (HDF5Attribute timeAttribute = dataset.GetAttribute("Time"))
                                        {
                                            string sTime = timeAttribute.AsString();
                                            DateTime.TryParse(sTime, out datasetTime);
                                        }
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
