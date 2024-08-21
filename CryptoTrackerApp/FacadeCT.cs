using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CryptoTrackerApp.DataAccessLayer.EntityFramework;
using CryptoTrackerApp.DTO;
using System.Globalization;
using System.Text.RegularExpressions;
using CryptoTrackerApp.Domain;
using System.Windows.Forms;
using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp.Visual;
using ScottPlot.Renderable;
using Quartz.Impl.AdoJobStore.Common;
using CryptoTrackerApp.EmailService;
using CryptoTrackerApp.Api.Exceptions;
using Microsoft.VisualBasic.ApplicationServices;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.EmailService;
using Microsoft.VisualBasic.Logging;
using CryptoTrackerApp;

namespace CryptoTrackerApp
{
    public class FacadeCT
    {
        CoinCapApiClient cryptoInteraction = new CoinCapApiClient();

        public List<CryptoDTO> GetFavoriteList(User user)
        {
            try
            {
                DatabaseHelper context = new DatabaseHelper();
                RepositoryUser userRepo = new RepositoryUser(context);
                string[] result = user.FavCryptos.Split(' ');
                List<string> favoriteList = result.ToList();
                List<CryptoDTO> cryptoDTOList = cryptoInteraction.GetFavCryptosDTO(favoriteList);
                return cryptoDTOList;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    //LoginForm.log.Error("Error: no response from the service");
                    //throw new ApiException("An error occurred with the cryptocurrency data service, please try again later");
                }
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                    // LoginForm.log.Error("Error: {0} " + errorText);
                    //throw new ApiException("Connection error with the service, please try again later");
                }
            }
            catch (Exception ex)
            {
                //LoginForm.log.Error("Error: {0} " + ex.Message);
                //throw new ApiException("Connection error with the service, please try again later");
            }
        }

        public List<CryptoDTO> GetAllCryptoDTO()
        {
            try
            {
                CoinCapApiClient apiInteraction = new CoinCapApiClient();
                return apiInteraction.GetAllCryptosDTO();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    //LoginForm.log.Error("Error: no response from the service");
                    //throw new ApiException("An error occurred with the cryptocurrency data service, please try again later");
                }
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                    //LoginForm.log.Error("Error: {0} " + errorText);
                    //throw new ApiException("Connection error with the service, please try again later");
                }
            }
            catch (Exception ex)
            {
                //LoginForm.log.Error("Error: {0} " + ex.Message);
                //throw new ApiException("Connection error with the service, please try again later");
            }
        }

        public IEnumerable<Alert> GetAllAlerts()
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryAlerts alertRepo = new RepositoryAlerts(context);
            return alertRepo.GetAll();
        }

        public List<HistoryItem> GetHistoryFrom(string crypto)
        {
            try
            {
                DatabaseHelper apiInteraction = new DatabaseHelper();
                var history = apiInteraction.Get6MonthHistoryFrom(crypto);
                return history;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    LoginForm.log.Error("Error: no response from the service");
                    throw new ApiException("An error occurred with the cryptocurrency data service, please try again later");
                }
                else
                {
                    WebResponse errorResponse = ex.Response;
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        string errorText = reader.ReadToEnd();
                        LoginForm.log.Error("Error: {0} " + errorText);
                        LoginForm.log.Debug("Empty service response, crypto not found or API did not respond");
                        List<HistoryItem> empty = new List<HistoryItem>();
                        return empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LoginForm.log.Error("Error: {0} " + ex.Message);
                throw new ApiException("Connection error with the service, please try again later");
            }
        }

        public void AddFavoriteCrypto(string crypto)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            using (IUnitOfWork uow = new UnitOfWork(new DatabaseHelper()))
            {
                var user = userRepo.GetCurrentUser();
                user.FavCryptos = user.FavCryptos + " " + crypto;
                context.SaveChanges();
            }
        }

        public User GetCurrentUser()
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            return userRepo.GetCurrentUser();
        }

        public bool UserExists(string nickname)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var searched = userRepo.Get(nickname);
            return searched != null;
        }

        public void AddNewUser(string nickname, string firstName, string lastName, string password, string email)
        {
            using (IUnitOfWork uow = new UnitOfWork(new DatabaseHelper()))
            {
                User user = new User(nickname, firstName, lastName, password, email, "", 0, false);
                uow.RepositoryUser.Add(user);
                uow.Complete();
                Login.log.Info("Registration Successful");
            }
        }

        public User GetUser(string nickname)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            return userRepo.Get(nickname);
        }

        public void RemoveFavoriteCrypto(string crypto)
        {
            Utilities utilities = new Utilities();
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            string favoriteCryptos = "";
            var user = userRepo.GetCurrentUser();
            string[] cryptoArray = user.FavCryptos.Split(' ');
            cryptoArray[utilities.GetCryptoIndexToRemove(crypto)] = null;
            foreach (var cryptoName in cryptoArray)
            {
                if (cryptoName != "")
                {
                    favoriteCryptos = favoriteCryptos + " " + cryptoName;
                }
            }
            using (IUnitOfWork uow = new UnitOfWork(new DatabaseHelper()))
            {
                user.FavCryptos = favoriteCryptos;
                context.SaveChanges();
            }
        }

        public void ChangeThreshold(string threshold, NumberFormatInfo provider)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.Threshold = double.Parse(threshold, provider);
            context.SaveChanges();
        }

        public void RemoveAlertByIndex(int index)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryAlerts alertRepo = new RepositoryAlerts(context);
            var alertList = this.GetAllAlerts();
            var i = -1;
            if (alertList.Count() > 0)
            {
                foreach (var alert in alertList)
                {
                    i++;
                    if (index == i)
                    {
                        alertRepo.Remove(alert);
                    }
                }
                context.SaveChanges();
            }
        }

        public void RemoveAllAlertsFrom(string nickname)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryAlerts alertRepo = new RepositoryAlerts(context);
            var alertList = this.GetAllAlerts();
            if (alertList.Count() > 0)
            {
                foreach (var alert in alertList)
                {
                    if (nickname == alert.UserId)
                    {
                        alertRepo.Remove(alert);
                    }
                }
                context.SaveChanges();
            }
        }

        public static void ActivateSession(string nickname)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.Get(nickname);
            user.ActiveSession = true;
            context.SaveChanges();
        }

        public static void DeactivateSession()
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var allUsers = userRepo.GetAll();
            foreach (var user in allUsers)
            {
                if (user.ActiveSession)
                {
                    user.ActiveSession = false;
                }
            }
            context.SaveChanges();
        }

        public void ChangeFirstName(string firstName)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.FirstName = firstName;
            context.SaveChanges();
        }

        public void ChangeLastName(string lastName)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.LastName = lastName;
            context.SaveChanges();
        }

        public void ChangePassword(string password)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.Password = password;
            context.SaveChanges();
        }

        public void ChangeEmail(string email)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.Email = email;
            context.SaveChanges();
        }

        public void ChangeThreshold(double threshold)
        {
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            var user = userRepo.GetCurrentUser();
            user.Threshold = threshold;
            context.SaveChanges();
        }

        public void QuartzJob()
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            MailService mail = new MailService();
            Facade facade = new Facade();
            DatabaseHelper context = new DatabaseHelper();
            RepositoryUser userRepo = new RepositoryUser(context);
            RepositoryAlerts alertRepo = new RepositoryAlerts(context);
            provider.NumberGroupSeparator = ",";
            provider.NumberDecimalSeparator = ".";
            var allUsers = userRepo.GetAll();

            List<User> userList = new List<User>();
            foreach (var user in allUsers)
            {
                userList.Add(user);
            }

            foreach (var user in userList)
            {
                var favoriteList = facade.GetFavoriteList(user);
                foreach (var crypto in favoriteList)
                {
                    if (user.Threshold < Math.Abs(double.Parse(crypto.ChangePercent24hs, provider)))
                    {
                        Alert alert = new Alert
                        {
                            CryptoId = crypto.Name,
                            UserId = user.Nickname,
                            Date = DateTime.Now,
                            AlertThreshold = double.Parse(crypto.ChangePercent24hs, provider)
                        };
                        alertRepo.Add(alert);
                        context.SaveChanges();
                    }
                    Login.log.Info("Mail sent to " + user.Email);
                }
            }

            mail.CreateEmailMessage();
        }
    }
}
