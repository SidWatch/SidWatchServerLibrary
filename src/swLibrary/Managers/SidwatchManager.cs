using System;
using Sidwatch.Library.DAOs;
using Sidwatch.Library.Objects;
using TreeGecko.Library.Mongo.Managers;
using TreeGecko.Library.Net.DAOs;
using TreeGecko.Library.Net.Enums;
using TreeGecko.Library.Net.Objects;

namespace Sidwatch.Library.Managers
{
    public class SidwatchManager : AbstractMongoManager
    {
        public SidwatchManager() : base("SW")
        {
        }

        #region User
        public User GetUser(string _username)
        {
            UserDAO dao = new UserDAO(MongoDB);
            return dao.GetUser(_username);
        }

        public User GetUser(Guid _userGuid)
        {
            UserDAO dao = new UserDAO(MongoDB);
            return (User) dao.Get(_userGuid);
        }

        public void Persist(User _user)
        {
            UserDAO dao = new UserDAO(MongoDB);
            dao.Persist(_user);
        }

        public bool ValidateUser(User _user, string _password)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserAuthorization

        public TGUserAuthorization GetUserAuthorization(Guid _userGuid, string _authToken)
        {
            TGUserAuthorizationDAO dao = new TGUserAuthorizationDAO(MongoDB);
            return dao.Get(_userGuid, _authToken);
        }

        public void Persist(TGUserAuthorization _authorization)
        {
            TGUserAuthorizationDAO dao = new TGUserAuthorizationDAO(MongoDB);
            dao.Persist(_authorization);
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

        #endregion

        #region SiteSpectrum

        public SiteSpectrum GetSiteSpectrum(Guid _siteSpectrumGuid)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            return dao.Get(_siteSpectrumGuid);
        }

        public void Persist(SiteSpectrum _siteSpectrum)
        {
            SiteSpectrumDAO dao = new SiteSpectrumDAO(MongoDB);
            dao.Persist(_siteSpectrum);
        }

        #endregion

        #region Station

        public Station Get(Guid _stationGuid)
        {
            StationDAO dao = new StationDAO(MongoDB);
            return dao.Get(_stationGuid);
        }

        public void Persist(Station _station)
        {
            StationDAO dao = new StationDAO(MongoDB);
            dao.Persist(_station);
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

        #endregion

        #region SystemCredentials

        public SystemCredentials GetSystemCredentials(Guid _systemCredentialsGuid)
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            return dao.Get(_systemCredentialsGuid);
        }

        public void Persist(SystemCredentials _systemCredentials)
        {
            SystemCredentialsDAO dao = new SystemCredentialsDAO(MongoDB);
            dao.Persist(_systemCredentials);
        }

        #endregion

        #region Logging
        public void LogException(Guid _userGuid, string _exception)
        {
            WebLogEntry logEntry = new WebLogEntry
            {
                Active = true,
                UserGuid = _userGuid,
                Message = _exception,
                MessageDateTime = DateTime.UtcNow,
                WebLogType = LogMessageType.Exception
            };

            WebLogEntryDAO dao = new WebLogEntryDAO(MongoDB);
            dao.Persist(logEntry);
        }

        public void LogWarning(Guid _userGuid, string _warning)
        {
            WebLogEntry logEntry = new WebLogEntry
            {
                Active = true,
                UserGuid = _userGuid,
                Message = _warning,
                MessageDateTime = DateTime.UtcNow,
                WebLogType = LogMessageType.Warning
            };

            WebLogEntryDAO dao = new WebLogEntryDAO(MongoDB);
            dao.Persist(logEntry);
        }

        public void LogVerbose(Guid _userGuid, string _verbose)
        {
            WebLogEntry logEntry = new WebLogEntry
            {
                Active = true,
                UserGuid = _userGuid,
                Message = _verbose,
                MessageDateTime = DateTime.UtcNow,
                WebLogType = LogMessageType.Warning
            };

            WebLogEntryDAO dao = new WebLogEntryDAO(MongoDB);
            dao.Persist(logEntry);
        }

        public void LogInfo(Guid _userGuid, string _info)
        {
            WebLogEntry logEntry = new WebLogEntry
            {
                Active = true,
                UserGuid = _userGuid,
                Message = _info,
                MessageDateTime = DateTime.UtcNow,
                WebLogType = LogMessageType.Info
            };

            WebLogEntryDAO dao = new WebLogEntryDAO(MongoDB);
            dao.Persist(logEntry);
        }

        #endregion
    }
}
