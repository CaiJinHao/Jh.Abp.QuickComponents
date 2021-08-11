using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Jh.Abp.Common
{
    public class UtilTree
    {
        public static async Task<List<T>> GetMenusTreeAsync<T>(List<T> menus) where T : TreeDto
        {
            var _type = typeof(T);
            //组装树
            async Task<IEnumerable<T>> GetChildNodesAsync(string parentNodeId)
            {
                var childs = menus.Where(a => a.parent_id == parentNodeId);
                foreach (var item in childs)
                {
                    if (_type == typeof(NavTreeDto))
                    {
                        (item as NavTreeDto).children = await GetChildNodesAsync(item.id) as IEnumerable<NavTreeDto>;
                    }
                    else
                    {
                        (item as CheckTreeDto).data = await GetChildNodesAsync(item.id) as IEnumerable<CheckTreeDto>;
                    }
                }
                return childs.OrderBy(a => a.sort).ToList();
            }

            //找到根节点
            var roots = menus.Where(a => a.parent_id == null || a.parent_id == "").OrderBy(a => a.sort).ToList();
            foreach (var item in roots)
            {
                if (_type == typeof(NavTreeDto))
                {
                    (item as NavTreeDto).children = (await GetChildNodesAsync(item.id) as IEnumerable<NavTreeDto>).OrderBy(a => a.sort);
                }
                else
                {
                    (item as CheckTreeDto).data = (await GetChildNodesAsync(item.id) as IEnumerable<CheckTreeDto>).OrderBy(a => a.sort);
                }
            }
            return roots;
        }
    }
}
