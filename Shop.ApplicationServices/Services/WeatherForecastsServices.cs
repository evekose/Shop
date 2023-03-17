using Nancy.Json;
using Shop.Core.Dto.WeatherDtos;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
	public class WeatherForecastsServices : IWeatherForecastsServices
	{
		public async Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto)
		{
			string apikey = "RH73rXRgeGF6rWyQgbVlidiDdedfngdO";
			var url = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/127964?apikey=RH73rXRgeGF6rWyQgbVlidiDdedfngdO&metric=true";


			using (WebClient client = new WebClient())
			{
				//127964 Tallinna kood
				string json = client.DownloadString(url);

				WeatherRootDto weatherInfo = new JavaScriptSerializer().Deserialize<WeatherRootDto>(json);

				dto.EffectiveDate = weatherInfo.Headline.EffectiveDate;
				dto.EffectiveEpochDate = weatherInfo.Headline.EffectiveEpochDate;
				dto.Severity = weatherInfo.Headline.Severity;
				dto.Text = weatherInfo.Headline.Text;
				dto.Category = weatherInfo.Headline.Category;
				dto.EndDate = weatherInfo.Headline.EndDate;
				dto.EndEpochDate = weatherInfo.Headline.EndEpochDate;

				dto.MobileLink = weatherInfo.Headline.MobileLink;
				dto.Link = weatherInfo.Headline.Link;

				dto.TempMinValue = weatherInfo.DailyForecasts[0].Temperature.Minimum.Value;
				dto.TempMinUnit = weatherInfo.DailyForecasts[0].Temperature.Minimum.Unit;
				dto.TempMinUnitType = weatherInfo.DailyForecasts[0].Temperature.Minimum.UnitType;

				dto.TempMaxValue = weatherInfo.DailyForecasts[0].Temperature.Maximum.Value;
				dto.TempMaxUnit = weatherInfo.DailyForecasts[0].Temperature.Maximum.Unit;
				dto.TempMaxUnitType = weatherInfo.DailyForecasts[0].Temperature.Maximum.UnitType;

				dto.DayIcon = weatherInfo.DailyForecasts[0].Day.Icon;
				dto.DayIconPhrase = weatherInfo.DailyForecasts[0].Day.IconPhrase;
				dto.DayHasPrecipitation = weatherInfo.DailyForecasts[0].Day.HasPrecipitation;
				dto.DayPrecipitationType = weatherInfo.DailyForecasts[0].Day.PrecipitationType;
				dto.DayPrecipitationIntensity = weatherInfo.DailyForecasts[0].Day.PrecipitationIntensity;

				dto.NightIcon = weatherInfo.DailyForecasts[0].Night.Icon;
				dto.NightIconPhrase = weatherInfo.DailyForecasts[0].Night.IconPhrase;
				dto.NightHasPrecipitation = weatherInfo.DailyForecasts[0].Night.HasPrecipitation;
				dto.NightPrecipitationType = weatherInfo.DailyForecasts[0].Night.PrecipitationType;
				dto.NightPrecipitationIntensity = weatherInfo.DailyForecasts[0].Night.PrecipitationIntensity;

			}

			return dto;
		}

		public async Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto)
		{
			var url = $"https://api.openweathermap.org/data/2.5/weather?lat=41.015137&lon=28.979530&appid=4cb0fe3c2a58d6d03ad5c766290af9a1";

			using (WebClient client = new WebClient())
			{
				string json = client.DownloadString(url);
				var openWeatherInfo = new JavaScriptSerializer().Deserialize<OpenWeatherDto>(json);

				dto.Temp = Math.Round(openWeatherInfo.Main.Temp - 272.15, 2);
				dto.Feels_like = Math.Round(openWeatherInfo.Main.Feels_like - 272.15, 2);
				dto.Pressure = openWeatherInfo.Main.Pressure;
				dto.Humidity = openWeatherInfo.Main.Humidity;
				dto.Visibility = openWeatherInfo.Visibility;
				dto.Timezone = openWeatherInfo.Timezone;
				dto.Id = openWeatherInfo.Id;
				dto.Name = openWeatherInfo.Name;
				dto.Country = openWeatherInfo.Sys.Country;
				dto.Sunrise = openWeatherInfo.Sys.Sunrise;
				dto.Sunset = openWeatherInfo.Sys.Sunset;
				dto.Main = openWeatherInfo.Weather[0].Main;
				dto.Description = openWeatherInfo.Weather[0].Description;
				dto.Speed = openWeatherInfo.Wind.Speed;
			}

			return dto;

		}
	}
}
