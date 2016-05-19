using System;
using System.Collections.Generic;
using Sidwatch.Library.DAOs;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Net.Managers;
using TreeGecko.Library.Net.Objects;

namespace Sidwatch.Library.Managers
{
    public class SidWatchManager : AbstractCoreManager
    {
        public SidWatchManager() 
            : base("SW")
        {
        }

        #region User

        public User GetUser(string _username)
        {
            UserDAO dao = new UserDAO(MongoDB);
            return dao.Get(_username);
        }

        public User GetUserByEmail(string _email)
        {
            UserDAO dao = new UserDAO(MongoDB);
            return dao.GetByEmail(_email);
        }

        public User GetUser(Guid _userGuid)
        {
            UserDAO dao = new UserDAO(MongoDB);
            return dao.Get(_userGuid);
        }

        public void Persist(User _user)
        {
            UserDAO dao = new UserDAO(MongoDB);
            dao.Persist(_user);
        }

        public List<User> GetUsers()
        {
            UserDAO dao = new UserDAO(MongoDB);

            return dao.GetAll();
        }

        public List<User> GetActiveUsers()
        {
            UserDAO dao = new UserDAO(MongoDB);

            return dao.GetActive();
        }

        public bool ValidateUser(string _username, string _authToken, out User _user)
        {
            _user = GetUser(_username);

            if (_user != null)
            {
                TGUserAuthorization userAuthorization = GetUserAuthorization(_user.Guid, _authToken);

                if (userAuthorization != null)
                {
                    return true;
                }
                
                _user = null;
            }

            return false;
        }

        #endregion

        #region DataFile

        public DataFile GetDataFile(Guid _dataFileGuid)
        {
            DataFileDAO dao = new DataFileDAO(MongoDB);
            return dao.Get(_dataFileGuid);
        }

        public void Persist(DataFile _dataFile)
        {
            DataFileDAO dao = new DataFileDAO(MongoDB);
            dao.Persist(_dataFile);
        }

        public List<DataFile> GetDataFiles(Guid _siteGuid)
        {
            DataFileDAO dao = new DataFileDAO(MongoDB);
            return dao.GetDataFilesBySite(_siteGuid);
        }

        #endregion

        #region Site

        public Site GetSite(Guid _siteGuid)
        {
            SiteDAO dao = new SiteDAO(MongoDB);
            return dao.Get(_siteGuid);
        }

        public void Persist(Site _site)
        {
            SiteDAO dao = new SiteDAO(MongoDB);
            dao.Persist(_site);
        }

        public List<Site> GetSites()
        {
            SiteDAO dao = new SiteDAO(MongoDB);
            return dao.GetAll();
        }

        public List<Site> GetActiveSites()
        {
            SiteDAO dao = new SiteDAO(MongoDB);
            return dao.GetActive();
        }

        #endregion

        #region SiteDay

        public SiteDay GetSiteDay(Guid _siteGuid, DateTime _date)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            return dao.Get(_siteGuid, _date);
        }

