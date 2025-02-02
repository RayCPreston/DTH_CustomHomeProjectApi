using DTH.API.Models;

namespace DTH.API.Interfaces
{
    public interface IGetHomeProjectsService
    {
        List<HomeProject> GetHomeProjects(string queryProperty);
    }
}
