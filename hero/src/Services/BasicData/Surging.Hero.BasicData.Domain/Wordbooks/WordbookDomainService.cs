﻿using System.Threading.Tasks;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Repositories;
using Surging.Hero.BasicData.IApplication.Wordbook.Dtos;

namespace Surging.Hero.BasicData.Domain.Wordbooks
{
    public class WordbookDomainService : IWordbookDomainService
    {
        private readonly IDapperRepository<Wordbook, long> _wordbookRepository;
        public WordbookDomainService(IDapperRepository<Wordbook, long> wordbookRepository) {
            _wordbookRepository = wordbookRepository;
        }

        public async Task CreateWordbook(CreateWordbookInput input)
        {
            var wordbook = await _wordbookRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (wordbook != null) {
                throw new BusinessException($"系统中已经存在{input.Code}的字典类型");
            }
            wordbook = input.MapTo<Wordbook>();
            await _wordbookRepository.InsertAsync(wordbook);
        }
    }
}
