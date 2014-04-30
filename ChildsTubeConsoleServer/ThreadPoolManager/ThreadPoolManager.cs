using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;
using ChildsTubeConsoleServer.ViewModel;
using ChildsTubeConsoleServer.Communications;

namespace ChildsTubeConsoleServer.ThreadPoolManagerNS
{
    public class ThreadPoolManager
    {
        MainWindowViewModel MainWindowViewModel { get; set; }

        public ThreadPoolManager(MainWindowViewModel mainWindowViewModel)
        {
            this.MainWindowViewModel = mainWindowViewModel;
        }

        public void AddTask(HttpListenerContext context)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolWorkerCallback), context);
        }

        private void ThreadPoolWorkerCallback(object state)
        {
            HttpListenerContext context = state as HttpListenerContext;
            HttpListenerRequest request = context.Request;
            if (request != null && request.HasEntityBody)
            {
                using (Stream body = request.InputStream) // here we have data
                {
                    using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                    {
                        string strData = reader.ReadToEnd();

                        dynamic dynamicObject = JsonConvert.DeserializeObject(strData);

                        Nullable<CommunicationsHelper.UserToServerMessage> type = dynamicObject.type;
                        if (type != null)
                        {
                            switch (type)
                            {
                                case CommunicationsHelper.UserToServerMessage.GetTvSeriesDetails:

                                    //HandleSearchResultsMessage(dynamicObject, context);

                                    break;

                                case CommunicationsHelper.UserToServerMessage.SearchPhrase:

                                    //HandleGetAllJoinedWeddingsMessage(dynamicObject, context);
                                    
                                    break;

                                default:

                                    MainWindowViewModel.LogManager.PrintWarningMessage("User --> Server: Unknown");

                                    break;

                                //case Helpers.Helpers.MessageTypeFromClient.RetreiveLastMessages:

                                //    HandleRetreiveLastMessagesMessage(strData, context);

                                //    break;



                                //case Helpers.Helpers.MessageTypeFromClient.RetreiveTopHits:



                                //    break;


                                //case Helpers.Helpers.MessageTypeFromClient.PushGreeting:

                                //    HandlePushGreetingMessage(strData, context);

                                //    break;
                            }
                        }
                    }
                }
            }
        }

       
        //private void HandleGetAllJoinedWeddingsMessage(dynamic dynamicObject, HttpListenerContext context)
        //{
        //    try
        //    {
        //        dynamic sendFlexible = new ExpandoObject();
        //        sendFlexible.Type = Helpers.Helpers.MessageTypeToClient.GetAllJoinedWeddings;
        //        var weddings = from wedding in MainWindowViewModel.dataEntities.Wedding
        //                       where wedding.Member.First_Name.Contains("Chen") || wedding.Member1.First_Name.Contains("Chen") || wedding.Member.First_Name.Contains("Eti") || wedding.Member1.First_Name.Contains("Eti")
        //                       select wedding;

        //        List<ExpandoObject> flexibleWeddingList = new List<ExpandoObject>();
        //        foreach (var weddingLinq in weddings)
        //        {
        //            dynamic flexibleWedding = new ExpandoObject();
        //            flexibleWedding.BrideFullName = weddingLinq.Member.First_Name + " " + weddingLinq.Member.Last_Name;
        //            flexibleWedding.GroomFullName = weddingLinq.Member1.First_Name + " " + weddingLinq.Member1.Last_Name;
        //            flexibleWedding.Date = weddingLinq.Date;
        //            flexibleWedding.Place = weddingLinq.Place;
        //            if (weddingLinq.Photo != null)
        //                flexibleWedding.Image = weddingLinq.Photo.Image_Path;
        //            flexibleWeddingList.Add(flexibleWedding);
        //        }

        //        sendFlexible.Weddings = flexibleWeddingList;

        //        SendMessage(context, sendFlexible);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogAddMessage("Message has exception: " + exception.Message + ". InnerMessage: " + exception.InnerException);
        //    }
        //}

        //private void HandleLikeGreetingMessage(dynamic dynamicObject, HttpListenerContext context)
        //{
        //    try
        //    {
        //        Member userOut = null;
        //        List<Member> membersList = MainWindowViewModel.dataEntities.Member.ToList();
        //        string email = dynamicObject.Email;
        //        if (UserExists(membersList, email, out userOut) == true)
        //        {
        //            Debug.Assert(userOut != null);

        //            if (userOut.Is_Blocked == true)
        //            {
        //                LogAddMessage("Received message from blocked user: " + dynamicObject.UserFirstName + " " + dynamicObject.UserLastName + ". ignoring.");

        //                return;
        //            }

        //            int? greetingId = dynamicObject.GreetingId;
        //            if (greetingId != null)
        //            {
        //                foreach (var greeting in this.MainWindowViewModel.dataEntities.Greeting)
        //                {
        //                    if (greeting.Greeting_ID == (int)greetingId)
        //                    {
        //                        greeting.Like.Add(new Like() { Greeting_ID = greetingId, Member_ID = userOut.Member_ID });
                                
        //                        dynamic sendFlexible = new ExpandoObject();
        //                        sendFlexible.Type = Helpers.Helpers.MessageTypeToClient.AOK;

        //                        SendMessage(context, sendFlexible);

        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogAddMessage("Message has exception: " + exception.Message + ". InnerMessage: " + exception.InnerException);
        //    }
        //}


        //private void HandleSearchResultsMessage(dynamic dynamicObject, HttpListenerContext context)
        //{
        //    try
        //    {
        //        dynamic sendFlexible = new ExpandoObject();
        //        sendFlexible.Type = Helpers.Helpers.MessageTypeToClient.AOK;
        //        var weddings = from wedding in MainWindowViewModel.dataEntities.Wedding
        //                       where wedding.Member.First_Name.Contains("Chen") || wedding.Member1.First_Name.Contains("Chen") || wedding.Member.First_Name.Contains("Eti") || wedding.Member1.First_Name.Contains("Eti")
        //                       select wedding;

        //        List<ExpandoObject> flexibleWeddingList = new List<ExpandoObject>();
        //        foreach (var weddingLinq in weddings.Take(10))
        //        {
        //            dynamic flexibleWedding = new ExpandoObject();
        //            flexibleWedding.BrideFullName = weddingLinq.Member.First_Name + " " + weddingLinq.Member.Last_Name;
        //            flexibleWedding.GroomFullName = weddingLinq.Member1.First_Name + " " + weddingLinq.Member1.Last_Name;
        //            flexibleWedding.Date = weddingLinq.Date;
        //            flexibleWedding.Place = weddingLinq.Place;
        //            if (weddingLinq.Photo != null)
        //                flexibleWedding.Image = weddingLinq.Photo.Image_Path;
        //            flexibleWeddingList.Add(flexibleWedding);
        //        }

        //        sendFlexible.Results = flexibleWeddingList;

        //        SendMessage(context, sendFlexible);
        //    }
        //    catch (Exception exception)
        //    {
        //        LogAddMessage("Message has exception: " + exception.Message + ". InnerMessage: " + exception.InnerException);
        //    }
        //}

        //private void HandleRegistrationMessage(dynamic dynamicObject, HttpListenerContext context)
        //{
        //    try
        //    {
        //        if (dynamicObject != null)
        //        {
        //            Member userOut = null;
        //            if (UserExists(MainWindowViewModel.dataEntities.Member.ToList(), dynamicObject.Email, out userOut) == true)
        //            {
        //                Debug.Assert(userOut != null);

        //                if (userOut.Is_Blocked == true)
        //                {
        //                    LogAddMessage("Received message from blocked user: " + dynamicObject.UserFirstName + " " + dynamicObject.UserLastName + ". ignoring.");

        //                    dynamic sendFlexible = new ExpandoObject();
        //                    sendFlexible.Type = Helpers.Helpers.MessageTypeToClient.Error;
        //                    sendFlexible.ErrorType = Helpers.Helpers.ErrorType.UserAlreadyExists;

        //                    SendMessage(context, sendFlexible);

        //                    return;
        //                }
        //            }
        //            else // user doesn't exist
        //            {
        //                if (dynamicObject.UserFirstName != null && dynamicObject.UserLastName != null)
        //                {
        //                    LogAddMessage("Adding user (" + dynamicObject.UserFirstName + " " + dynamicObject.UserLastName + ")!");
        //                    MainWindowViewModel.dataEntities.Member.Add(new Member()
        //                    {
        //                        First_Name = dynamicObject.UserFirstName,
        //                        Last_Name = dynamicObject.UserLastName,
        //                        Is_Blocked = false,
        //                        Email = dynamicObject.Email,
        //                        Token_ID = dynamicObject.DeviceToken,
        //                        Permission_ID = 5
        //                    });
        //                    MainWindowViewModel.SaveDBData();

        //                    dynamic sendFlexible = new ExpandoObject();
        //                    sendFlexible.Type = Helpers.Helpers.MessageTypeToClient.AOK;

        //                    SendMessage(context, sendFlexible);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogAddMessage("Message has exception: " + exception.Message + ". InnerMessage: " + exception.InnerException);
        //    }
        //}

        private void SendMessage(HttpListenerContext context, dynamic dynamicObject)
        {
            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            string responseString = JsonConvert.SerializeObject(dynamicObject);

            MainWindowViewModel.LogManager.PrintSuccessMessage("Server --> User: " + responseString);

            // Construct a response.
            byte[] buffer = Encoding.Unicode.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            response.ContentEncoding = Encoding.Unicode;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }
    }
}
