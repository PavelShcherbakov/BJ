using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.ViewModels.GameViews
{
    public class StartGameResponseView
    {
        public Guid GameId { get; set; }
        public int State { get; set; }
    }
}
