using Shop.Core.Dto.WeatherDtos;


namespace Shop.Core.ServiceInterface
{
	public interface IWeatherForecastsServices
	{
		Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto);
		Task<OpenWeatherResultDto> OpenWeatherDetail(OpenWeatherResultDto dto);
	}
}
