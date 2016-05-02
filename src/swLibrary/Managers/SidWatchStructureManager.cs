using System;
using Sidwatch.Library.DAOs;
using TreeGecko.Library.Net.Managers;

namespace Sidwatch.Library.Managers
{
    public class SidWatchStructureManager : AbstractCoreStructureManager
    {
        public SidWatchStructureManager() 
            : base("SW")
        {
        }

        public override void BuildDB()
        {
            BuildDB(false);

            DataFileDAO dataFileDAO = new DataFileDAO(MongoDB);
            dataFileDAO.BuildTable();

            SiteDAO siteDAO = new SiteDAO(MongoDB);
            siteDAO.BuildTable();

            SiteDayDAO siteDayDAO = new SiteDayDAO(MongoDB);
            siteDayDAO.BuildTable();

            SiteSpectrumDAO siteSpectrumDAO = new SiteSpectrumDAO(MongoDB);
            siteSpectrumDAO.BuildTable();

            StationDAO stationDAO = new StationDAO(MongoDB);
            stationDAO.BuildTable();

            StationReadingDAO stationReadingDAO = new StationReadingDAO(MongoDB);
            stationReadingDAO.BuildTable();

            SystemCredentialsDAO systemCredentialsDAO = new SystemCredentialsDAO(MongoDB);
            systemCredentialsDAO.BuildTable();

            UserDAO dao = new UserDAO(MongoDB);
            dao.BuildTable();
        }
    }
}
