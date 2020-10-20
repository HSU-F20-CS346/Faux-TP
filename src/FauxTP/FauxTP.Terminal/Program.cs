using System;
using System.Data;
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

            var menu = new MenuBar(new MenuBarItem[]
            {
                new MenuBarItem ("_File", new MenuItem []
                {
                    new MenuItem ("_Quit", "", () =>
                    {
                        Application.RequestStop ();
                    })
                }),
                new MenuBarItem ("_Connect", new MenuItem []
                {
                    new MenuItem ("_Login", "", () =>
                    {
                        OpenLoginDialog ();
                    })
                })
            });

            var win = new Window("FauxTP")
            {
                X = 0,
                Y = 1,
                Width = Dim.Percent(50),
                Height = Dim.Fill() - 1,
                ColorScheme = Colors.Base
            };

            var dir = new Window("File Directory")
            {
                X = Pos.Percent(50),
                Y = 1,
                Width = Dim.Percent(50),
                Height = Dim.Fill() - 1,
                ColorScheme = Colors.Base
            };

            // Add both menu and win in a single call
            Application.Top.Add(menu, win, dir);
            Application.Run();
        }

        static void OpenLoginDialog()
        {
            bool connectPressed = false;
            
            var connect = new Button(16, 11, "Connect")
            {
                //Clicked = () => { Application.RequestStop(); connectPressed = true; }
            };

            connect.Clicked += () => { Application.RequestStop(); connectPressed = true; };

            var cancel = new Button(31, 11, "Cancel")
            {
                //Clicked = () => Application.RequestStop()
            };

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
    }
}
