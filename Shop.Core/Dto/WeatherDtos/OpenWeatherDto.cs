using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Shop.Core.Dto.WeatherDtos.OpenWeatherDto;

namespace Shop.Core.Dto.WeatherDtos
{
	public class OpenWeatherDto
	{
		[JsonPropertyName("coord")]
		public Coords Coord { get; set; }

		[JsonPropertyName("main")]
		public Mains Main { get; set; }

		[JsonPropertyName("weather")]
		public List<Weathers> Weather { get; set; }

		[JsonPropertyName("wind")]
		public Winds Wind { get; set; }

		[JsonPropertyName("sys")]
		public Syss Sys { get; set; }

		[JsonPropertyName("timezone")]
		public int Timezone { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("visibility")]
		public int Visibility { get; set; }


		public class Coords
		{
			[JsonPropertyName("lon")]
			public double Lon { get; set; }

			[JsonPropertyName("lon")]
			public double Lat { get; set; }
		}

		public class Mains
		{
			[JsonPropertyName("temp")]
			public double Temp { get; set; }

			[JsonPropertyName("feels_like")]
			public double Feels_like { get; set; }

			[JsonPropertyName("pressure")]
			public int Pressure { get; set; }

			[JsonPropertyName("humidity")]
			public int Humidity { get; set; }
		}

		public class Syss
		{
			[JsonPropertyName("country")]
			public string Country { get; set; }

			[JsonPropertyName("sunrise")]
			public int Sunrise { get; set; }

			[JsonPropertyName("sunset")]
			public int Sunset { get; set; }
		}

		public class Weathers
		{
			[JsonPropertyName("main")]
			public string Main { get; set; }

			[JsonPropertyName("description")]
			public string Description { get; set; }
		}

		public class Winds
		{
			[JsonPropertyName("speed")]
			public double Speed { get; set; }
		}
	}
}
