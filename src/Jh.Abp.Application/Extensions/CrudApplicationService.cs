using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace Jh.Abp.Extensions
{
    public abstract class CrudApplicationService<TEntity, TEntityDto, TPagedRetrieveInputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        : CrudAppService<TEntity, TEntityDto, TPagedRetrieveInputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto>
        , ICrudApplicationService<TEntity, TEntityDto, TPagedRetrieveInputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
        where TPagedRetrieveInputDto : IEntityDto<TKey>
    {
        public CrudApplicationService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }

        public async Task DeleteAsync(TDeleteInputDto deleteInputDto)
        {
            var delModels = await Repository.GetListAsync();
            await DeleteAsync(delModels.Select(a => a.Id).ToArray());
        }

        public async Task DeleteAsync(TKey[] keys)
        {
            await Repository.DeleteAsync(a => keys.Contains(a.Id));
        }

        public async Task<List<TEntityDto>> GetModelsAsync(TRetrieveInputDto inputDto)
        {
            var query = ReadOnlyRepository;
            var entities = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities);
        }

        public async Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto updateInput)
        {
            var entity = await Repository.GetAsync(key);

            var toFields = entity.GetType().GetProperties();
            var formFields = updateInput.GetType().GetProperties();
            foreach (var item in formFields)
            {
                var toField = toFields.Where(a => a.Name == item.Name).FirstOrDefault();
                if (toField != null)
                {
                    var _value = item.GetValue(updateInput);
                    if (_value != null)
                    {
                        var fieldType = _value.GetType();
                        if (fieldType.IsEnum)
                        {
                            var _v = (int)_value;
                            if (_v > 0)
                            {
                                toField.SetValue(entity, _value);
                            }
                        }
                        else
                        {
                            switch (fieldType.Name)
                            {
                                case "String":
                                    {
                                        if (!string.IsNullOrEmpty((string)_value))
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "DateTime":
                                    {
                                        var _v = (DateTime)_value;
                                        if (_v > new DateTime(1900, 1, 1))
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int16":
                                    {
                                        var _v = (Int16)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int32":
                                    {
                                        var _v = (Int32)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int64":
                                    {
                                        var _v = (Int64)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Decimal":
                                    {
                                        var _v = (decimal)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Boolean":
                                    {
                                        toField.SetValue(entity, _value);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return entity;
        }
    }
}
