namespace ComputersExplorer.Logging
{
    /// <summary>
    /// Класс расширения для интерфейса ILoggingBuilder
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Метод для добавления кастомного логгера
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
        {
            builder.AddProvider(new FileLoggerProvider(filePath));
            return builder;
        }
    }
}