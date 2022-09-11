namespace DevagramCSharp.IMapper
{
    public interface IMapper<TSource, TDest>
    {
        //TDest  : Entity
        //TSource: Dto
        TDest MapearDtoParaEntidade(TSource src);
        TSource MapearEntidadeParaDto(TDest src);
    }
}
