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
		public IActionResult ShowWeather()
		{
			if (ModelState.IsValid) 
			{
				return RedirectToAction("City", "WeatherForecasts");
			}

			return View();
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


			vm.TempMinValue = dto.TempMinValue;
			vm.TempMinUnit = dto.TempMinUnit;
			vm.TempMinUnitType = dto.TempMinUnitType;

			vm.TempMaxValue = dto.TempMaxValue;
			vm.TempMaxUnit = dto.TempMaxUnit;
			vm.TempMaxUnitType = dto.TempMaxUnitType;

			vm.DayIcon = dto.DayIcon;
			vm.DayIconPhrase = dto.DayIconPhrase;
			vm.DayHasPrecipitation = dto.DayHasPrecipitation;
			vm.DayPrecipitationType = dto.DayPrecipitationType;
			vm.DayPrecipitationIntensity = dto.DayPrecipitationIntensity;

			vm.NightIcon = dto.NightIcon;
			vm.NightIconPhrase = dto.NightIconPhrase;
			vm.NightHasPrecipitation = dto.NightHasPrecipitation;
			vm.NightPrecipitationType = dto.NightPrecipitationType;
			vm.NightPrecipitationIntensity = dto.NightPrecipitationIntensity;


			return View(vm);

		}
	}
}
