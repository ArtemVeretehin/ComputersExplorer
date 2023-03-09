using ComputersExplorer.DTO;
using ComputersExplorer.DBO;
using ComputersExplorer.Repositories;

namespace ComputersExplorer.Logic
{
    /// <summary>
    /// Класс-провайдер с логикой обработки запросов по компьютерам
    /// </summary>
    public class ComputerLogicProvider
    {
        public ComputerRepository ComputerRepository;
        public ComputerLogicProvider(ComputerRepository _ComputerRepository)
        {
            this.ComputerRepository = _ComputerRepository;
        }

        /// <summary>
        /// Получение списка всех компьютеров
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Computer> GetComputers()
        {
            return ComputerRepository.GetAll();
        }

        /// <summary>
        /// Добавление компьютера в контекст
        /// </summary>
        /// <param name="Computer"></param>
        public void AddComputer(Computer Computer)
        {
            ComputerRepository.Add(Computer);
        }

        /// <summary>
        /// Удаление компьютера из контекста 
        /// </summary>
        /// <param name="Computer"></param>
        public void DeleteComputer(Computer Computer)
        {
            ComputerRepository.Remove(Computer);
        }

        /// <summary>
        /// Получение компьютера по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Computer GetComputerById(int id)
        {
            return ComputerRepository.GetById(id);
        }

        /// <summary>
        /// Сохранение изменений в контексте
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChanges()
        {
            return ComputerRepository.SaveChanges();
        }
    }
}
