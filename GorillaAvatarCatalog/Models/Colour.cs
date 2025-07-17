using System;
using Newtonsoft.Json;
using UnityEngine;

namespace GorillaAvatarCatalog.Models
{
    [Serializable]
    public class Colour
    {
        [JsonProperty(PropertyName = "r")]
        public int Red { get; set; }

        [JsonProperty(PropertyName = "g")]
        public int Green { get; set; }

        [JsonProperty(PropertyName = "b")]
        public int Blue { get; set; }

        public static implicit operator Color(Colour colour)
        {
            return new Color(colour.Red / 255f, colour.Green / 255f, colour.Blue / 255f);
        }

        public static implicit operator Colour(Color colour)
        {
            return new Colour
            {
                Red = Mathf.FloorToInt(colour.r * 255),
                Green = Mathf.FloorToInt(colour.g * 255),
                Blue = Mathf.FloorToInt(colour.b * 255)
            };
        }
    }
}
