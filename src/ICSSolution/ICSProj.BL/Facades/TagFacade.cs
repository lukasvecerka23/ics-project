using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.Repositories;
using ICSProj.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICSProj.BL.Facades;

public class TagFacade : FacadeBase<TagEntity, TagListModel, TagDetailModel, TagEntityMapper>
{
    private readonly ITagModelMapper _tagModelMapper;

    public TagFacade(IUnitOfWorkFactory unitOfWorkFactory,
        ITagModelMapper tagModelMapper) : base(unitOfWorkFactory, tagModelMapper) =>
        _tagModelMapper = tagModelMapper;

    public async Task<IEnumerable<TagListModel>> GetTagsByUser(Guid userId)
    {
        IRepository<TagEntity> tagRepository = UnitOfWorkFactory.Create().GetRepository<TagEntity, TagEntityMapper>();

        var tagEntities = tagRepository.Get();

        var tagsByUser = tagEntities.Where(tag => tag.CreatorId == userId);
        List<TagEntity> tagsByUserList = await tagsByUser.ToListAsync();
        
        return tagsByUser.Any() ? _tagModelMapper.MapToListModel(tagsByUserList) : null;
    }

}
