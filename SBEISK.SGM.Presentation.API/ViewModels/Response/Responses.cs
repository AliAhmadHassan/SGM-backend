using System.Collections.Generic;

namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public static class Responses
    {
        public static IDictionary<ResponseEnum, string> Messages = new Dictionary<ResponseEnum, string>()
        {
            { ResponseEnum.Error, "Erro interno de servidor" },
            { ResponseEnum.Success, "Sucesso" },
            { ResponseEnum.NoModified, "Não foi possível modificar" },
            { ResponseEnum.Created, "Registro salvo com sucesso" },
            { ResponseEnum.Removed, "Registro excluído com sucesso" },
            { ResponseEnum.Modified, "Registro alterado com sucesso" }
        };
    }
}