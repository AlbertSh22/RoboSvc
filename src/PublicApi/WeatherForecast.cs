namespace PublicApi
{
    /// <summary>
    /// eee
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// fff
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ddd
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ggg
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// hhh
        /// </summary>
        public string? Summary { get; set; }
    }
}