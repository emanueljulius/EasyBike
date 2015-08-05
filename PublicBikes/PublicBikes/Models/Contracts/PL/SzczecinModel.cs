﻿using Newtonsoft.Json;

namespace PublicBikes.Models.Contracts.PL
{
    public class SzczecinModel
    {
        [JsonProperty(PropertyName = "map")]
        public SzczecinModelStation[] Stations { get; set; }
    }

    public class SzczecinModelStation : StationModelBase
    {
        [JsonProperty(PropertyName = "Latitude")]
        public override double Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public override double Longitude { get; set; }

        [JsonProperty(PropertyName = "AvailableBikesCount")]
        public override int AvailableBikes { get; set; }

        [JsonProperty(PropertyName = "FreeLocksCount")]
        public override int? AvailableBikeStands { get; set; }

        public override bool Status { get; set; }

        public override bool Banking { get; set; }
    }

}


