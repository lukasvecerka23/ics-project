using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Mappers;

public interface IProjectAssignModelMapper
    : IModelMapper<ProjectAssignEntity, ProjectAssignListModel, ProjectAssignDetailModel>
{
}

