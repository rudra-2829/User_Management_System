using AarohiWPFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AarohiWPFCore.Screens;
using AarohiWPFCore.Services;

namespace AarohiWPFCore
{
    public static class NavigationEvent
    {
        public static event Action<User>? OpenMainWindowRequested;

        public static void RaiseOpenMainWindow(User user)
        {
            OpenMainWindowRequested?.Invoke(user);
        }
    }
}
