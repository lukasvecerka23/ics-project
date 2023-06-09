using ICSProj.BL.Models;
using ICSProj.DAL.Entities;

namespace ICSProj.BL.Facades;

public interface ITagFacade: IFacade<TagEntity, TagListModel, TagDetailModel>
{
    public Task<IEnumerable<TagListModel>?> GetTagsByUser(Guid userId);
}
