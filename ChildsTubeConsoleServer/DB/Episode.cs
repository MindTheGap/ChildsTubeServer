//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChildsTubeConsoleServer.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Episode
    {
        public int EpisodeID { get; set; }
        public int TvSeriesID { get; set; }
        public int EpisodeNumber { get; set; }
        public string URLPath { get; set; }
        public Nullable<int> SeriesNumber { get; set; }
    
        public virtual TV_Series TV_Series { get; set; }
    }
}
