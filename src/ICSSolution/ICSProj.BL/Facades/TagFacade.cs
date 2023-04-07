using ICSProj.BL.Mappers;
using ICSProj.BL.Models;
using ICSProj.DAL.Entities;
using ICSProj.DAL.Mappers;
using ICSProj.DAL.UnitOfWork;

namespace ICSProj.BL.Facades;

public class TagFacade : FacadeBase<TagEntity, TagListModel, TagDetailModel, TagEntityMapper>
{
    private readonly ITagModelMapper _tagModelMapper;

    public TagFacade(IUnitOfWorkFactory unitOfWorkFactory,
        ITagModelMapper tagModelMapper) : base(unitOfWorkFactory, tagModelMapper) =>
        _tagModelMapper = tagModelMapper;

}
