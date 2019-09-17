using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class ViewerLogic
    {
        private readonly IViewerContext _viewer;

        public ViewerLogic(IViewerContext viewer)
        {
            _viewer = viewer;
        }
    }
}
