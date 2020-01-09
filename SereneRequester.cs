using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using MyApp.Domain.Models;
using Serenity.Services;
using Slps.ProtectionAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyApp.Domain.Utilities
{
    public static class SereneRequester
    {
        public static CookieContainer rsCookie = new CookieContainer();

        // Application Update Request       
        public static IRestResponse<RetrieveResponse<MyRow>> AppUpdateRequest<MyRow>(string AppUpdateURL, string resourceAppUpdate)
        {
            #region Application Update Request
            IRestResponse<RetrieveResponse<MyRow>> response = new RestResponse<RetrieveResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(AppUpdateURL);
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = resourceAppUpdate
                    };
                    request.AddHeader("Content-type", "application/json");
                    request.AddJsonBody(
                         new
                         {
                             EntityId = 1
                         });
                    response = restClient.Execute<RetrieveResponse<MyRow>>(request);
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Check user is LoggedIn or not
        
        public static IRestResponse<bool> IsLoggedIn(string baseUrl, string cookieName)
        {
            #region IsLoggedIn Request
            IRestResponse<bool> response = new RestResponse<bool>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = "/Account/IsLoggedIn"
                    };
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    if (rscookie == null)
                    {
                        rscookie = new CookieContainer();
                    }
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<bool>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // If you want to used data as UserDefination, so please replace UsersDTO with UserDefinition
        // Get current user row request
        public static IRestResponse<RetrieveResponse<MyRow>> GetLoginUserData<MyRow>(string baseUrl, string resource, string cookieName)
        {
            #region Get Login User Request
            IRestResponse<RetrieveResponse<MyRow>> response = new RestResponse<RetrieveResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = resource
                    };
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    if (rscookie == null)
                        rscookie = new CookieContainer();
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<RetrieveResponse<MyRow>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // SignUp request
        public static IRestResponse<RetrieveResponse<MyRow>> SignUp<MyRow>(string baseUrl, string resourceSignUp, SignUpRequest signUpRequestRow, string cookieName)
        {
            #region Get SignUp request
            IRestResponse<RetrieveResponse<MyRow>> response = new RestResponse<RetrieveResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(signUpRequestRow);
                    request.Resource = resourceSignUp;
                    request.AddParameter("application/json;charset=utf-8", json, ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    if (rscookie == null)
                        rscookie = new CookieContainer();
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<RetrieveResponse<MyRow>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Change Password      
        public static IRestResponse<RetrieveResponse<MyRow>> ChangePassword<MyRow>(string baseUrl, string resourceChangePassword, ChangePasswordRequest ChangePasswordRequestRow, string cookieName)
        {
            #region Get Change Password
            IRestResponse<RetrieveResponse<MyRow>> response = new RestResponse<RetrieveResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(ChangePasswordRequestRow);
                    request.Resource = resourceChangePassword;
                    request.AddParameter("application/json;charset=utf-8", json, ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    if (rscookie == null)
                        rscookie = new CookieContainer();
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<RetrieveResponse<MyRow>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

       

        // Login user request
        
        public static IRestResponse<ServiceResponse> Login(string baseUrl, string username, string password, string cookieName)
        {
            #region Login Service Request
            IRestResponse<ServiceResponse> response = new RestResponse<ServiceResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = "/Account/Login"
                    };
                    //Method 1 : Passing parameters in c# object
                    request.AddParameter("Username", username);
                    request.AddParameter("Password", password);

                    #region Other Method to pass JSON Parameters (Working)
                    //Method 2 : Passing parameters in Json body format
                    //request.AddHeader("Content-type", "application/json");
                    //request.AddJsonBody(
                    //    new
                    //    {
                    //        Username = txtUsername.Text,
                    //        Password = txtPassword.Password
                    //    });

                    //Method 3 : Passing parameters in Json object string format
                    //dynamic jsonParameters = new JObject();
                    //jsonParameters.Username = txtUsername.Text;
                    //jsonParameters.Password = txtPassword.Password;
                    //var parameters = jsonParameters.ToString();
                    //request.AddParameter("application/json", parameters, ParameterType.RequestBody);
                    #endregion

                    restClient.CookieContainer = new CookieContainer();
                    response = restClient.Execute<ServiceResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        public static IRestResponse<SaveResponse> Sync<MyRow>(string baseUrl, string resource, SaveRequest<MyRow> saveRequest, string cookieName)
        {
            #region Create Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(saveRequest);
                    request.Resource = resource;
                    request.AddParameter("application/json;charset=utf-8", json, ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        public static IRestResponse<SaveResponse> BulkInsert<MyRow>(string baseUrl, string resource, List<MyRow> saveList, string cookieName)
        {
            #region Create Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(saveList);
                    request.Resource = resource;
                    request.AddParameter("application/json;charset=utf-8", "{\"Entity\":" + json + "}", ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        public static IRestResponse<SaveResponse> BulkUpdate<MyRow>(string baseUrl, string resource, List<MyRow> saveList, string cookieName)
        {
            #region Create Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(saveList);
                    request.Resource = resource;
                    request.AddParameter("application/json;charset=utf-8", "{\"Entity\":" + json + "}", ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }


        // Create row request
        public static IRestResponse<SaveResponse> Create<MyRow>(string baseUrl, string resource, SaveRequest<MyRow> saveRequest, string cookieName)
        {
            #region Create Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(saveRequest);
                    request.RequestFormat = DataFormat.Json;
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Bulk Create row request
        public static IRestResponse<ListResponse<ResponseEntity>> BulkCreate<RequestEntity, ResponseEntity>(string baseUrl, string resource, List<SaveRequest<RequestEntity>> saveRequestList, string cookieName)
        {
            #region Bulk Create Service Request
            IRestResponse<ListResponse<ResponseEntity>> response = new RestResponse<ListResponse<ResponseEntity>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(saveRequestList);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<ListResponse<ResponseEntity>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Bulk Generate rows request
        public static IRestResponse<ListResponse<ResponseEntity>> BulkGenerate<RequestEntity, ResponseEntity>(string baseUrl, string resource, RequestEntity requestParam, string cookieName)
        {
            #region Bulk Generate Rows Service Request
            IRestResponse<ListResponse<ResponseEntity>> response = new RestResponse<ListResponse<ResponseEntity>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(requestParam);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<ListResponse<ResponseEntity>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Update row request
        public static IRestResponse<SaveResponse> Update<MyRow>(string baseUrl, string resource, SaveRequest<MyRow> saveRequest, string cookieName)
        {
            #region Update Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(saveRequest);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Update row request with fileList
        public static IRestResponse<SaveResponse> Update<MyRow>(string baseUrl, string resource, SaveRequest<MyRow> saveRequest, List<string> fileList, string cookieName)
        {
            #region Update Service Request
            IRestResponse<SaveResponse> response = new RestResponse<SaveResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(saveRequest);
                    request.RequestFormat = DataFormat.Json;
                    fileList.ForEach(file =>
                    {
                        request.AddFile("receipt[receipt_file]", File.ReadAllBytes(file), "Invoice.jpg", "application/octet-stream");
                    });

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<SaveResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        //response.Data.EntityId = (object)jsonResponse["EntityId"];
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Delete row request
        public static IRestResponse<DeleteResponse> Delete(string baseUrl, string resource, DeleteRequest deleteRequest, string cookieName)
        {
            #region Delete Service Request
            IRestResponse<DeleteResponse> response = new RestResponse<DeleteResponse>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(deleteRequest);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<DeleteResponse>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // Retrieve row request
        public static IRestResponse<RetrieveResponse<MyRow>> Retrieve<MyRow>(string baseUrl, string resource, RetrieveRequest retrieveRequest, string cookieName)
        {
            #region Retrieve Service Request
            IRestResponse<RetrieveResponse<MyRow>> response = new RestResponse<RetrieveResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/octet-stream");
                    request.Resource = resource;
                    request.AddJsonBody(retrieveRequest);
                    request.RequestFormat = DataFormat.Json;

                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<RetrieveResponse<MyRow>>(request);

                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        // List of rows retrieve request
        public static IRestResponse<ListResponse<MyRow>> List<MyRow>(string baseUrl, string resource, ListRequest listRequest, string cookieName)
        {
            #region List Service Request
            IRestResponse<ListResponse<MyRow>> response = new RestResponse<ListResponse<MyRow>>();
            if (InternetConnection.IsConnectedToInternet() == true)
            {
                try
                {
                    var restClient = new RestClient(baseUrl);
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = resource
                    };
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    request.AddJsonBody(listRequest);
                    request.RequestFormat = DataFormat.Json;
                    CookieContainer rscookie = RestoreCookie(cookieName);
                    //if (rscookie == null)
                    //    return response;
                    restClient.CookieContainer = rscookie;
                    response = restClient.Execute<ListResponse<MyRow>>(request);
                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseStatus == ResponseStatus.Completed)
                    {
                        SereneRequester.SaveCookie(restClient.CookieContainer, cookieName);
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                    response.ErrorException = ex.InnerException;
                    //MessageBox.Show(ex.Message);
                }
                return response;
            }
            else
            {
                response.ErrorMessage = "Internet connection not available. Please check connection.";
                return response;
            }
            #endregion
        }

        
        public static string UploadFile(string baseUrl, string uploadPath, string filePath)
        {
            string tempFilePath = string.Empty;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                //filePath = filePath.Substring(8, filePath.Length - 1);
                string fileName = Path.GetFileName(filePath);
                var client = new RestClient(baseUrl);
                var request = new RestRequest(uploadPath, Method.POST);

                // add files to upload (works with compatible verbs)
                request.AddFile(fileName, filePath);

                // execute the request
                IRestResponse response = client.Execute(request);
                var obj = JsonConvert.DeserializeObject<FileUploadResponse>(response.Content.ToString());
                //response.Content = "{"TemporaryFile":"temporary/0ea10d13dbaa40cfb5f4dbdaf1b10304.png","Size":18650,"IsImage":true,"Width":151,"Height":95}"
                tempFilePath = uploadPath + "/" + obj.TemporaryFile;

                // Async Request
                //client.ExecuteAsync(request, response =>
                //{
                //    tempFilePath = response.Content.ToString();
                //});
            }
            return tempFilePath;
        }

        // Get FileName from Path
        public static string GetFileNameFromUri(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                int pathLength = filePath.Length - 8;
                var path = filePath.Substring(8, pathLength).ToString();
                return path;
            }
            else
            {
                return filePath;
            }
        }

        // Get FilePath from Uri
        public static string GetPathFromUri(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                int pathLength = filePath.Length - 8;
                var path = filePath.Substring(8, pathLength).ToString();
                return path;
            }
            else
            {
                return filePath;
            }
        }

        // Create TempFile for uploading purpose
        
        public static string CreateTempFile(string filePath)
        {
            #region Create Temp File
            // Generate temporary directory name
            String directory = Path.Combine(Path.GetTempPath(), "Serene");// Path.GetRandomFileName());
            // Temporary file path
            String tempfile = Path.Combine(directory, Path.GetFileName(filePath));
            // Create directory in file system
            Directory.CreateDirectory(directory);
            // Copy input file to the temporary directory
            File.Copy(filePath, tempfile);
            FileInfo fileInfo = new FileInfo(tempfile)
            {

                // Set the Attribute property of this file to Temporary. 
                // Although this is not completely necessary, the .NET Framework is able 
                // to optimize the use of Temporary files by keeping them cached in memory.
                Attributes = FileAttributes.Temporary
            };
            // Delete file in temporary directory
            //File.Delete(tempfile);
            return tempfile;
            #endregion
        }

        // Delete TempFile
        
        public static void DeleteTempFile(string tempfile)
        {
            #region Delete Temp File
            if (File.Exists(tempfile))
                File.Delete(tempfile);
            #endregion
        }

        // Save cookie 
        
        public static void SaveCookie(CookieContainer cookieContainer, string cookieName)
        {
            #region SaveCookie
            var formatter = new BinaryFormatter();
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\YourDirectoryName", cookieName);

            using (Stream s = File.Create(file))
                formatter.Serialize(s, cookieContainer);
            #endregion
        }

        // Restore cookie
        
        public static CookieContainer RestoreCookie(string cookieName)
        {
            #region RestoreCookie
            var formatter = new BinaryFormatter();
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\YourDirectoryName", cookieName);
            CookieContainer retrievedCookieContainer = null;
            if (File.Exists(file))
            {
                using (Stream s = File.OpenRead(file))
                    retrievedCookieContainer = (CookieContainer)formatter.Deserialize(s);
            }
            else
            {
                retrievedCookieContainer = null;
            }
            return retrievedCookieContainer;
            #endregion
        }
    }

    public class FileUploadResponse
    {
        public string TemporaryFile { get; set; }
        public double Size { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsImage { get; set; }
    }
}
