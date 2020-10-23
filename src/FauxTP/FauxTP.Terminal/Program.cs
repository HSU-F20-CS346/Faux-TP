using System;
using System.Data;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Terminal.Gui;

namespace FauxTP.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.Green, Color.Black);

            // Main menu
            var menu = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem ("_File", new MenuItem []
                {
                    // Show names of FauxTP team and other general information
                    new MenuItem ("_About", "", () =>
                    {
                        OpenAboutDialog ();
                    }),

                    new MenuItem ("_Options", "", () =>
                    {
                        OpenOptionsDialog ();
                    }),

                    // Close application
                    new MenuItem ("_Quit", "", () =>
                    {
                        Application.RequestStop ();
                    })
                }),
                new MenuBarItem ("_Connect", new MenuItem []
                {
                    // Connect to server at specified IP with username and password
                    new MenuItem ("_Login", "", () =>
                    {
                        OpenLoginDialog ();
                    }),

                    // Disconnect from the current server
                    new MenuItem ("_Disconnect", "", () =>
                    {
                        CloseConnection ();
                    })
                }),
                new MenuBarItem ("_Help", new MenuItem []
                {
                    // Show basic functionality and link to documentation
                    new MenuItem ("_Show Help", "", () =>
                    {
                        OpenHelpDialog ();
                    })
                })
            });

            // Command / control window
            var fauxtp = new Window("FauxTP")
            {
                X = 0,
                Y = 1,
                Width = Dim.Percent(34),
                Height = Dim.Fill(),
                ColorScheme = Colors.Base
            };

            // Directory view of local files
            var localDir = new Window("Local Directory")
            {
                X = Pos.Percent(34),
                Y = 1,
                Width = Dim.Percent(66),
                Height = Dim.Percent(50),
                ColorScheme = Colors.Base
            };

            // Directory view of remote files
            var peerDir = new Window("Peer Directory")
            {
                //X = Pos.Percent(67),
                X = Pos.Percent(34),
                Y = Pos.Percent(50),
                Width = Dim.Percent(66),
                Height = Dim.Percent(50),
                ColorScheme = Colors.Base
            };
            
            var download = new Button(5, 1, "     Download File     ") { };
            download.Clicked += () => { DownloadFile(); };

            var upload = new Button(5, 3, "      Upload File      ") { };
            upload.Clicked += () => { UploadFile(); };

            var pause = new Button(5, 5, "     Pause Transfer    ") { };
            pause.Clicked += () => { PauseTransfer(); };

            var resume = new Button(5, 7, "    Resume Transfer    ") { };
            resume.Clicked += () => { ResumeTransfer(); };

            var cancel = new Button(5, 9, "    Cancel Transfer    ") { };
            cancel.Clicked += () => { CancelTransfer(); };

            fauxtp.Add(download, upload, pause, resume, cancel);

            // Add components to application window
            Application.Top.Add(menu, fauxtp, localDir, peerDir);
            Application.Run();
        }

        static void OpenAboutDialog()
        {
            var aboutInfo = new Label(
                "Project for CS 346 @ HSU \n \n" +
                "Designed by Vanja Venezia, Riley Heffernan, \n" +
                "Fernando Crespo, James Pelligra, Candance M., \n" +
                "Ryan Beck, Grayson Beckert, and Bradley Arline")
            {
                X = 1,
                Y = 1
            };

            var ok = new Button(20, 7, "Cool!") { };
            ok.Clicked += () => { Application.RequestStop(); };

            var dialog = new Dialog("Login", 50, 11, ok);
            dialog.Add(aboutInfo);

            Application.Run(dialog);
        }

        static void OpenOptionsDialog()
        {
            //TODO: Make options functional

            var saveIP = new CheckBox(1, 1, "Save server IP", GlobalBools.saveIP) { };
            var saveUsername = new CheckBox(1, 3, "Save username", GlobalBools.saveUsername) { };
            var savePassword = new CheckBox(1, 5, "Save password", GlobalBools.savePassword) { };
            var saveSession = new CheckBox(1, 7, "Save session history to log", GlobalBools.saveSession) { };

            var clearInfo = new Button(1, 9, "Clear saved information") { };
            clearInfo.Clicked += () => { ClearSavedUserInfo(); };
            var closeMenu = new Button(1, 11, "Save and close options menu") { };
            closeMenu.Clicked += () => { 
                if (saveIP.Checked) { GlobalBools.saveIP = true; }
                else { GlobalBools.saveIP = false; };
                if (saveUsername.Checked) { GlobalBools.saveUsername = true; }
                else { GlobalBools.saveUsername = false; };
                if (savePassword.Checked) { GlobalBools.savePassword = true; }
                else { GlobalBools.savePassword = false; };
                if (saveSession.Checked) { GlobalBools.saveSession = true; }
                else { GlobalBools.saveSession = false; };
                Application.RequestStop(); };

            var dialog = new Dialog("Options", 35, 15, clearInfo, closeMenu);
            dialog.Add(saveIP, saveUsername, savePassword, saveSession);

            Application.Run(dialog);
        }

        static void OpenLoginDialog()
        {
            bool connectPressed = false;

            var connect = new Button(16, 11, "Connect") { };
            connect.Clicked += () => { Application.RequestStop(); connectPressed = true; };

            var cancel = new Button(31, 11, "Cancel") { };
            cancel.Clicked += () => { Application.RequestStop(); };

            var dialog = new Dialog("Login", 60, 15, connect, cancel);

            var ipForm = new TextField()
            {
                X = 1,
                Y = 2,
                Width = Dim.Fill(),
                Height = 1
            };
            var usernameForm = new TextField()
            {
                X = 1,
                Y = 5,
                Width = Dim.Fill(),
                Height = 1
            };
            var passwordForm = new TextField()
            {
                X = 1,
                Y = 8,
                Width = Dim.Fill(),
                Height = 1,
                Secret = true
            };

            var ipLabel = new Label("Server Address")
            {
                X = 1,
                Y = 1
            };
            var usernameLabel = new Label("Username")
            {
                X = 1,
                Y = 4
            };
            var passwordLabel = new Label("Password")
            {
                X = 1,
                Y = 7
            };

            dialog.Add(ipLabel);
            dialog.Add(ipForm);
            dialog.Add(usernameLabel);
            dialog.Add(usernameForm);
            dialog.Add(passwordLabel);
            dialog.Add(passwordForm);

            Application.Run(dialog);

            if (connectPressed)
            {
                //TODO: Add auth logic
            }
        }
        static void CloseConnection()
        {
            //TODO: Close connection
        }

        static void OpenHelpDialog()
        {
            //TODO: Tutorialize basic features and link to docs in dialog box
        }

        static void DownloadFile()
        {
            //TODO: Download specified file from peer
        }

        static void UploadFile()
        {
            //TODO: Send specified file to peer
        }

        static void PauseTransfer()
        {
            //TODO: Pause transfer
        }

        static void ResumeTransfer()
        {
            //TODO: Resume transfer

            // Potentially remove and implement both
            // pause and resume functionality in the
            // PauseTrasnfer() method using a bool
        }

        static void CancelTransfer()
        {
            //TODO: Cancel transfer
        }

        static void ClearSavedUserInfo()
        {
            //TODO: Clear saved user information from config 
            //      by writing empty strings to config fields
        }
    }

    class GlobalBools
    {
        //TODO: Load these bool values from config

        public static bool saveIP = false;
        public static bool saveUsername = false;
        public static bool savePassword = false;
        public static bool saveSession = false;
    }
}
