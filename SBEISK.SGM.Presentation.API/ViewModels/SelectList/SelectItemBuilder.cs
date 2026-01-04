using System;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Presentation.API.ViewModels.SelectList
{
    public static class SelectItemBuilder 
    {
        /// <summary>
        /// Método que cria uma lista de SelectItem para ser utilizado por combobox
        /// </summary>
        /// <typeparam name="T">Tipo de classe contida na lista</typeparam>
        /// <typeparam name="TId">Tipo de dado que será retornado como "id"</typeparam>
        /// <typeparam name="TText">Tipo de dado que será retornado como "text"</typeparam>
        /// <param name="items">Lista de dados a ser tratada</param>
        /// <param name="idSelector">Expressão que retorna a propriedade que será usada como Id</param>
        /// <param name="textSelector">Expressão que retorna a propriedade que será usada como Text</param>
        /// <returns></returns>
        public static IList<SelectItem<TId, TText>> Generate<T, TId, TText>(IList<T> items, Func<T,TId> idSelector, Func<T, TText> textSelector)
        {
            return items.Select(x => new SelectItem<TId, TText>(idSelector(x), textSelector(x))).ToList();
        }
    }
}
