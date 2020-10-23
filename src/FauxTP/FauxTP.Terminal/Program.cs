using System;
using System.Data;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
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
                Width = Dim.Percent(34),
                Height = Dim.Fill(),
                ColorScheme = Colors.Base
            };

            // Directory view of remote files
            var peerDir = new Window("Peer Directory")
            {
                X = Pos.Percent(67),
                Y = 1,
                Width = Dim.Percent(34),
                Height = Dim.Fill(),
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

        static void OpenLoginDialog()
        {
            bool connectPressed = false;
            
            var connect = new Button(16, 11, "Connect") {};
            connect.Clicked += () => { Application.RequestStop(); connectPressed = true; };

            var cancel = new Button(31, 11, "Cancel") {};
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

        static void OpenHelpDialog()
        {
            //TODO: Tutorialize basic features and link to docs in dialog box
        }

        static void CloseConnection()
        {
            //TODO: Close connection
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
    }
}
