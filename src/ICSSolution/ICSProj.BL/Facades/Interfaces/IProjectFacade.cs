using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Facades;

public interface IProjectFacade: IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
