using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.WeatherDtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Weather;

namespace Shop.Controllers
{
	public class WeatherForecastsController : Controller
	{
		private readonly IWeatherForecastsServices _weatherForecastServices;

		public WeatherForecastsController
			(
				IWeatherForecastsServices weatherForecastServices
			)
		{
			_weatherForecastServices= weatherForecastServices;
		}

		public IActionResult Index()
		{
			WeatherViewModel vm = new WeatherViewModel();


			return View(vm);
		}

		[HttpPost]
		public IActionResult ShowWeather(WeatherViewModel vm)
		{
			if (ModelState.IsValid) 
			{
				return RedirectToAction("City", "WeatherForecasts");
			}

			return View(vm);
		}

		[HttpGet]
		public IActionResult City()
		{
			WeatherResultDto dto = new WeatherResultDto();
			WeatherViewModel vm = new WeatherViewModel();

			_weatherForecastServices.WeatherDetail(dto);

			vm.Date = dto.EffectiveDate;
			vm.EpochDate = dto.EffectiveEpochDate;
			vm.Severity = dto.Severity;
			vm.Text =  dto.Text;
			vm.MobileLink = dto.MobileLink;
			vm.Link = dto.Link;
			vm.Category = dto.Category;


			vm.Temperature.Minimum.Value = dto.TempMinValue;
			vm.Temperature.Minimum.Unit = dto.TempMinUnit;
			vm.Temperature.Minimum.UnitType = dto.TempMinUnitType;

			vm.Temperature.Maximum.Value = dto.TempMaxValue;
			vm.Temperature.Maximum.Unit = dto.TempMaxUnit;
			vm.Temperature.Maximum.UnitType = dto.TempMaxUnitType;

			vm.DayNightCycle.Icon = dto.DayIcon;
			vm.DayNightCycle.IconPhrase = dto.DayIconPhrase;
			vm.DayNightCycle.HasPrecipitation = dto.DayHasPrecipitation;
			vm.DayNightCycle.PrecipitationType = dto.DayPrecipitationType;
			vm.DayNightCycle.PrecipitationIntensity = dto.DayPrecipitationIntensity;

			vm.DayNightCycle.Icon = dto.NightIcon;
			vm.DayNightCycle.IconPhrase = dto.NightIconPhrase;
			vm.DayNightCycle.HasPrecipitation = dto.NightHasPrecipitation;
			vm.DayNightCycle.PrecipitationType = dto.NightPrecipitationType;
			vm.DayNightCycle.PrecipitationIntensity = dto.NightPrecipitationIntensity;


			return View(vm);

		}
	}
}