        public SiteDay GetSiteDay(Guid _siteDayGuid)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            return dao.Get(_siteDayGuid);
        }

        public List<SiteDay> GetSiteDays(Guid _siteGuid)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            return dao.GetSiteDays(_siteGuid);
        }

        public List<SiteDay> GetSiteDays(Guid _siteGuid, DateTime _startDate)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            return dao.GetSiteDays(_siteGuid, _startDate.Date);
        }
        
        public List<SiteDay> GetSiteDays(Guid _siteGuid, DateTime _startDate, DateTime _endDate)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            return dao.GetSiteDays(_siteGuid, _startDate.Date, _endDate.Date);
        }

        public void Persist(SiteDay _siteDay)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            dao.Persist(_siteDay);
        }

        public void AddFileToSiteDay(Guid _siteGuid, DateTime _date)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            SiteDay sd = dao.Get(_siteGuid, _date.Date);

            if (sd == null)
            {
                sd = new SiteDay
                {
                    Active = true,
                    DataFileCount = 0,
                    Date = _date.Date,
                    Guid = Guid.NewGuid(),
                    ParentGuid = _siteGuid
                };
            }

            sd.DataFileCount++;
            dao.Persist(sd);
        }

        public void SetSiteDayFiles(Guid _siteGuid, DateTime _date, int _count)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            SiteDay sd = dao.Get(_siteGuid, _date.Date);

            if (sd == null)
            {
                sd = new SiteDay
                {
                    Active = true,
                    Date = _date.Date,
                    Guid = Guid.NewGuid(),
                    ParentGuid = _siteGuid
                };
            }

            sd.DataFileCount = _count;
            dao.Persist(sd);
        }

        public DateTime? GetMinDay(Guid _siteGuid)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            SiteDay minDay = dao.GetMinSiteDay(_siteGuid);

            if (minDay == null)
            {
                return null;
            }

            return minDay.Date;
        }

        public DateTime? GetMaxDay(Guid _siteGuid)
        {
            SiteDayDAO dao = new SiteDayDAO(MongoDB);
            SiteDay minDay = dao.GetMaxSiteDay(_siteGuid);

            if (minDay == null)
            {
                return null;
            }

            return minDay.Date;
        }

        #endregion

        #region SiteSpectrum

        public SiteSpectrum GetSiteSpectrum(Guid _siteSpectrumGuid)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            return dao.Get(_siteSpectrumGuid);
        }

        public SiteSpectrum GetLatestSiteSpectrum(Guid _siteGuid)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            return dao.GetLatest(_siteGuid);
        }

        public List<SiteSpectrum> GetSiteSpectrums(Guid _siteGuid, DateTime _startDateTime, DateTime _endDateTime)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            return dao.GetSiteSpectrums(_siteGuid, _startDateTime, _endDateTime);
        }

        public void Persist(SiteSpectrum _siteSpectrum)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            dao.Persist(_siteSpectrum);
        }

        #endregion

        #region Station

        public Station GetStation(Guid _stationGuid)
        {
            StationDAO dao = new StationDAO(MongoDB);
            return dao.Get(_stationGuid);
        }

        public void Persist(Station _station)
        {
            StationDAO dao = new StationDAO(MongoDB);
            dao.Persist(_station);
        }

        public List<Station> GetStations()
        {
            StationDAO dao = new StationDAO(MongoDB);
            return dao.GetAll();
        }

        public List<Station> GetActiveStations()
        {
            StationDAO dao = new StationDAO(MongoDB);
            return dao.GetActive();
        }

        #endregion

        #region StationReading

        public StationReading GetStationReading(Guid _stationReadingGuid)
        {
            StationReadingDAO dao = new StationReadingDAO(MongoDB);
            return dao.Get(_stationReadingGuid);
        }

        public void Persist(StationReading _stationReading)
        {
            StationReadingDAO dao = new StationReadingDAO(MongoDB);
            dao.Persist(_stationReading);
        }

        public List<StationReading> GetStationReadings(Guid _stationGuid, Guid _siteGuid, DateTime _startDateTime, DateTime _endDateTime)
        {
            StationReadingDAO dao = new StationReadingDAO(MongoDB);

            return dao.GetReadings(_stationGuid, _siteGuid, _startDateTime, _endDateTime);
        }

        #endregion

        #region SystemCredentials

        public SystemCredentials GetSystemCredential(Guid _systemCredentialsGuid)
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            return dao.Get(_systemCredentialsGuid);
        }

        public void Persist(SystemCredentials _systemCredentials)
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            dao.Persist(_systemCredentials);
        }

        public SystemCredentials GetLatestActive()
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            return dao.GetLatestActive();
        }

        public List<SystemCredentials> GetSystemCredentials()
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            return dao.GetAll();
        }

        public List<SystemCredentials> GetActiveSystemCredentials()
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            return dao.GetActive();
        }

        #endregion

    }
}
